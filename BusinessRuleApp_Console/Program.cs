using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessRuleApp_ErrorHandler;

namespace BusinessRuleApp_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MainAsync(args).Wait();
            }
            catch (Exception e) {
                GenericError.PrintErrorMessages(e);
            }            
            Console.WriteLine("Press Enter");
            Console.ReadLine();
        }

        static async Task MainAsync(string[] args)
        {
            ProgramOptions MyMenuOptions = new ProgramOptions();
            ConsoleKeyInfo cki;

            do {
                MyMenuOptions.DisplayMenu();
                cki = Console.ReadKey(false);
                switch (cki.KeyChar.ToString().ToUpper()) {
                    case "1":
                        MyMenuOptions.CheckAppDocumentStructure();
                        break;
                    case "2":
                        MyMenuOptions.CheckBusinessRulesDocumentStructure();
                        break;
                    case "3":
                        MyMenuOptions.GetMappedApplications();
                        break;
                    case "4":
                        MyMenuOptions.GetMappedBusinessRules();
                        break;
                    case "5":
                        await MyMenuOptions.InsertNewApplication();
                        break;
                    case "6":
                        await MyMenuOptions.InsertNewBusinessRules();
                        break;
                    case "7":
                        await MyMenuOptions.GetListOfApplications();
                        break;
                    case "8":
                        await MyMenuOptions.GetListOfBusinessRules();
                        break;
                    case "9":
                        await MyMenuOptions.GetListOfBusinessRules2();
                        break;
                    case "A":
                        await MyMenuOptions.GetListOfAppsByFilter(1, SetFilters());
                        break;
                    case "B":
                        await MyMenuOptions.GetListOfAppsByFilter(2, SetFilters());
                        break;
                    case "C":
                        await MyMenuOptions.GetListOfAppsByFilter(3, SetFilters());
                        break;
                    case "D":
                        await MyMenuOptions.GetListOfAppsByFilter(4, SetFilters());
                        break;
                    case "E":
                        await MyMenuOptions.GetListOfAppsByFilter(5, SetFilters());
                        break;
                    case "F":
                        await MyMenuOptions.GetListOfAppsByFilter(6, SetFilters());
                        break;
                    case "G":
                        await MyMenuOptions.GetListOfAppsByFilter(7, SetFilters());
                        break;
                    case "H":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(1, SetFilters());
                        break;
                    case "I":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(2, SetFilters());
                        break;
                    case "J":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(3, SetFilters());
                        break;
                }
            }
            while (cki.Key != ConsoleKey.Escape);          
        }

        private static List<KeyValuePair<string, string>> SetFilters() {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("ApplicationName","Application 2"),
                new KeyValuePair<string,string>("BrCategoryId","2"),
                new KeyValuePair<string,string>("BrName","Business Rule 1"),
                new KeyValuePair<string,string>("BrName","Business Rule 2"),
                new KeyValuePair<string,string>("BrCategoryId","1")
            }; 
        }

    }
}
