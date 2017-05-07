using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;
using BusinessRuleApp_Models.Models;
using System;
using BusinessRuleApp_ErrorHandler;

namespace BusinessRuleApp_DataAccess
{
    public class DataAccessTest
    {
        private IMongoDatabase db;
        private Connections _cnxMongoDB;

        public DataAccessTest()
        {
            _cnxMongoDB = new Connections(1);
            db = _cnxMongoDB._client.GetDatabase("test");
        }

        //Insert data (only one document)
        public async Task insertApplication(BsonDocument docApp)
        {
            var col = db.GetCollection<BsonDocument>("Application");     //<BSonDocument>
            try
            {
                col.InsertOne(docApp);
            }
            catch (Exception e) {
                GenericError.PrintErrorMessages(e);
            }            
        }

        //Insert data (many documents sample)
        public async Task insertManyBusinessRules(BsonDocument docBr1, BsonDocument docBr2)
        {
            var col2 = db.GetCollection<BsonDocument>("BusinessRule");     //<BSonDocument>
            try{
                col2.InsertMany(new[] { docBr1, docBr2 });
            }
            catch (Exception e) {
                GenericError.PrintErrorMessages(e);
            }            
        }

        //Search for all aplications
        public async Task<IMongoCollection<BsonDocument>> getListOfApplications()
        {
            return db.GetCollection<BsonDocument>("Application");    //<BsonDocument>
        }

        //Search for all business rules
        public async Task<IMongoCollection<BsonDocument>> getListOfBusinessRules()
        {
            return db.GetCollection<BsonDocument>("BusinessRule");
        }

        //Search for applications per filter
        public async Task<List<BsonDocument>> getListOfApplicationsByFilter(int filter, List<KeyValuePair<string, string>> filterKeyAndValue)
        {
            IMongoCollection<BsonDocument> col = null;
            FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;
            BsonDocument searchFilter = null;
            List<BsonDocument> list = null;
            col = (filter <= 2) ? db.GetCollection<BsonDocument>("Application") : db.GetCollection<BsonDocument>("BusinessRule");

            try {
                switch (filter)
                {
                    case 1:
                        //Per App name - Mongo DB Equality comparison filter (string)
                        searchFilter = new BsonDocument(filterKeyAndValue[0].Key.ToString(), filterKeyAndValue[0].Value.ToString());
                        list = await col.Find(searchFilter).ToListAsync();
                        break;
                    case 2:
                        //Per App name - Mongo DB Equality comparison filter (Json format)
                        list = await col.Find("{ " + filterKeyAndValue[0].Key.ToString() + ": '" + filterKeyAndValue[0].Value.ToString() + "' }").ToListAsync();
                        break;
                    case 3:
                        //Per Business Rules category - Mongo DB use of complex filters (Larger Than)
                        searchFilter = new BsonDocument(filterKeyAndValue[1].Key.ToString(), new BsonDocument("$lt", int.Parse(filterKeyAndValue[1].Value.ToString())));
                        list = await col.Find(searchFilter).ToListAsync();
                        break;
                    case 4:
                        //Per Business Rules (Name and Category). Use of complex filters with connectors with BsonArray
                        searchFilter = new BsonDocument("$and", new BsonArray {
                        new BsonDocument(filterKeyAndValue[1].Key.ToString(),new BsonDocument("$lt", int.Parse(filterKeyAndValue[1].Value.ToString()))),
                        new BsonDocument(filterKeyAndValue[2].Key.ToString(),filterKeyAndValue[2].Value.ToString())
                        });
                        list = await col.Find(searchFilter).ToListAsync();
                        break;
                    case 5:
                        //Per Business Rules - Using Builders (FilterDefinitionBuilder<BsonDocument>) as another way of Filter
                        list = await col.Find(builder.Lt(filterKeyAndValue[1].Key.ToString(), int.Parse(filterKeyAndValue[1].Value.ToString()))).ToListAsync();
                        break;
                    case 6:
                        //Per Business Rules - Using Builders (FilterDefinitionBuilder<BsonDocument>) as another way of Filter
                        list = await col.Find(
                            builder.And(
                                builder.Lt(filterKeyAndValue[1].Key.ToString(), int.Parse(filterKeyAndValue[1].Value.ToString())),
                                builder.Eq(filterKeyAndValue[2].Key.ToString(), filterKeyAndValue[2].Value.ToString()))
                        ).ToListAsync();
                        break;
                    case 7:
                        //Per Business Rules - Using Builders (FilterDefinitionBuilder<BsonDocument>) as another way of Filter (use #2)
                        list = await col.Find(
                                builder.Lt(filterKeyAndValue[1].Key.ToString(), int.Parse(filterKeyAndValue[1].Value.ToString())) &
                                !builder.Eq(filterKeyAndValue[3].Key.ToString(), filterKeyAndValue[3].Value.ToString())
                        ).ToListAsync();
                        break;
                    case 8:
                        //Show all Business Rules with a projection (only required data)
                        //Where Key: Value = 1 (Will appear) and Key: Value = 0 (Will not appear)
                        list = await col.Find(new BsonDocument()).Project("{brName: 1, _id: 0, brCreationTime: 1}").ToListAsync();
                        break;
                    case 9:
                        //Show all Business Rules with a projection (only required data) using BsonDocument class in the collection
                        //Where Key: Value = 1 (Will appear) and Key: Value = 0 (Will not appear)
                        list = await col.Find(new BsonDocument()).Project(new BsonDocument("brName",1).Add("_id",0).Add("brCreationTime",1))
                            .ToListAsync();
                        break;
                    case 10:
                        //Show all Business Rules with a projection (only required data) using BsonDocument class in the collection
                        //Where Key: Value = 1 (Will appear) and Key: Value = 0 (Will not appear)
                        list = await col.Find(new BsonDocument())
                            .Project(Builders<BsonDocument>.Projection.Include("brName").Exclude("_id").Include("brCreationTime"))
                            .ToListAsync();
                        break;
                }
            }
            catch (Exception e)
            {
                GenericError.PrintErrorMessages(e);
            }
            return list;
        }

