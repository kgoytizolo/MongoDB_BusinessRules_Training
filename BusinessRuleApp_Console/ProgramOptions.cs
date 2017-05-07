using BusinessRuleApp_Models.Models;
using BusinessRuleApp_Repository;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessRuleApp_Console
{
    public class ProgramOptions
    {
        private ApplicationRepository _appRepository;
        private BusinessRulesRepository _brRepository;

        public ProgramOptions() {
            _appRepository = new ApplicationRepository();
            _brRepository = new BusinessRulesRepository();
        }

        //Menu with required options to accomplish Business Rules exercises with MongoDB connectivity
        public void DisplayMenu()
        {
            Console.WriteLine("Welcome to the Business Rules System!! ");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("1) Check app Document structure");
            Console.WriteLine("2) Check app Business rules structure (with arrays)");
            Console.WriteLine("3) Get Mapped Applications");
            Console.WriteLine("4) Get Mapped Business Rules");
            Console.WriteLine("5) Insert a new Application (InsertOne)");
            Console.WriteLine("6) Insert new Business Rules (InsertMany)");
            Console.WriteLine("7) Find Application documents from MongoDB using a cursor per batch found");
            Console.WriteLine("8) Find Business Rules documents from MongoDB using a list (all documents)");
            Console.WriteLine("9) Find Business Rules documents from MongoDB using foreach (all documents)");
            Console.WriteLine("A) Find Application documents with filters - By Name using String Key/Value (Application 2)");
            Console.WriteLine("B) Find Application documents with filters - By Name using JSON Key/Value (Application 2)");
            Console.WriteLine("C) Find Business Rules documents with filters - Using Key/Value (Categories Less Than 2 / $lt)");
            Console.WriteLine("D) Find Business Rules documents with filters - Using complex Key/Value conjunctions and filters with BsonArray");
            Console.WriteLine("E) Find Business Rules documents with filters - Using Filter Definition Builder (Categories Less Than 2 / $lt)");
            Console.WriteLine("F) Find Business Rules documents with filters - Using Filter Definition Builder for complex search");
            Console.WriteLine("G) Find Business Rules documents with filters - Using Filter Definition Builder for complex search (2)");
            Console.WriteLine("H) Find Business Rules documents with filters - Using <BusinessRule> class instead of JSON <BsonDocument> for complex search (2)");
            Console.WriteLine("I) Find Business Rules documents with filters - Using <BusinessRule> class, builders and delegates for complex search (2)");
            Console.WriteLine("J) Find Business Rules documents with filters - Using <BusinessRule> class and expression trees only for complex search (2)");
            Console.WriteLine("K) Find Business Rules documents with filters - Using MongoDB Limits(1) to restrict retrieved data (Paging)");
            Console.WriteLine("L) Find Business Rules documents with filters - Using MongoDB Limits(1) and Skip(1) to restrict retrieved data (Paging)");
            Console.WriteLine("M) Find Business Rules documents with filters - Using MongoDB Sort(Descending CreationTime) + Limits(10) Business Rules");
            Console.WriteLine("N) Find Business Rules documents with filters - Using MongoDB Sort(Desc Creation Time, Asc Name) + Limits(10) Business Rules");
            Console.WriteLine("O) Find Business Rules documents with filters - Using MongoDB Sort(Desc Creation Time, Asc Name) + Limits(10) + expression trees");
            Console.WriteLine("P) Display all Business Rules documents using MongoDB Projections <BsonDocument> - only Name and Creation Time");
            Console.WriteLine("Q) Display all Business Rules documents using MongoDB Projections <BsonDocument> - only Name and Creation Time (2)");
            Console.WriteLine("R) Display all Business Rules documents using MongoDB Builders<BsonDocument>.Projection - only Name and Creation Time");
            Console.WriteLine("S) Display all Business Rules documents using MongoDB Builders<BusinessRule>.Projection - only Name and Creation Time");
            Console.WriteLine("T) Display all Business Rules documents using MongoDB Builders<BusinessRule>.Projection (and expression trees)");
            Console.WriteLine("U) Display all Business Rules documents using MongoDB Projection with expression trees only (String 'BrName' results)");
            Console.ReadLine();
        }

        //Option 1: 
        public void CheckAppDocumentStructure() {
            //Check results of Mongo DB Documents:
            Console.WriteLine(_appRepository.getApplications());        //Check all application Document structure <BSonDocument>                              
            Console.WriteLine("*******************************");
        }

        //Option 2:
        public void CheckBusinessRulesDocumentStructure() {
            var brDocument = _brRepository.getBusinessRules();       //MongoDB document for business rules class
            Console.WriteLine(brDocument);                           //Check all business rules Document structure <BSonDocument>
            Console.WriteLine("First ApplicationId for BR: " + brDocument["ApplicationsPerBusinessRules"][0]["ApplicationId"]);
            Console.WriteLine("*******************************");
        }

        //Option 3:
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

        //Option 4:
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

        //Option 5:    
        public async Task InsertNewApplication(string selectedOption) {
            await _appRepository.InsertOneApplication(_appRepository.getApplications());
            Console.Clear();
            GenericDocumentMessages(1, selectedOption);
        }

        //Option 6:
        public async Task InsertNewBusinessRules(string selectedOption) {
            var businessRule = _brRepository.GetBusinessRulesForMapping();
            var bsonDocument1 = businessRule.ToBsonDocument();
            var bsonDocument2 = businessRule.ToBsonDocument();
            await _brRepository.InsertManyBusinessRules(bsonDocument1, bsonDocument2);
            Console.Clear();
            GenericDocumentMessages(1, selectedOption);
        }

        //Option 7:
        public async Task GetListOfApplications() {
            var col = await _appRepository.GetListOfApplicationsFromDb();
            Console.WriteLine();
            Console.WriteLine("Get list of All Applications *******************************");
            //We look for all documents from an specific collection (Application) using a cursor per each batch found
            using (var cursor = await col.Find(new BsonDocument()).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())        //Moving per each batch
                {
                    foreach (var doc in cursor.Current)
                    {
                        Console.WriteLine(doc);
                    }
                }
            }
            Console.WriteLine("*******************************");
            Console.WriteLine("");
        }

        //Option 8:
        public async Task GetListOfBusinessRules() {
            var col = await _brRepository.GetListOfBusinessRulesFromDb();
            Console.WriteLine();
            Console.WriteLine("Get list of All Business Rules *******************************");
            //We look for all documents from an specific collection (Business Rules) using a quick way (List) without a cursor. 
            //All documents will be delivered
            var list = await col.Find(new BsonDocument()).ToListAsync();
            foreach (var doc in list)
            {
                Console.WriteLine(doc);
            }
            Console.WriteLine("");
            Console.WriteLine("*******************************");
            Console.WriteLine("");
        }

        //Option 9:
        public async Task GetListOfBusinessRules2() {
            var col = await _brRepository.GetListOfBusinessRulesFromDb();
            Console.WriteLine();
            Console.WriteLine("Get list of All Business Rules *******************************");
            //We look for all documents from an specific collection (Business Rules) using a quick way (List) without a cursor. 
            //All documents will be delivered
            await col.Find(new BsonDocument()).ForEachAsync(doc => Console.WriteLine(doc));
            Console.WriteLine("");
            Console.WriteLine("*******************************");
            Console.WriteLine("");
        }

        //Option between A and G - Mongo DB filters search with Bson Document:
        public async Task GetListOfAppsAndBusinessRulesByFilterBsonDocs(int filterType, List<KeyValuePair<string, string>> filterKeyAndValue, string selectedOption)
        {
            Console.Clear();
            List<BsonDocument> listOfAppsByFilter = await _appRepository.SearchApplicationsByFilter(filterType, filterKeyAndValue);
            //In case there are no results, print another message
            if (listOfAppsByFilter.Count == 0 || listOfAppsByFilter == null)
            {
                GenericDocumentMessages(0, selectedOption);
            }
            else {
                //All documents will be delivered
                foreach (var doc in listOfAppsByFilter)
                {
                    Console.WriteLine(doc);
                }
                GenericDocumentMessages(listOfAppsByFilter.Count, selectedOption);
            }
        }

        //Option H and beyond - Mongo DB filters search with Business Rule class List:
        public async Task GetListOfBusinessRulesByFilter(int filterType, List<KeyValuePair<string, string>> filterKeyAndValue, string selectedOption)
        {
            Console.Clear();
            List<BusinessRule> listOfBrsByFilter = await _brRepository.getListOfBusinessRulesByFilter(filterType, filterKeyAndValue);
            //In case there are no results, print another message
            if (listOfBrsByFilter.Count == 0 || listOfBrsByFilter == null)
            {
                GenericDocumentMessages(0, selectedOption);
            }
            else
            {
                //All documents will be delivered
                foreach (var br in listOfBrsByFilter)
                {
                    Console.WriteLine("Business Rule Name: " + br.BrName + ", Id: " + br.Id + ", Creation Date: " + br.BrCreationTime.ToString());
                }
                GenericDocumentMessages(listOfBrsByFilter.Count, selectedOption);
            }
        }

        //Option U - Mongo DB filters search with String class List
        public async Task getListOfBusinessRulesByFilterString(int filter, List<KeyValuePair<string, string>> filterKeyAndValue, string selectedOption) {
            Console.Clear();
            List<string> listOfBrs = await _brRepository.getListOfBusinessRulesByFilterStrings(filter, filterKeyAndValue);
            //In case there are no results, print another message
            if (listOfBrs.Count == 0 || listOfBrs == null)
            {
                GenericDocumentMessages(0, selectedOption);
            }
            else
            {
                //All documents will be delivered
                foreach (var br in listOfBrs)
                {
                    Console.WriteLine("Business Rule Name: " + br.ToString());
                }
                GenericDocumentMessages(listOfBrs.Count, selectedOption);
            }
        }

        //General message after dynamic results
        private void GenericDocumentMessages(int totalReg, string selectedOption) {
            Console.WriteLine("");
            if (totalReg == 0)  Console.WriteLine("Sorry! No results found for this search!");
            else {
                switch (selectedOption) {
                    case "5":
                        Console.WriteLine("Document was inserted into Application collection (x1)");
                        break;
                    case "6":
                        Console.WriteLine("Documents were inserted into Business Rules collection (x2)");
                        break;
                    default:
                        Console.WriteLine("A total of " + totalReg + " document(s) have been found in the current search");
                        break;
                }                
                switch (selectedOption)
                {
                    case "K":
                    case "L":
                        Console.WriteLine("Business Rules has been limited to the first result (1 row only)");
                        break;
                    case "M":
                        Console.WriteLine("Business Rules has been limited to the first 10 results, sorted by Creation Time (Descending)");
                        break;
                    case "N":
                        Console.WriteLine("Business Rules has been limited to the first 10 results, sorted by Creation Time (Descending) and Name (Ascending)");
                        break;
                    case "S":
                    case "T":
                        Console.WriteLine("Business Rules '_Id' appears with zeroes as a default value due to only 'brName' and 'brCreationTime' were returned from MongoDB database to Business Rule class. '_Id' is empty.");
                        break;
                }
            } 
            Console.WriteLine("");
            Console.WriteLine("*******************************");
            Console.WriteLine("");
        }

    }
}
