using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace BusinessRuleApp_DataAccess
{
    public class DataAccessTest
    {
        private IMongoDatabase db;
        private Connections _cnxMongoDB;

        public DataAccessTest() {
            _cnxMongoDB = new Connections(1);
            db = _cnxMongoDB._client.GetDatabase("test");
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

        //Search for all aplications
        public async Task<IMongoCollection<BsonDocument>> getListOfApplications() {
            var col = db.GetCollection<BsonDocument>("Application");    //<BsonDocument>
            return col;
        }

        //Search for all business rules
        public async Task<IMongoCollection<BsonDocument>> getListOfBusinessRules() {
            return db.GetCollection<BsonDocument>("BusinessRule");
        }

    }
}
