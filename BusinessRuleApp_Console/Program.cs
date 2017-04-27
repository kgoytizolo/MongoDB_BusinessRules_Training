using System;
using System.Threading.Tasks;

namespace BusinessRuleApp_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.SetWindowSize(800, 600);
            MainAsync(args).Wait();
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
                switch (cki.KeyChar.ToString()) {
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
                    case "10":
                        await MyMenuOptions.GetListOfAppsByFilter(1);
                        break;
                }
            }
            while (cki.Key != ConsoleKey.Escape);          
        }

    }
}
