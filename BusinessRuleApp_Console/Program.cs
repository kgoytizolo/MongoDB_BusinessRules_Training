using System;
using System.Threading.Tasks;
using BusinessRuleApp_Repository;

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
        }

    }
}
