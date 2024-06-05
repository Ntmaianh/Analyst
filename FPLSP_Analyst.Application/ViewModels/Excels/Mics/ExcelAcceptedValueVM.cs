namespace FPLSP_Analyst.Application.ViewModels.Excels.Mics
{
    public class ExcelAcceptedValueVM
    {
        public string ColumnName { get; set; } = string.Empty;
        public List<string> AcceptedValues { get; set; } = new();
        public bool MatchCase { get; set; } = false;
    }
}
