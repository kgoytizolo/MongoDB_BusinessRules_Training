using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        //Search for applications per filter
        public async Task<List<BsonDocument>> getListOfApplicationsByFilter(int filter) {
            IMongoCollection<BsonDocument> col = null;
            BsonDocument searchFilter = null;
            List<BsonDocument> list = null;
            switch (filter) {
                case 1:
                    //Per App name
                    searchFilter = new BsonDocument("ApplicationName","Application 2");
                    col = db.GetCollection<BsonDocument>("Application");
                    list = await col.Find(searchFilter).ToListAsync();
                    break;
                case 2:
                    col = db.GetCollection<BsonDocument>("Application");
                    break;
            }
            return list;
        }

    }
}
