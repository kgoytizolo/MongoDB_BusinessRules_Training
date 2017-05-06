using System;
using System.Diagnostics;

namespace BusinessRuleApp_ErrorHandler
{
    public static class GenericError
    {
        //Create class to print any error result
        public static void PrintErrorMessages(Exception e)
        {
            Debug.WriteLine("Error: " + e.Message);
            Debug.WriteLine("Error: " + e.Source);
            Debug.WriteLine("Error: " + e.StackTrace);
        }

    }
}
