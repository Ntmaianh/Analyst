using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Domain.Entities.Base
{
    public class EntityBase : ICreatedBase, IModifiedBase, IDeletedBase
    {
        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public DateTimeOffset CreatedTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
    }
}
