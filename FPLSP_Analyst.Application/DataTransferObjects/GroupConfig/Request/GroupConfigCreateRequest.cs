namespace FPLSP_Analyst.Application.DataTransferObjects.GroupConfig.Request
{
    public class GroupConfigCreateRequest
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;

        public Dictionary<string, string> ListConfigs { get; set; }
    }
}
