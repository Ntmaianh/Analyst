using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Infrastructure.Extensions
{
    public class EntityStatusExtensions
    {

        public string ConvertForGeneral(EntityStatus status)
        {
            switch (status)
            {
                case EntityStatus.Active:
                    return "Đang hoạt động";
                case EntityStatus.InActive:
                    return "Ngừng hoạt động";
                case EntityStatus.Deleted:
                    return "Đã xóa";
                case EntityStatus.Pending:
                    return "Đang chờ";
                case EntityStatus.Locked:
                    return "Đã khóa";
            }

            return "N/A";
        }

        public string ConvertForExample(EntityStatus status)
        {
            switch (status)
            {
                case EntityStatus.Active:
                    return "Đang mở";
                case EntityStatus.InActive:
                    return "Đang đóng";
                case EntityStatus.Deleted:
                    return "Đã xóa";
                case EntityStatus.Pending:
                    return "Đang chờ";
                case EntityStatus.Locked:
                    return "Đã khóa";
            }
            return "N/A";
        }
    }
}
