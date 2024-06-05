using FPLSP_Analyst.Application.ViewModels.Excels.Mics;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace FPLSP_Analyst.Infrastructure.Implements.Services.Additional
{
    public class ExcelServices<T>
    {
        private readonly List<string>? ListExcelHeaders;

        public ExcelServices(List<string>? listExcelHeaders = null)
        {
            ListExcelHeaders = listExcelHeaders;
        }

        public async Task<List<T>> GetValueFromFileStream(MemoryStream stream, List<int> ValidationValidRows, int startRow = 2, string? sheetName = null)
        {
            ExcelValidationResultVM result = new();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage(stream);
            var workbook = package.Workbook;
            var findWorkSheet = sheetName == null ? workbook.Worksheets.FirstOrDefault(c => c.Name == sheetName) : workbook.Worksheets.First();
            var worksheet = findWorkSheet == null ? workbook.Worksheets.First() : findWorkSheet;

            worksheet = TrimWorksheet(worksheet);

            var countRow = worksheet.Dimension.End.Row;
            var countCol = worksheet.Dimension.End.Column;

            var props = typeof(T).GetProperties();


            var listObjHeaders = typeof(T).GetProperties().Select(c => c.Name).ToList();
            var listExcelHeaders = ListExcelHeaders ?? listObjHeaders;

            JArray jArrResult = new();

            for (var i = 1; i <= countRow; i++)
            {
                if (ValidationValidRows.Any(c => c == i))
                {
                    JObject jObj = new();
                    foreach (var header in listObjHeaders)
                    {
                        var curCol = listObjHeaders.IndexOf(header) + 1;
                        var valCol = worksheet.Cells[i, curCol].Value;
                        jObj[header] = valCol == null ? "" : valCol.ToString();
                    }

                    jArrResult.Add(jObj);
                }
            }

            List<T> listResult = new();

            foreach (var jToken in jArrResult)
            {
                var obj = jToken.ToObject<T>();
                listResult.Add(obj);
            }

            return listResult;
        }


        public async Task<ExcelValidationResultVM> Validate(MemoryStream stream, List<ExcelAcceptedValueVM>? lstAcceptedValues = null, int startRow = 1)
        {
            ExcelValidationResultVM result = new();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage(stream);
            var workbook = package.Workbook;
            var worksheet = workbook.Worksheets.First();
            
            worksheet = TrimWorksheet(worksheet);

            var countRow = worksheet.Dimension.End.Row;
            var countCol = worksheet.Dimension.End.Column;

            var props = typeof(T).GetProperties();
            bool hasError = false;

            List<string> listString = new List<string>();
            //Add values in the cell needs to be checked for duplicates into list string.
            for (var row = startRow; row <= countRow; row++)
            {
                var stringValue = worksheet.Cells[row, 1].Value == null ? "" : worksheet.Cells[row, 1].Value.ToString();
                listString.Add(stringValue);
            }

            var listValueAppearMoreThanOne = ValidDuplicate(listString); // list string with value appears more than 1 time 

            for (var row = startRow; row <= countRow; row++)
            {               
                bool isValidRow = true;
                
                var codeVal = worksheet.Cells[row, 1].Value == null ? "" : worksheet.Cells[row, 1].Value.ToString();
                //If the value appears in listValue then set background is red for that row with error massage
                if (listValueAppearMoreThanOne.ContainsKey(codeVal))
                {
                    worksheet.Row(row).Style.Fill.SetBackground(Color.Red);
                    worksheet.Cells[row, countCol + 1].Value = codeVal + "(Lỗi : Mã trùng lặp)";
                    isValidRow = false;
                    hasError = true;
                    continue;
                }

                for (var col = 1; col <= props.Count(); col++)
                {
                    var prop = props[col - 1];
                    //Just to be clear and easily access the flow, so I don't merge all the validations into one line.
                    var val = worksheet.Cells[row, col].Value == null ? "" : worksheet.Cells[row, col].Value.ToString();
                    
                    var isValidValidation = true;

                    var errorMessage = "";
                    //Try parse first
                    if (!ProcValidationParseValue(val, prop.PropertyType))
                    {
                        isValidValidation = false;
                        errorMessage = " (Lỗi: Giá trị nhập vào không đúng kiểu của dữ liệu)";
                    }
                    //Then data annotation
                    else if (ProcValidationDataAnnotation(typeof(T), prop.Name, val) != null)
                    {
                        var dataAnnotationMessage = ProcValidationDataAnnotation(typeof(T), prop.Name, val);
                        isValidValidation = false;
                        errorMessage = " (Lỗi: " + dataAnnotationMessage + ")";
                    }
                    //Then check accepted values
                    else if (lstAcceptedValues != null)
                    {
                        if (!ProcValidationAcceptedValues(val, prop.Name, lstAcceptedValues))
                        {
                            isValidValidation = false;
                            errorMessage = " (Lỗi: Giá trị nhập vào không hợp lệ)";
                        }
                    }

                    if (!isValidValidation)
                    {
                        worksheet.Cells[row, col].Style.Fill.SetBackground(Color.Red);
                        var colVal = worksheet.Cells[row, col].Value + errorMessage;
                        worksheet.Cells[row, col].Value = colVal;
                        isValidRow = false;
                        hasError = true;
                    }
                  
                }
                if (isValidRow)
                {
                    //for(var col = 1; col <= countCol; col++)
                    //{
                    //    worksheet.Cells[row, col].Style.Fill.SetBackground(Color.LightGreen);
                    //}
                    result.ValidRows.Add(row);                  
                }

            }
            result.IsValid = hasError;
            var fileBytes = package.GetAsByteArray();
            var memoryStream = new MemoryStream(fileBytes);

            result.MemoryStream = memoryStream;
            return result;
        }

        public async Task<MemoryStream> MarkupRows(MemoryStream stream, List<ExcelErrorVM> lstMarkups)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage(stream);
            var workbook = package.Workbook;
            var worksheet = workbook.Worksheets.First();

            worksheet = TrimWorksheet(worksheet);

            var countRow = worksheet.Dimension.End.Row;
            var countCol = worksheet.Dimension.End.Column;

            var props = typeof(T).GetProperties();

            for (var row = 2; row <= countRow; row++)
            {
                var rowMarkup = lstMarkups.FirstOrDefault(c => c.Row == row);
                if (rowMarkup != null)
                {
                    Color rowColor = Color.White;
                    
                    if (rowMarkup.IsSuccess)
                    {
                        rowColor = Color.LightGreen;
                    }
                    else
                    {
                        rowColor = Color.Red;
                    }
                    worksheet.Cells[row, countCol + 1].Value = rowMarkup.Message;
                    worksheet.Cells[row, countCol + 1].Style.Font.Color.SetColor(rowMarkup.IsSuccess ? Color.Green : Color.Red);
                    if (rowMarkup.Col != null)
                    {
                        worksheet.Cells[row, rowMarkup.Col ?? 0].Style.Fill.SetBackground(rowColor);
                    }
                    else
                    {
                        for (var col = 1; col <= countCol; col++)
                        {
                            worksheet.Cells[row, col].Style.Fill.SetBackground(rowColor);
                        }
                    }
                }
            }
            
            var fileBytes = package.GetAsByteArray();
            var memoryStream = new MemoryStream(fileBytes);
            return memoryStream;
        }

        public async Task<MemoryStream> GenerateExcel(List<T> ListObj, string? SheetName = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var pkgExcel = new ExcelPackage();
            var worksheet = pkgExcel.Workbook.Worksheets.Add(SheetName ?? "Sheet1");

            var listObjHeaders = typeof(T).GetProperties().Select(c => c.Name).ToList();
            var jsonArray = JArray.FromObject(ListObj);
            var listExcelHeaders = ListExcelHeaders ?? listObjHeaders;

            var colFirstRow = 1;
            foreach (var i in listExcelHeaders)
            {
                worksheet.Cells[1, colFirstRow].Value = i;
                colFirstRow++;
            }

            var currentRow = 2;
            foreach (JObject obj in jsonArray)
            {
                var colCurrentRow = 1;
                foreach (var prop in listObjHeaders)
                {
                    worksheet.Cells[currentRow, colCurrentRow].Value = (obj.GetValue(prop) ?? "<N/A>").ToString();
                    colCurrentRow++;
                }

                currentRow++;
            }

            var fileBytes = pkgExcel.GetAsByteArray();
            var memoryStream = new MemoryStream(fileBytes);
            return memoryStream;
        }

        private string? ProcValidationDataAnnotation(Type modelType, string propertyName, string propertyValue)
        {
            var propertyInfo = modelType.GetProperty(propertyName);
            if (propertyInfo == null)
                throw new ArgumentException($"The property '{propertyName}' does not exist on type '{modelType.Name}'.");

            var attributes = propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true);
            foreach (var attribute in attributes)
                if (attribute is ValidationAttribute validationAttribute)
                {
                    var parsedValue = Convert.ChangeType(propertyValue, propertyInfo.PropertyType);
                    if (!validationAttribute.IsValid(parsedValue))
                        return validationAttribute.ErrorMessage;
                }
            return null;
        }

        private bool ProcValidationParseValue(string value, Type targetType)
        {
            try
            {
                var tryPar = Convert.ChangeType(value, targetType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ProcValidationAcceptedValues(string value, string name, List<ExcelAcceptedValueVM> listAcpValues)
        {
            var obj = listAcpValues.FirstOrDefault(c => c.ColumnName == name);
            if (obj == null) return true;
            if (obj.MatchCase)
                return obj.AcceptedValues.Any(c => c == value);
            return obj.AcceptedValues.Any(c => c.ToLower() == value.ToLower());
        }

        private ExcelWorksheet TrimWorksheet(ExcelWorksheet worksheet)
        {
            var lastRow = worksheet.Dimension.End.Row;
            var lastColumn = worksheet.Dimension.End.Column;

            // Remove blank rows
            for (var row = lastRow; row >= 1; row--)
            {
                var isRowEmpty = true;

                for (var col = worksheet.Dimension.Start.Column; col <= lastColumn; col++)
                {
                    var cellValue = worksheet.Cells[row, col]?.Value;
                    if (cellValue != null)
                    {
                        isRowEmpty = false;
                        break;
                    }
                }

                if (isRowEmpty) worksheet.DeleteRow(row);
            }

            // Remove blank columns
            for (var column = lastColumn; column >= 1; column--)
            {
                var isColumnEmpty = true;

                for (var row = worksheet.Dimension.Start.Row; row <= lastRow; row++)
                {
                    var cellValue = worksheet.Cells[row, column]?.Value;
                    if (cellValue != null)
                    {
                        isColumnEmpty = false;
                        break;
                    }
                }

                if (isColumnEmpty) worksheet.DeleteColumn(column);
            }

            return worksheet;
        }
        public Dictionary<string, int> ValidDuplicate(List<string> lst)
        {           
            Dictionary<string, int> counts = new Dictionary<string, int>();

            foreach (string number in lst)
            {
                if (counts.ContainsKey(number))
                {
                    counts[number]++;
                }
                else
                {
                    counts[number] = 1;
                }
            }
            return counts.Where(c => c.Value > 1).ToDictionary(c => c.Key, c => c.Value);
        }
    }
}
