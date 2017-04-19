using BusinessRuleApp_Models.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace BusinessRuleApp_DataAccess
{
    public class DataAccessTest
    {
        private IMongoDatabase db;

        public DataAccessTest(MongoClient client) {
            db = client.GetDatabase("test");
        }

        //Insert data (only one document)
        public async Task insertApplication(BsonDocument docApp) {
            var col = db.GetCollection<BsonDocument>("Application");     //<BSonDocument>
            col.InsertOne(docApp);
        }

        //Insert data (many documents sample)
        public async Task insertManyBusinessRules(BsonDocument docBr1, BsonDocument docBr2) {
            var col2 = db.GetCollection<BsonDocument>("BusinessRule");     //<BSonDocument>
            col2.InsertMany(new[] { docBr1, docBr2 });
        }

    }
}
