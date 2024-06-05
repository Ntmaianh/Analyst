using FPLSP_Analyst.Application.ValueObjects.Common;

namespace FPLSP_Analyst.Application.ValueObjects.Exceptions
{
    public class UnauthorizedException : ExceptionBase
    {
        public UnauthorizedException(string context, string key, string message, Tracker? tracker = null)
            : base(context, key, message, tracker)
        {
        }

        public UnauthorizedException(string context, string key, string message, Exception exception, Tracker? tracker = null)
            : base(context, key, message, exception, tracker)
        {
        }
    }
}
