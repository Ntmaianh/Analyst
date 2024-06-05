namespace FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility.Request
{
    public class TrainingFacilityUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

    }
}
