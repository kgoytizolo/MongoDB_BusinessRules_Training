using System;

namespace BusinessRuleApp_Models.Models
{
    public partial class Application
    {
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationDescription { get; set; }
        public string ApplicationUrlSource { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public Nullable<int> UserCreation { get; set; }
    }
}