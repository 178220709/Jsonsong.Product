using System.Configuration;
using System.Runtime.CompilerServices;
using MongoDB.Driver;

namespace Jsonsong.Dal.Common.MongoDb
{
    public class DataBaseManager
    {
        public const string ConnectionKey = "MongoDB";
        private static string _connectionString = "";

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = GetDefaultConnectionString();
                }
                return _connectionString;
            }
            set { _connectionString = value; }
        }


        internal static IMongoDatabase GetDatabase(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = ConnectionString;
            }

            var mongoUrl = new MongoUrl(connectionString);

            var client = new MongoClient(connectionString);

            return client.GetDatabase(mongoUrl.DatabaseName);
        }


        private static string GetDefaultConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionKey].ConnectionString;
        }
    }
}