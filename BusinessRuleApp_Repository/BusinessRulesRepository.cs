using MongoDB.Bson;
using BusinessRuleApp_Models.Models;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System.Threading.Tasks;
using BusinessRuleApp_DataAccess;
using MongoDB.Driver;
using BusinessRuleApp_Repository.Interfaces;

namespace BusinessRuleApp_Repository
{
    public class BusinessRulesRepository : IBusinessRulesRepository
    {
        private DataAccessTest _daTest;

        public BusinessRulesRepository() {
            if(_daTest == null) _daTest = new DataAccessTest();
        }

        //Getting Business Rules From MongoDB directly (BSonDocument)  
        public BsonDocument getBusinessRules()
        {
            var docBr = new BsonDocument
            {
                { "BrName", "Business Rule 1" }                               //Creating a new business rule from scratch
            };
            docBr["BrDescription"] = "This is a business rule blah blah";     //Adding another row ([key] = value)
            docBr.Add("BrTypeId", 1);                                         //Adding a new row (key, value)
            docBr.Add("BrCategoryId", 1);                                     //Adding a new row (key, value)
            docBr.Add("BrUserCreation", 1);                                   //Adding a new row (key, value)
            docBr["BrCreationTime"] = System.DateTime.Now;                    //Adding another row ([key] = value)

            //Object's arrays:
            var nestedBrPerAppArray = new BsonArray();
            nestedBrPerAppArray.Add(new BsonDocument("ApplicationId", "1"));    //Creating an array
            docBr.Add("ApplicationsPerBusinessRules", nestedBrPerAppArray);     //Adding an array to the business rules

            return docBr;
        }

        //Insert business rules samples (InsertMany)
        public async Task InsertManyBusinessRules(BsonDocument brSample1, BsonDocument brSample2) {
            await _daTest.insertManyBusinessRules(brSample1, brSample2);
        }

        //Search and list all the Business Rules
        public async Task<IMongoCollection<BsonDocument>> GetListOfBusinessRulesFromDb() {
            return await _daTest.getListOfBusinessRules();
        }

        //Getting Business Rules using POCO Representation - From C# class to BsonDocument mapping
        public BusinessRule GetBusinessRulesForMapping()
        {

            //Applying convention pack for a group mapping or for all the elements
            //There are several predefined mappings to use
            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camelCase", conventionPack, t => true);    //To apply to all my types

            BsonClassMap.RegisterClassMap<BusinessRule>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.BrName).SetIsRequired(true);                //Applying POCO mapping for Application class
                cm.MapMember(x => x.BrDescription).SetDefaultValue("");         //Applying POCO mapping for Application class
                cm.MapMember(x => x.BrLastModification).SetIgnoreIfNull(true);  //Applying POCO mapping for Application class
                cm.MapMember(x => x.BrUserModification).SetIgnoreIfNull(true);  //Applying POCO mapping for Application class
            }
            );

            BusinessRule br = new BusinessRule
            {
                BrName = "Business Rule 1",
                BrDescription = "This is a business rule blah blah",
                BrTypeId = 1,
                BrCategoryId = 1,
                BrCreationTime = System.DateTime.Now,
                BrUserCreation = 1,
                BrDeprecated = true,
                Aplications = new List<Application>(){
                    new Application {
                        _Id = ObjectId.Empty,
                        ApplicationName = "Application 1",
                        ApplicationDescription = "This is an application blah blah",
                        ApplicationUrlSource = "/root/GitRepository/Application1/",
                        CreationTime = System.DateTime.Now,
                        UserCreation = 1
                    }
                },
                Tags = new string[] { "Clients", "sql business rule", "Eligibility", "Integrity" }
            };

            return br;
        }

        public async Task<List<BusinessRule>> getListOfBusinessRulesByFilter(int filter, List<KeyValuePair<string, string>> filterKeyAndValue) {
            return await _daTest.getListOfBusinessRulesByFilter(filter, filterKeyAndValue);
        }

        public async Task<List<string>> getListOfBusinessRulesByFilterStrings(int filter, List<KeyValuePair<string, string>> filterKeyAndValue)
        {
            return await _daTest.getListOfBusinessRulesByFilterStrings(filter, filterKeyAndValue);
        }
    }
}