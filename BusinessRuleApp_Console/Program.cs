using System;
using System.Threading.Tasks;
using BusinessRuleApp_Repository;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace BusinessRuleApp_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
            Console.WriteLine("Press Enter");
            Console.ReadLine();
        }

        static async Task MainAsync(string[] args)
        {
            //var client = new Connections(1);
            var appRepository = new ApplicationRepository();
            var brRepository = new BusinessRulesRepository();
            var appDocument = appRepository.getApplications();      //MongoDB document for application class
            var brDocument = brRepository.getBusinessRules();       //MongoDB document for business rules class
            //Check results of Mongo DB Documents:
            Console.WriteLine(appDocument);         //Check all application Document structure <BSonDocument>                              
            Console.WriteLine("*******************************");
            Console.WriteLine(brDocument);          //Check all business rules Document structure <BSonDocument>
            Console.WriteLine("First ApplicationId for BR: " + brDocument["ApplicationsPerBusinessRules"][0]["ApplicationId"]);
            Console.WriteLine("*******************************");

            Console.WriteLine("SECOND EXERCISE *******************************");

            //POCO representation
            var application = appRepository.GetApplicationsForMapping();
            var businessRule = brRepository.GetBusinessRulesForMapping();

            //To convert a class into a JSon / Bson document
            using (var app1 = new JsonWriter(Console.Out))
            {
                BsonSerializer.Serialize(app1, application);
            }

            Console.WriteLine("*******************************");

            //To convert a class into a JSon / Bson document
            using (var br1 = new JsonWriter(Console.Out))
            {
                BsonSerializer.Serialize(br1, businessRule);
            }

        }

    }
}
