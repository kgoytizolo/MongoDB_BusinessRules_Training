using MongoDB.Driver;
using System.Threading.Tasks;
using BusinessRuleApp_Models.Models;

namespace BusinessRuleApp_DataAccess
{
    public class Connections
    {
        public MongoClient _client;

        public Connections(byte cnxSetup) {
            switch (cnxSetup) {
                case 1:
                    MainMongoDBConnection();
                    break;
                default:
                    MainDBSQLConnection();
                    break;
            }
        }

        public async Task MainMongoDBConnection()
        {
            var connectionString = "mongodb://localhost:27017";
            _client = new MongoClient(connectionString);
            var db = _client.GetDatabase("test");
        }

        public int MainDBSQLConnection() {
            return 0;
        }

    }
}