namespace BusinessRuleApp_Models.Models
{
    public class BusinessRulesTags
    {
        public int BrId { get; set; }
        public int TagId { get; set; }

        public virtual BusinessRule BusinessRule { get; set; }
    }
}
