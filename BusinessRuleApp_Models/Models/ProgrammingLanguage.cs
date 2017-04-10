namespace BusinessRuleApp_Models.Models
{
    public partial class ProgrammingLanguage
    {
        public short ProgrammingLanguageId { get; set; }
        public string ProgrammingLanguageName { get; set; }
        public string ProgrammingLanguageDescription { get; set; }
        public string Version { get; set; }
        public string CommentsSymbolIn { get; set; }
        public string CommentsSymbolOut { get; set; }
    }
}
