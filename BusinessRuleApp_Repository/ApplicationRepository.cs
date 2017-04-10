using BusinessRuleApp_Repository.Interfaces;
using MongoDB.Bson;

namespace BusinessRuleApp_Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        public ApplicationRepository() { }

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
    }
}