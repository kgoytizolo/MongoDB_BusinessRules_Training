using BusinessRuleApp_Repository.Interfaces;
using MongoDB.Bson;
using BusinessRuleApp_Models.Models;
using MongoDB.Bson.Serialization;
using System.Threading.Tasks;
using BusinessRuleApp_DataAccess;
using MongoDB.Driver;
using System.Collections.Generic;

namespace BusinessRuleApp_Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private DataAccessTest _daTest;

        public ApplicationRepository() {
            _daTest = new DataAccessTest();
        }

        //Getting Applications From MongoDB directly (BSonDocument) 
        public BsonDocument getApplications()
        {
            var docApp = new BsonDocument
            {
                { "ApplicationName", "Application 2" }
            };

            docApp.Add("ApplicationDescription", "This is an application blah blah");   //Adding a new row (key, value)
            docApp["ApplicationUrlSource"] = "/root/GitRepository/Application2/";       //Adding another row ([key] = value)
            docApp["CreationTime"] = System.DateTime.Now.ToString();                    //Adding another row ([key] = value)
            docApp["UserCreation"] = 1;                                                 //Adding another row ([key] = value)

            return docApp;
        }

        //Getting Aplications using POCO Representation - From C# class to BsonDocument mapping
        public Application GetApplicationsForMapping()
        {
            BsonClassMap.RegisterClassMap<Application>(cm =>
                {
                    cm.AutoMap();
                    cm.MapMember(x => x.ApplicationUrlSource).SetIsRequired(true);       //Applying POCO mapping for Application class
                    cm.MapMember(x => x.ApplicationDescription).SetDefaultValue("");     //Applying POCO mapping for Application class
                }
            );

            Application app = new Application
            {
                ApplicationName = "Application 1",
                ApplicationDescription = "This is an application blah blah",
                ApplicationUrlSource = "/root/GitRepository/Application1/",
                CreationTime = System.DateTime.Now,
                UserCreation = 1
            };

            return app;
        }

        //Insert aplication sample (InsertOne)
        public async Task InsertOneApplication(BsonDocument appSample1)
        {
            await _daTest.insertApplication(getApplications());
        }

        //Search and list all Aplications
        public async Task<IMongoCollection<BsonDocument>> GetListOfApplicationsFromDb()
        {
            return await _daTest.getListOfApplications();
        }

        //Search and list Aplications (by filter)
        public async Task<List<BsonDocument>> SearchApplicationsByFilter(int filter, List<KeyValuePair<string, string>> filterKeyAndValue) {
            return await _daTest.getListOfApplicationsByFilter(filter, filterKeyAndValue);
        }

    }
}