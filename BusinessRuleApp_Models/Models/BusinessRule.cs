using System;
using System.Collections.Generic;

namespace BusinessRuleApp_Models.Models
{
    public partial class BusinessRule
    {
        public BusinessRule()
        {
            this.BusinessRulesPerApp = new HashSet<BusinessRulesPerApplication>();
            this.BusinessRulesTags = new HashSet<BusinessRulesTags>();
        }

        public int BrId { get; set; }
        public string BrName { get; set; }
        public string BrDescription { get; set; }
        public Nullable<byte> BrTypeId { get; set; }
        public Nullable<byte> BrCategoryId { get; set; }
        public Nullable<int> BrUserCreation { get; set; }
        public Nullable<System.DateTime> BrCreationTime { get; set; }
        public Nullable<int> BrUserModification { get; set; }
        public Nullable<System.DateTime> BrLastModification { get; set; }
        public Nullable<bool> BrDeprecated { get; set; }

        //A Business rules might have in one or more apps
        public virtual ICollection<BusinessRulesPerApplication> BusinessRulesPerApp { get; set; }

        //Business rules and related tags for accurate findings
        public virtual ICollection<BusinessRulesTags> BusinessRulesTags { get; set; }


    }
}