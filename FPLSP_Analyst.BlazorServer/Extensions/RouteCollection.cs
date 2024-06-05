namespace FPLSP_Analyst.BlazorServer.Extensions
{
    public class RouteManager
    {
        public class Home
        {
            public static string AdminHome = "/home";
        }

        public class Profile
        {
            public static string UserProfile = "/profile";
        }

        public class GroupConfig
        {
            public static string List = "/group-config/list";
        }

        public class Semester
        {
            public static string List = "/semester/list";
        }

        public class Major
        {
            public static string List = "/major/list";
            public static string Indicator = "/major/indicator";
            public static string Details = "/major/details";
        }

        public class Subject
        {
            public static string List = "/subject/list";
            public static string Indicator = "/subject/indicator";
            public static string Details = "/subject/details";
        }

        public class Lecturer
        {
            public static string List = "/lecturer/list";
            public static string Indicator = "/lecturer/indicator";
            public static string Details = "/lecturer/details";
        }

        public class Class
        {
            public static string List = "/class/list";
            public static string Indicator = "/class/indicator";
            public static string Details = "/class/details";
        }

        public class Statistic
        {
            public static string GeneralStatistic = "/general-statistic";
            public static string MajorOverview = "/major-overview";
            public static string MajorStatistic = "/major-statistic";
        }
    }
}
