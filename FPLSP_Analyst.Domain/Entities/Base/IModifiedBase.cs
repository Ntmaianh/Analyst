namespace FPLSP_Analyst.Domain.Entities.Base
{
    public interface IModifiedBase
    {
        public DateTimeOffset ModifiedTime { get; set; }

        public Guid? ModifiedBy { get; set; }

    }
}
