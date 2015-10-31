using System.Configuration;

namespace leishi.Common
{
    public static class GlobalConfiguration
    {
        public static readonly string DataBaseConnectionString;

        static GlobalConfiguration()
        {
            DataBaseConnectionString = ConfigurationManager.ConnectionStrings["leishiConnectionString"].ConnectionString;
        }
    }
}
