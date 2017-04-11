using BusinessRuleApp_Repository.Interfaces;
using MongoDB.Bson;
using BusinessRuleApp_Models.Models;
using MongoDB.Bson.Serialization;

namespace BusinessRuleApp_Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        public ApplicationRepository() { }

        //Getting Applications From MongoDB directly (BSonDocument) 
        public BsonDocument getApplications()
        {
            var docApp = new BsonDocument
            {
                { "ApplicationName", "Application 1" }
            };

            docApp.Add("ApplicationDescription", "This is an application blah blah");   //Adding a new row (key, value)
            docApp["ApplicationUrlSource"] = "/root/GitRepository/Application1/";       //Adding another row ([key] = value)
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
                ApplicationId = 1,
                ApplicationName = "Application 1",
                ApplicationDescription = "This is an application blah blah",
                ApplicationUrlSource = "/root/GitRepository/Application1/",
                CreationTime = System.DateTime.Now,
                UserCreation = 1
            };

            return app;
        }

    }
}