        //Search for Business Rules per filter. Returns a List of Business class instead of a dynamic BsonDocument
        public async Task<List<BusinessRule>> getListOfBusinessRulesByFilter(int filter, List<KeyValuePair<string, string>> filterKeyAndValue)
        {
            IMongoCollection<BusinessRule> col = db.GetCollection<BusinessRule>("BusinessRule");
            FilterDefinitionBuilder<BusinessRule> builder = Builders<BusinessRule>.Filter;
            List<BusinessRule> list = null;
            try
            {
                switch (filter)
                {
                    case 1:
                        //Use a complex search that returns a list of Business Rules classes
                        list = await col.Find(
                                builder.Lt(filterKeyAndValue[1].Key.ToString(), int.Parse(filterKeyAndValue[1].Value.ToString())) &
                                !builder.Eq(filterKeyAndValue[3].Key.ToString(), filterKeyAndValue[3].Value.ToString())
                        ).ToListAsync();
                        break;
                    case 2:
                        //Use a complex search with Expression trees (Lambda expressions) + Filter Definition Builder 
                        var searchFilter = builder.Lt(x => x.BrCategoryId, Byte.Parse(filterKeyAndValue[1].Value.ToString())) & !builder.Eq(x => x.BrName, filterKeyAndValue[3].Value.ToString());
                        list = await col.Find(searchFilter).ToListAsync();
                        break;
                    case 3:
                        //Using a reduced but complex search with Expression trees (Lambda expressions only as a collection filter) 
                        list = await col.Find(x => x.BrCategoryId < byte.Parse(filterKeyAndValue[1].Value.ToString()) && x.BrName != filterKeyAndValue[3].Value.ToString()).ToListAsync();
                        break;
                    case 4:
                        //Using complex search with lambda expressions and 1 row limit result 
                        list = await col.Find(x => x.BrCategoryId < byte.Parse(filterKeyAndValue[1].Value.ToString()) && x.BrName != filterKeyAndValue[3].Value.ToString())
                            .Limit(1)
                            .ToListAsync();
                        break;
                    case 5:
                        //Using complex search with lambda expressions, 1 row limit result and skipping the first document
                        //In this case, we will show only the second row of Business Rules, according to the search
                        list = await col.Find(x => x.BrCategoryId < byte.Parse(filterKeyAndValue[1].Value.ToString()) && x.BrName != filterKeyAndValue[3].Value.ToString())
                            .Skip(1)        //Skip should be first to eliminate data before limiting
                            .Limit(1)
                            .ToListAsync();
                        break;
                    case 6:
                        //Using complex search with sort (creation time) and Builder helper for descending date results (10 last results)
                         list = await col.Find(x => x.BrCategoryId < byte.Parse(filterKeyAndValue[1].Value.ToString()) && x.BrName != filterKeyAndValue[3].Value.ToString())
                            .Sort(Builders<BusinessRule>.Sort.Descending("brCreationTime"))        
                            .Limit(10)
                            .ToListAsync();
                        break;
                    case 7:
                        //Using complex search with sort (creation time / name) and Builder helper for descending date results (10 last results)
                        list = await col.Find(x => x.BrCategoryId < byte.Parse(filterKeyAndValue[1].Value.ToString()) && x.BrName != filterKeyAndValue[3].Value.ToString())
                            .Sort(Builders<BusinessRule>.Sort.Descending("brCreationTime").Ascending("brName"))
                            .Limit(10)
                            .ToListAsync();
                        break;
                    case 8:
                        //Using complex search with sort (creation time / name) and Builder helper for descending date results (10 last results)
                        //In this case we sort using expression trees (lambda expressions)
                        list = await col.Find(x => x.BrCategoryId < byte.Parse(filterKeyAndValue[1].Value.ToString()) && x.BrName != filterKeyAndValue[3].Value.ToString())
                            .SortByDescending(x => x.BrCreationTime)
                            .ThenBy(x => x.BrName)                      //Ascending
                            .Limit(10)
                            .ToListAsync();
                        break;
                    case 9:
                        //Show all Business Rules with a projection (only required data) using BsonDocument class in the collection
                        //Important: Projection is adapted to direct BusinessRule class mapping
                        //Check difference between Project and Projection
                        list = await col.Find(new BsonDocument())
                            .Project<BusinessRule>(Builders<BusinessRule>.Projection.Include("brName").Exclude("_id").Include("brCreationTime"))
                            .ToListAsync();
                        break;
                    case 10:
                        //Show all Business Rules with a projection (only required data) using BsonDocument class in the collection
                        //Important: Projection is adapted to direct BusinessRule class mapping
                        //Add into Projection expression trees to delimitate information to show
                        list = await col.Find(new BsonDocument())
                            .Project<BusinessRule>(Builders<BusinessRule>.
                                Projection.Include(x => x.BrName).Exclude(x => x.Id).Include(x => x.BrCreationTime))
                            .ToListAsync();
                        break;
                }
            }
            catch (Exception e) {
                GenericError.PrintErrorMessages(e);
            }
            return list;
        }

        //Search for Business Rules per filter. Returns a List of string instead of a dynamic BsonDocument
        public async Task<List<string>> getListOfBusinessRulesByFilterStrings(int filter, List<KeyValuePair<string, string>> filterKeyAndValue)
        {
            IMongoCollection<BusinessRule> col = db.GetCollection<BusinessRule>("BusinessRule");
            List<String> list = null;
            try
            {
                switch (filter)
                {
                    case 1:
                        //Show all Business Rules with a projection (only required data) using BsonDocument class in the collection
                        //Only Name in String values will be shown, using expression trees
                        //When we use only expression trees is easier to refactor from new classes
                        //The client-side will handle all the expression projections and results. 
                        //Also used for complex calculations the server-side can't handle, such as this expression tree Projection sample: 
                        //Project(x => new {x.brName, NewCategoryId = 'CAT0000' + x.brCategoryId })
                        list = await col.Find(new BsonDocument())
                            .Project(x => x.BrName)
                            .ToListAsync();
                        break;
                }
            }
            catch (Exception e)
            {
                GenericError.PrintErrorMessages(e);
            }
            return list;
        }

    }
}
