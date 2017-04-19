using BusinessRuleApp_DataAccess;
using BusinessRuleApp_Repository;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System;
using System.Threading.Tasks;

namespace BusinessRuleApp_Console
{
    public class ProgramOptions
    {
        private ApplicationRepository _appRepository;
        private BusinessRulesRepository _brRepository;
        private Connections _cnxMongoDB;

        public ProgramOptions() {
            _appRepository = new ApplicationRepository();
            _brRepository = new BusinessRulesRepository();
            _cnxMongoDB = new Connections(1);                    
        }

        //Menu with required options to accomplish Business Rules exercises with MongoDB connectivity
        public void DisplayMenu()
        {
            Console.WriteLine("Welcome to the Business Rules System!! ");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("1. Check app Document structure");
            Console.WriteLine("2. Check app Business rules structure (with arrays)");
            Console.WriteLine("3. Get Mapped Applications");
            Console.WriteLine("4. Get Mapped Business Rules");
            Console.WriteLine("5. Insert a new Application (InsertOne)");
            Console.WriteLine("6. Insert new Business Rules (InsertMany)");
            Console.ReadLine();
        }

        public void CheckAppDocumentStructure() {
            //Check results of Mongo DB Documents:
            Console.WriteLine(_appRepository.getApplications());        //Check all application Document structure <BSonDocument>                              
            Console.WriteLine("*******************************");
        }

        public void CheckBusinessRulesDocumentStructure() {
            var brDocument = _brRepository.getBusinessRules();       //MongoDB document for business rules class
            Console.WriteLine(brDocument);                           //Check all business rules Document structure <BSonDocument>
            Console.WriteLine("First ApplicationId for BR: " + brDocument["ApplicationsPerBusinessRules"][0]["ApplicationId"]);
            Console.WriteLine("*******************************");
        }

        public void GetMappedApplications() {
            //POCO representation
            var application = _appRepository.GetApplicationsForMapping();
            //To convert a class into a JSon / Bson document
            using (var app1 = new JsonWriter(Console.Out))
            {
                BsonSerializer.Serialize(app1, application);
            }
            Console.WriteLine("*******************************");
        }

        public void GetMappedBusinessRules() {
            //POCO representation
            var businessRule = _brRepository.GetBusinessRulesForMapping();
            //To convert a class into a JSon / Bson document
            using (var br1 = new JsonWriter(Console.Out))
            {
                BsonSerializer.Serialize(br1, businessRule);
            }
            Console.WriteLine("*******************************");
        }

        public async Task InsertNewApplication() {
            MongoDB.Driver.MongoClient client = _cnxMongoDB._client;
            DataAccessTest daTest = new DataAccessTest(client);
            await daTest.insertApplication(_appRepository.getApplications());
            Console.WriteLine("Document was inserted into Application collection");
            Console.WriteLine("*******************************");
        }

        public async Task InsertNewBusinessRules() {
            MongoDB.Driver.MongoClient client = _cnxMongoDB._client;
            DataAccessTest daTest = new DataAccessTest(client);
            await daTest.insertManyBusinessRules(_brRepository.getBusinessRules(), _brRepository.getBusinessRules());
            Console.WriteLine("Documents were inserted into Business Rules collection (x2)");
            Console.WriteLine("*******************************");
        }

    }
}
