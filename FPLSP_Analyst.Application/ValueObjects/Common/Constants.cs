namespace FPLSP_Analyst.Application.ValueObjects.Common
{
    public class QueryConstant
    {
        public class OperatorTypes
        {
            public const string GreaterThan = ">";

            public const string GreaterThanOrEqual = ">=";

            public const string Equal = "=";

            public const string LessThan = "<";

            public const string LessThanOrEqual = "<=";

            public const string None = "";
        }

        public class OperatorMatch
        {
            public const string Or = "||";
            public const string And = "&&";
        }

        public class MatchTypes
        {
            public const bool Contain = false;

            public const bool Equal = true;
        }
    }
}
