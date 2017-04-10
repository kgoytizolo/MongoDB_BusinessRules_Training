using MongoDB.Driver;
using System.Threading.Tasks;
using BusinessRuleApp_Models.Models;

namespace BusinessRuleApp_DataAccess
{
    public class Connections
    {
        public Connections(byte cnxSetup) {
            switch (cnxSetup) {
                case 1:
                    MainMongoDBConnection();
                    break;
                default:
                    MainDBConnection();
                    break;
            }
        }

        public async Task MainMongoDBConnection()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<Application>("application");     //<BSonDocument>
        }

        public int MainDBConnection() {
            return 0;
        }

    }
}