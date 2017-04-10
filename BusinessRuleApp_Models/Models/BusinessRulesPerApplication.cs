namespace BusinessRuleApp_Models.Models
{
    public partial class BusinessRulesPerApplication
    {
        public int ApplicationId { get; set; }
        public int BrId { get; set; }
        public string Component { get; set; }
        public string Release { get; set; }

        public virtual BusinessRule BusinessRule { get; set; }
    }
}
