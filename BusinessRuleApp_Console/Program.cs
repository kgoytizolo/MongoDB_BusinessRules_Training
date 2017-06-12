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
                        await MyMenuOptions.InsertNewApplication(cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "6":
                        await MyMenuOptions.InsertNewBusinessRules(cki.KeyChar.ToString().Trim().ToUpper());
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
                        await MyMenuOptions.GetListOfAppsAndBusinessRulesByFilterBsonDocs(1, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "B":
                        await MyMenuOptions.GetListOfAppsAndBusinessRulesByFilterBsonDocs(2, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "C":
                        await MyMenuOptions.GetListOfAppsAndBusinessRulesByFilterBsonDocs(3, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "D":
                        await MyMenuOptions.GetListOfAppsAndBusinessRulesByFilterBsonDocs(4, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "E":
                        await MyMenuOptions.GetListOfAppsAndBusinessRulesByFilterBsonDocs(5, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "F":
                        await MyMenuOptions.GetListOfAppsAndBusinessRulesByFilterBsonDocs(6, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "G":
                        await MyMenuOptions.GetListOfAppsAndBusinessRulesByFilterBsonDocs(7, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "H":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(1, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "I":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(2, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "J":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(3, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "K":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(4, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "L":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(5, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "M":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(6, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "N":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(7, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "O":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(8, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "P":
                        await MyMenuOptions.GetListOfAppsAndBusinessRulesByFilterBsonDocs(8, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "Q":
                        await MyMenuOptions.GetListOfAppsAndBusinessRulesByFilterBsonDocs(9, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "R":
                        await MyMenuOptions.GetListOfAppsAndBusinessRulesByFilterBsonDocs(10, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "S":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(9, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "T":
                        await MyMenuOptions.GetListOfBusinessRulesByFilter(10, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "U":
                        await MyMenuOptions.getListOfBusinessRulesByFilterString(1, SetFilters(), cki.KeyChar.ToString().Trim().ToUpper());
                        break;
                    case "V":
                        await MyMenuOptions.ReplaceExistingApplication(cki.KeyChar.ToString().Trim().ToUpper(),1);
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
