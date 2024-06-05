using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Application.ViewModels.Excels;
using FPLSP_Analyst.Application.ViewModels.Excels.Mics;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Domain.Enums;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;
using FPLSP_Analyst.Infrastructure.Implements.Services.Additional;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System.Collections.Generic;

namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadWrite
{
    public class FileHandlingReadWriteRepository : IFileHandlingReadWriteRepository
    {
        private ILecturerReadWriteRepository _lecturerReadWrite;
        private readonly IMajorReadWriteRepository _majorReadWriteRepository;
        private readonly ISubjectReadWriteRepository _subjectReadWriteRepository;
        private readonly IClassIndicatorReadWriteRepository _classIndicatorReadWriteRepository;
        private readonly ISubjectIndicatorReadWriteRepository _subjectIndicatorReadWriteRepository;
        private readonly ILecturerIndicatorReadWriteRepository _lecturerIndicatorReadWriteRepository;
        private readonly IMajorIndicatorReadWriteRepository _majorIndicatorReadWriteRepository;
        private AppReadOnlyDbContext _dbContext;
        private readonly string _pathFolder;
        private CancellationToken cancellationToken;
        private bool _isError;
        public FileHandlingReadWriteRepository(ILecturerReadWriteRepository lecturerReadWrite,
                                               IMajorReadWriteRepository majorReadWriteRepository,
                                               ISubjectReadWriteRepository subjectReadWriteRepository,
                                               IClassIndicatorReadWriteRepository classIndicatorReadWriteRepository,
                                               ISubjectIndicatorReadWriteRepository subjectIndicatorReadWriteRepository,
                                               ILecturerIndicatorReadWriteRepository lecturerIndicatorReadWriteRepository,
                                               IMajorIndicatorReadWriteRepository majorIndicatorReadWriteRepository
                                               )
        {
            _dbContext = new AppReadOnlyDbContext();
            _pathFolder = Path.Combine(Directory.GetCurrentDirectory(), "SavedFiles");
            _lecturerReadWrite = lecturerReadWrite;
            _majorReadWriteRepository = majorReadWriteRepository;
            _subjectReadWriteRepository = subjectReadWriteRepository;
            _classIndicatorReadWriteRepository = classIndicatorReadWriteRepository;
            _subjectIndicatorReadWriteRepository = subjectIndicatorReadWriteRepository;
            _lecturerIndicatorReadWriteRepository = lecturerIndicatorReadWriteRepository;
            _majorIndicatorReadWriteRepository = majorIndicatorReadWriteRepository;
            _pathFolder = Path.Combine(Directory.GetCurrentDirectory(), "SavedFiles");
            MkdirFunc(_pathFolder);
        }

        private void MkdirFunc(string folderPath)
        {
            List<string> lstFolders = new List<string>()
            {
                "Excels/Handled",
                "Excels/Handling",
                "Images",
            };

            foreach (var i in lstFolders)
            {
                var path = Path.Combine(folderPath, i);
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }

        public async Task<ExcelOutputVM> ExcelImport(ExcelImportInputVM obj)
        {
            //Đổ FileData vào MemoryStream
            MemoryStream memStream = new MemoryStream(obj.FileData);
            var fileSplit = obj.FileName.Split('.');
            var fileExtension = "." + fileSplit[fileSplit.Count() - 1];

            switch (obj.Function)
            {
                case "MajorIndicator":
                    {
                        var fileName1 = await ImportMajor(memStream, fileExtension, obj.Parameters);
                        var fileName2 = await ImportMajorIndicator(memStream, fileExtension, obj.Parameters);

                        return new()
                        {

                            IsSuccess = true,
                            FileName = fileName2,
                        };
                    }
                case "LecturerIndicator":
                    {
                        var fileName = await ImportLecturer(memStream, fileExtension, obj.Parameters);

                        return new()
                        {
                            IsSuccess = true,
                            FileName = fileName,
                            IsError = _isError
                        };
                    }
                case "SubjectIndicator":
                    {
                        var fileName1 = await ImportSubject(memStream, fileExtension, obj.Parameters);
                        var fileName2 = await ImportSubjectIndicator(memStream, fileExtension, obj.Parameters);

                        return new()
                        {
                            IsSuccess = true,
                            FileName = fileName2,
                        };
                    }
                case "ClassIndicator":
                    {
                        var fileName = await ImportClassIndicator(memStream, fileExtension, obj.Parameters);
                        return new()
                        {
                            IsSuccess = true,
                            FileName = fileName,
                            IsError = _isError,
                        };
                    }
            }
            return new() { IsSuccess = false };
        }
        public async Task<ExcelOutputVM> ExcelExport(ExcelExportVM obj)
        {

            switch (obj.Function)
            {
                case "SubjectIndicator":
                    {
                        var fileName = await ExportSubjectIndicator(obj);
                        return new()
                        {
                            IsSuccess = true,
                            FileName = fileName,
                        };
                    }
                case "ClassIndicator":
                    {
                        var fileName = await ExportClassIndicator(obj);
                        return new()
                        {
                            IsSuccess = true,
                            FileName = fileName,
                        };
                    }
                case "LectureIndicator":
                    {
                        var fileName = await ExportLecturerIndicator(obj);
                        return new()
                        {
                            IsSuccess = true,
                            FileName = fileName,
                        };
                    }
            }
            return new() { IsSuccess = false };
        }

        #region crud file
        public List<string> RemoveAllFileFromFolder(string relativeFolderPath)
        {
            List<string> listDeletedFiles = new List<string>();
            string filePath = Path.Combine(_pathFolder, relativeFolderPath);
            var fileList = Directory.GetFiles(filePath).Where(c => !c.Contains("dummyfile")).ToList();
            foreach (var file in fileList)
            {
                if (File.Exists(filePath))
                {
                    listDeletedFiles.Add(filePath);
                    File.Delete(filePath);
                }
            }
            return listDeletedFiles;
        }
        public bool RemoveFile(string relativeFilePath)
        {
            string filePath = Path.Combine(_pathFolder, relativeFilePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<string> SaveFileAsync(IFormFile file, string folder)
        {
            var fileSplit = file.FileName.Split('.');
            var fileExtension = "." + fileSplit[fileSplit.Count() - 1];

            var fileName = Guid.NewGuid() + fileExtension;

            var filePath = Path.Combine(_pathFolder, folder, fileName);
            var stream = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(stream);
            stream.Close();
            return fileName;
        }
        public string SaveFile(MemoryStream memoryStream, string fileExtension, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + fileExtension;

            var filePath = Path.Combine(_pathFolder, folder, fileName);
            var stream = new FileStream(filePath, FileMode.Create);
            memoryStream.Seek(0, SeekOrigin.Begin);
            memoryStream.CopyTo(stream);
            stream.Close();

            return fileName;
        }

        #endregion
        #region Import file excel

        #region Indicator
        private async Task<string> ImportLecturer(MemoryStream stream, string fileExtension, List<ExcelParameterVM> lstParams)
        {
            var trainingFacility = _dbContext.TrainingFacilities.Count();
            if (trainingFacility != 0)
            {
                var currentSemester = await _dbContext.Semesters.AsNoTracking().Where(c=>c.Status == EntityStatus.Active && !c.Deleted).FirstOrDefaultAsync(cancellationToken);

                if (currentSemester != null)
                {
                    if (currentSemester.StartTime < DateTimeOffset.Now && currentSemester.EndTime > DateTimeOffset.Now)
                    {
                        //Khởi tạo 1 ExcelServices với tên các cột trong file Excel
                        ExcelServices<ExcelLecturerIndicatorVM> _svExcel = new();
                        // Gọi validate giá trị
                        var validateResult = await _svExcel.Validate(stream, null, 4);
                        stream = validateResult.MemoryStream!;
                        //CRUD + Validate với dữ liệu trong DB
                        List<ExcelLecturerIndicatorVM> lstExcelVM = await _svExcel.GetValueFromFileStream(stream, validateResult.ValidRows);
                        List<LecturerIndicatorEntity> lstObjIn = lstExcelVM.Select(c => c.ConvertToIndicatorEntity()).ToList();
                        List<LecturerEntity> lstObj = lstExcelVM.Select(c => c.ConvertToEntity()).ToList();
                        List<LecturerIndicatorEntity> lstLecturerIndicator = new();
                        List<LecturerEntity> lstLecturer = new();
                        List<ExcelErrorVM> lstMarkup = new();

                        int length = lstObj.Count;

                        for (int i = 0; i < length; i++)
                        {
                            if (lstObj[i] == null || lstObjIn[i] == null)
                            {
                                lstMarkup.Add(new() { Row = validateResult.ValidRows[i], Message = "Sai kiểu dữ liệu", IsSuccess = false });
                            }
                            else
                            {
                                var majorId = Guid.Empty;
                                try
                                {
                                    string bmCode = lstExcelVM[i].Major;
                                    majorId = _dbContext.Majors.Where(x => x.Code == bmCode).Select(x => x.Id).FirstOrDefault();
                                    if (!_dbContext.Lecturers.AsNoTracking().AsQueryable().Any(c => c.Username.ToLower() == lstObj[i].Username.ToLower() && c.Status != EntityStatus.Deleted))
                                    {

                                        if (majorId == Guid.Empty)
                                        {
                                            lstMarkup.Add(new() { Row = validateResult.ValidRows[i], Message = "Ngành không tồn tại", IsSuccess = false });
                                        }
                                        else
                                        {
                                            lstObj[i].MajorId = majorId;

                                            lstObj[i].TrainingFacilityId = _dbContext.TrainingFacilities.Select(x => x.Id).FirstOrDefault();

                                            if (!lstLecturer.Any(c => c.Username.ToLower() == lstObj[i].Username.ToLower() && c.Status != EntityStatus.Deleted))
                                                lstLecturer.Add(lstObj[i]);
                                        }
                                    }

                                    if (majorId != Guid.Empty)
                                    {
                                        lstObjIn[i].SemesterId = currentSemester.Id;
                                        string lecturerCode = lstExcelVM[i].Username;
                                        lstObjIn[i].LecturerId = _dbContext.Lecturers.Where(x => x.Username == lecturerCode).Select(x => x.Id).FirstOrDefault();

                                        lstLecturerIndicator.Add(lstObjIn[i]);
                                        lstMarkup.Add(new() { Row = validateResult.ValidRows[i], Message = "Thêm thành công", IsSuccess = true });
                                    }
                                }
                                catch
                                {
                                    lstMarkup.Add(new() { Row = validateResult.ValidRows[i], Message = "Thêm dữ liệu thất bại (Lỗi hệ thống hoặc dữ liệu)", IsSuccess = false });
                                }
                            }
                        }
                        var lecturer = await _lecturerReadWrite.CreateRangeLecturerAsync(lstLecturer, cancellationToken);
                        if (lecturer.Success)
                        {
                            var lecturerIndicator = await _lecturerIndicatorReadWriteRepository.CreateRangeLecturerIndicatorAsync(lstLecturerIndicator, cancellationToken);
                            if (!lecturerIndicator.Success)
                            {
                                lstMarkup.Where(x => x.IsSuccess == true).ToList().ForEach(x => { x.IsSuccess = false; x.Message = "Thêm dữ liệu thất bại (Lỗi hệ thống hoặc dữ liệu)"; });
                            }
                        }
                        else
                        {
                            lstMarkup.Where(x => x.IsSuccess == true).ToList().ForEach(x => { x.IsSuccess = false; x.Message = "Thêm dữ liệu thất bại (Lỗi hệ thống hoặc dữ liệu)"; });
                        }

                        //Gọi phương thức đánh dấu row
                        var markedUpFile = await _svExcel.MarkupRows(stream, lstMarkup);
                        bool isError = lstMarkup.Any(x => x.IsSuccess == false);
                        if (isError || validateResult.IsValid == true)
                        {
                            _isError = true;
                        }
                        else
                            _isError = false;
                        return SaveFile(markedUpFile, fileExtension, "Excels/Handled");
                    }
                    _isError = true;
                    return "Thời gian nằm ngoài phạm vi kỳ học hiện tại.";
                }
                _isError = true;
                return "Không có kỳ học hợp lệ.";
            }

            return "Không có cơ sở hợp lệ.";
        }
        private async Task<string> ImportClassIndicator(MemoryStream stream, string fileExtension, List<ExcelParameterVM> lstParams)
        {
            var currentSemester = await _dbContext.Semesters.AsNoTracking().Where(c=>c.Status == EntityStatus.Active && !c.Deleted).FirstOrDefaultAsync(cancellationToken);
            if (currentSemester != null)
            {
                if (currentSemester.StartTime< DateTimeOffset.Now && currentSemester.EndTime> DateTimeOffset.Now)
                {
                    //Validate giá trị cơ bản
                    //Khởi tạo 1 ExcelServices với tên các cột trong file Excel
                    ExcelServices<ExcelClassIndicatorVM> _svExcel = new();
                    // Gọi validate giá trị
                    var validateResult = await _svExcel.Validate(stream, null, 2);
                    stream = validateResult.MemoryStream!;
                    //CRUD + Validate với dữ liệu trong DB
                    List<ExcelClassIndicatorVM> lstExcelVM = await _svExcel.GetValueFromFileStream(stream, validateResult.ValidRows);
                    List<ClassIndicatorEntity> lstObj = lstExcelVM.Select(c => c.ConvertToEntity()).ToList();

                    List<ExcelErrorVM> lstMarkup = new();
                    List<ClassIndicatorEntity> lstClass = new();

                    int length = lstObj.Count;

                    foreach (var i in lstObj)
                    {
                        if (i == null)
                        {
                            lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Sai kiểu dữ liệu", IsSuccess = false });
                        }
                        else
                        {
                            var subjectId = Guid.Empty;
                            var lecturerId = Guid.Empty;
                            try
                            {
                                string subjectCode = lstExcelVM[lstObj.IndexOf(i)].SubjectCode;
                                subjectId = _dbContext.Subjects.Where(x => x.Code == subjectCode).Select(x => x.Id).FirstOrDefault();

                                if (subjectId == Guid.Empty)
                                {
                                    lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Môn không tồn tại", IsSuccess = false });
                                }
                                else
                                {
                                    i.SemesterId = currentSemester.Id;
                                    i.SubjectId = subjectId;

                                    string lecturerCode = lstExcelVM[lstObj.IndexOf(i)].Lecturer;
                                    lecturerId = _dbContext.Lecturers.Where(x => x.Username.ToLower() == lecturerCode.ToLower()).Select(x => x.Id).FirstOrDefault();
                                    if (lecturerId != Guid.Empty)
                                    {
                                        i.LecturerId = lecturerId;
                                    }
                                    else
                                    {
                                        lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Giảng viên không tồn tại", IsSuccess = false });

                                    }
                                }
                                if (subjectId != Guid.Empty && lecturerId != Guid.Empty)
                                {

                                    lstClass.Add(i);
                                    lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Thêm thành công", IsSuccess = true });

                                }
                            }
                            catch
                            {
                                lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Thêm dữ liệu thất bại (Lỗi hệ thống hoặc dữ liệu)", IsSuccess = false });
                            }
                        }
                    }
                    var classIndicator = await _classIndicatorReadWriteRepository.CreateRangeClassIndicatorAsync(lstClass, cancellationToken);
                    if (!classIndicator.Success)
                    {
                        lstMarkup.Where(x => x.IsSuccess == true).ToList().ForEach(x => { x.IsSuccess = false; x.Message = "Thêm dữ liệu thất bại (Lỗi hệ thống hoặc dữ liệu)"; });
                    }
                    //Gọi phương thức đánh dấu row
                    var markedUpFile = await _svExcel.MarkupRows(stream, lstMarkup);
                    //kiểm tra nếu lstMarkup có dòng đỏ -> báo đỏ lên view 
                    bool isError = lstMarkup.Any(x => x.IsSuccess == false);
                    if (isError || validateResult.IsValid == true)
                    {
                        _isError = true;
                    }
                    else
                        _isError = false;
                    return SaveFile(markedUpFile, fileExtension, "Excels/Handled");
                }
                _isError = true;
                return "Thời gian nằm ngoài phạm vi kỳ học hiện tại.";
            }
            _isError = true;
            return "Không có kỳ học hợp lệ.";

        }
        private async Task<string> ImportSubjectIndicator(MemoryStream stream, string fileExtension, List<ExcelParameterVM> lstParams)
        {
            var currentSemester = await _dbContext.Semesters.AsNoTracking().Where(c => c.StartTime < DateTimeOffset.Now && DateTimeOffset.Now < c.EndTime && c.Status == EntityStatus.Active && !c.Deleted).FirstOrDefaultAsync(cancellationToken);

            //Validate giá trị cơ bản
            //Khởi tạo 1 ExcelServices với tên các cột trong file Excel
            ExcelServices<ExcelSubjectIndicatorVM> _svExcel = new();
            // Gọi validate giá trị
            var validateResult = await _svExcel.Validate(stream, null, 2);
            stream = validateResult.MemoryStream!;
            //CRUD + Validate với dữ liệu trong DB
            List<ExcelSubjectIndicatorVM> lstExcelVM = await _svExcel.GetValueFromFileStream(stream, validateResult.ValidRows);
            List<SubjectIndicatorEntity> lstObj = lstExcelVM.Select(c => c.ConvertToEntity()).ToList();
            List<ExcelErrorVM> lstMarkup = new();

            List<SubjectIndicatorEntity> lstSubject = new();
            foreach (var i in lstObj)
            {
                try
                {
                    var classIdParam = lstParams.FirstOrDefault(c => c.Name == "SubjectIndicatorId");
                    if (classIdParam != null)
                    {
                        i.Id = Guid.Parse(classIdParam.Value);
                    }
                    if (_dbContext.ClassIndicators.AsNoTracking().AsQueryable().Any(c => c.Id == i.Id && c.Status != EntityStatus.Deleted))
                    {
                        lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Mã đã tồn tại", IsSuccess = false, Col = 2 });
                    }
                    else
                    {

                        string SemesterCode = lstExcelVM[lstObj.IndexOf(i)].Semester;
                        i.SemesterId = _dbContext.Semesters.Where(x => x.Code == SemesterCode).Select(x => x.Id).FirstOrDefault();

                        string SubjectCode = lstExcelVM[lstObj.IndexOf(i)].Code;
                        i.SubjectId = _dbContext.Subjects.Where(x => x.Code == SubjectCode).Select(x => x.Id).FirstOrDefault();

                        i.SemesterId = currentSemester.Id;

                        lstSubject.Add(i);
                        lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Thêm thành công", IsSuccess = true });
                    }
                }
                catch
                {
                    lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Thêm dữ liệu thất bại (Lỗi hệ thống hoặc dữ liệu)", IsSuccess = false });
                }
            }
            await _subjectIndicatorReadWriteRepository.CreateRangeSubjectIndicatorAsync(lstSubject, cancellationToken);
            //Gọi phương thức đánh dấu row
            var markedUpFile = await _svExcel.MarkupRows(stream, lstMarkup);

            return SaveFile(markedUpFile, fileExtension, "Excels/Handled");

        }

        private async Task<string> ImportMajorIndicator(MemoryStream stream, string fileExtension, List<ExcelParameterVM> lstParams)
        {
            var currentSemester = await _dbContext.Semesters.AsNoTracking().Where(c => c.StartTime < DateTimeOffset.Now && DateTimeOffset.Now < c.EndTime && c.Status == EntityStatus.Active && !c.Deleted).FirstOrDefaultAsync(cancellationToken);

            //Validate giá trị cơ bản
            //Khởi tạo 1 ExcelServices với tên các cột trong file Excel
            ExcelServices<ExcelMajorIndicatorVM> _svExcel = new();
            // Gọi validate giá trị
            var validateResult = await _svExcel.Validate(stream, null, 3);
            stream = validateResult.MemoryStream!;
            //CRUD + Validate với dữ liệu trong DB
            List<ExcelMajorIndicatorVM> lstExcelVM = await _svExcel.GetValueFromFileStream(stream, validateResult.ValidRows);
            List<MajorIndicatorEntity> lstObj = lstExcelVM.Select(c => c.ConvertToEntity()).ToList();
            List<ExcelErrorVM> lstMarkup = new();

            List<MajorIndicatorEntity> lstMajorIndicator = new();
            foreach (var i in lstObj)
            {
                try
                {
                    var classIdParam = lstParams.FirstOrDefault(c => c.Name == "MajorIndicatorId");
                    if (classIdParam != null)
                    {
                        i.Id = Guid.Parse(classIdParam.Value);
                    }
                    if (_dbContext.MajorIndicators.AsNoTracking().AsQueryable().Any(c => c.Id == i.Id && c.Status != EntityStatus.Deleted))
                    {
                        lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Mã đã tồn tại", IsSuccess = false, Col = 2 });
                    }
                    else
                    {
                        string SemesterCode = lstExcelVM[lstObj.IndexOf(i)].Semester;
                        i.SemesterId = _dbContext.Semesters.Where(x => x.Code == SemesterCode).Select(x => x.Id).FirstOrDefault();
                        string MajorCode = lstExcelVM[lstObj.IndexOf(i)].MajorName;
                        i.MajorId = _dbContext.Majors.Where(x => x.Code == MajorCode).Select(x => x.Id).FirstOrDefault();

                        i.SemesterId = currentSemester.Id;

                        lstMajorIndicator.Add(i);
                        lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Thêm thành công", IsSuccess = true });
                    }

                }
                catch
                {
                    lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Thêm dữ liệu thất bại (Lỗi hệ thống hoặc dữ liệu)", IsSuccess = false });
                }
            }
            await _majorIndicatorReadWriteRepository.CreateRangeMajorIndicatorAsync(lstMajorIndicator, cancellationToken);
            //Gọi phương thức đánh dấu row
            var markedUpFile = await _svExcel.MarkupRows(stream, lstMarkup);

            return SaveFile(markedUpFile, fileExtension, "Excels/Handled");

        }


        private async Task<string> ImportMajor(MemoryStream stream, string fileExtension, List<ExcelParameterVM> lstParams)
        {
            //Validate giá trị cơ bản
            //Khởi tạo 1 ExcelServices với tên các cột trong file Excel
            ExcelServices<ExcelMajorVM> _svExcel = new();
            // Gọi validate giá trị
            var validateResult = await _svExcel.Validate(stream, null, 3);
            stream = validateResult.MemoryStream!;
            //CRUD + Validate với dữ liệu trong DB
            List<ExcelMajorVM> lstExcelVM = await _svExcel.GetValueFromFileStream(stream, validateResult.ValidRows);
            List<MajorEntity> lstObj = lstExcelVM.Select(c => c.ConvertToEntity()).ToList();
            List<ExcelErrorVM> lstMarkup = new();

            List<MajorEntity> lstMajor = new();
            foreach (var i in lstObj)
            {
                try
                {
                    var classIdParam = lstParams.FirstOrDefault(c => c.Name == "MajorId");
                    if (classIdParam != null)
                    {
                        i.Id = Guid.Parse(classIdParam.Value);
                    }
                    if (_dbContext.Majors.AsNoTracking().AsQueryable().Any(c => c.Id == i.Id && c.Status != EntityStatus.Deleted))
                    {
                        lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Mã đã tồn tại", IsSuccess = false, Col = 2 });
                    }
                    else
                    {
                        lstMajor.Add(i);
                        lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Thêm thành công", IsSuccess = true });
                    }
                }
                catch
                {
                    lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Thêm dữ liệu thất bại (Lỗi hệ thống hoặc dữ liệu)", IsSuccess = false });
                }
            }
            await _majorReadWriteRepository.CreateRangeMajorAsync(lstMajor, cancellationToken);
            //Gọi phương thức đánh dấu row
            var markedUpFile = await _svExcel.MarkupRows(stream, lstMarkup);

            return SaveFile(markedUpFile, fileExtension, "Excels/Handled");

        }
        private async Task<string> ImportSubject(MemoryStream stream, string fileExtension, List<ExcelParameterVM> lstParams)
        {
            //Validate giá trị cơ bản
            //Khởi tạo 1 ExcelServices với tên các cột trong file Excel
            ExcelServices<ExcelSubjectVM> _svExcel = new();
            // Gọi validate giá trị
            var validateResult = await _svExcel.Validate(stream, null, 2);
            stream = validateResult.MemoryStream!;
            //CRUD + Validate với dữ liệu trong DB
            List<ExcelSubjectVM> lstExcelVM = await _svExcel.GetValueFromFileStream(stream, validateResult.ValidRows);
            List<SubjectEntity> lstObj = lstExcelVM.Select(c => c.ConvertToEntity()).ToList();
            List<ExcelErrorVM> lstMarkup = new();
            List<SubjectEntity> lstSubject = new List<SubjectEntity>();
            foreach (var i in lstObj)
            {
                try
                {
                    var classIdParam = lstParams.FirstOrDefault(c => c.Name == "SubjectId");
                    if (classIdParam != null)
                    {
                        i.Id = Guid.Parse(classIdParam.Value);
                    }
                    if (_dbContext.Subjects.AsNoTracking().AsQueryable().Any(c => c.Id == i.Id && c.Status != EntityStatus.Deleted))
                    {
                        lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Mã đã tồn tại", IsSuccess = false, Col = 2 });
                    }
                    else
                    {
                        string MajorCode = lstExcelVM[lstObj.IndexOf(i)].MajorName;
                        i.MajorId = _dbContext.Majors.Where(x => x.Code == MajorCode).Select(x => x.Id).FirstOrDefault();
                        lstSubject.Add(i);
                        lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Thêm thành công", IsSuccess = true });
                    }
                }
                catch
                {
                    lstMarkup.Add(new() { Row = validateResult.ValidRows[lstObj.IndexOf(i)], Message = "Thêm dữ liệu thất bại (Lỗi hệ thống hoặc dữ liệu)", IsSuccess = false });
                }
            }
            await _subjectReadWriteRepository.CreateRangeSubjectAsync(lstSubject, cancellationToken);
            //Gọi phương thức đánh dấu row
            var markedUpFile = await _svExcel.MarkupRows(stream, lstMarkup);

            return SaveFile(markedUpFile, fileExtension, "Excels/Handled");

        }

        #endregion
        #endregion

        #region Export File excel
        private async Task<string> ExportClassIndicator(ExcelExportVM obj)
        {
            List<ClassIndicatorEntity> lstResult = new();
            if (!obj.IsTemplate)
            {
                var stringClassIndicatorId = obj.Parameters.FirstOrDefault(c => c.Name == "ClassIndicatorId");
                Guid? guidMajorId = stringClassIndicatorId == null ? null : Guid.Parse(stringClassIndicatorId.Value);
                lstResult = _dbContext.ClassIndicators.AsNoTracking().AsQueryable().Where(c => c.Id == guidMajorId && c.Status != 0).ToList();
            }
            ExcelServices<ExcelClassIndicatorVM> svExcel = new();
            var fileStream = await svExcel.GenerateExcel(lstResult.Select(c =>
            {
                return new ExcelClassIndicatorVM
                {
                    StudentTotalNumber = c.StudentTotalNumber,
                    StudentPassedNumber = c.StudentPassedNumber,
                    StudentBannedNumber = c.StudentBannedNumber,
                    StudentBannedPercent = c.StudentBannedPercent.ToString(),
                    StudentFailedNumber = c.StudentFailedNumber,
                    StudentFailedPercent = c.StudentFailedPercent.ToString(),
                    StudentPassedPercent = c.StudentPassedPercent.ToString()
                };
            }).ToList());
            return SaveFile(fileStream, ".xlsx", "Excels/Handled");
        }

        private async Task<string> ExportSubjectIndicator(ExcelExportVM obj)
        {
            List<SubjectIndicatorEntity> lstResult = new();
            if (!obj.IsTemplate)
            {
                var stringSubjectIndicatorId = obj.Parameters.FirstOrDefault(c => c.Name == "SubjectIndicatorId");
                Guid? guidSubjectIndicatorId = stringSubjectIndicatorId == null ? null : Guid.Parse(stringSubjectIndicatorId.Value);
                lstResult = _dbContext.SubjectIndicators.AsNoTracking().AsQueryable().Where(c => c.Id == guidSubjectIndicatorId && c.Status != 0).ToList();
            }
            ExcelServices<ExcelSubjectIndicatorVM> svExcel = new(new() {  "Mã","Tên Môn", "Loại hình đào tạo", "Bộ Môn", "Tổng sổ",
                "Pass","% Pass","Cấm thi" , "% Cấm thi" , "Fail" , "% Fail" ,"Số sinh viên không đi thi", "Cần giải trình" , "Gỉai trình", "Kì học" });
            var fileStream = await svExcel.GenerateExcel(lstResult.Select(c =>
            {
                return new ExcelSubjectIndicatorVM
                {
                    StudentTotalNumber = c.StudentTotalNumber,
                    StudentPassedNumber = c.StudentPassedNumber,
                    StudentBannedNumber = c.StudentBannedNumber,
                    StudentBannedPercent = c.StudentBannedPercent.ToString(),
                    StudentFailedNumber = c.StudentFailedNumber,
                    StudentFailedPercent = c.StudentFailedPercent.ToString(),
                    StudentPassedPercent = c.StudentPassedPercent.ToString()
                };
            }).ToList());
            return SaveFile(fileStream, ".xlsx", "Excels/Handled");
        }

        private async Task<string> ExportLecturerIndicator(ExcelExportVM obj)
        {
            List<LecturerIndicatorEntity> lstResult = new();
            if (!obj.IsTemplate)
            {
                var stringLectureIndicatorId = obj.Parameters.FirstOrDefault(c => c.Name == "SubjectIndicatorId");
                Guid? guidLectureIndicatorId = stringLectureIndicatorId == null ? null : Guid.Parse(stringLectureIndicatorId.Value);
                lstResult = _dbContext.LecturerIndicators.AsNoTracking().AsQueryable().Where(c => c.Id == guidLectureIndicatorId && c.Status != 0).ToList();
            }
            ExcelServices<ExcelLecturerIndicatorVM> svExcel = new(new() {  "LớpMôn", "Lớp", "Mã Môn", "Loại hình đào tạo", "Giảng Viên", "Bộ Môn", "Tổng sổ",
                "Pass","% Pass","Cấm thi" , "% Cấm thi" , "Fail" , "% Fail" , "Cần giải trình" , "Gỉai trình", "Kì học" });
            var fileStream = await svExcel.GenerateExcel(lstResult.Select(c =>
            {
                return new ExcelLecturerIndicatorVM
                {
                    StudentTotalNumber = c.StudentTotalNumber,
                    StudentPassedNumber = c.StudentPassedNumber,
                    StudentBannedNumber = c.StudentBannedNumber,
                    StudentBannedPercent = c.StudentBannedPercent.ToString(),
                    StudentFailedNumber = c.StudentFailedNumber,
                    StudentFailedPercent = c.StudentFailedPercent.ToString(),
                    StudentPassedPercent = c.StudentPassedPercent.ToString()
                };
            }).ToList());
            return SaveFile(fileStream, ".xlsx", "Excels/Handled");
        }
        #endregion

    }
}

