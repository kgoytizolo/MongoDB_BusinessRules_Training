using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BusinessRuleApp_Models.Models
{
    public partial class BusinessRule
    {
        public ObjectId Id { get; set; }                                //Always include ObjectId to include MongoDB generated ID (when collection returns class instead of BsonDocument)
        public string BrName { get; set; }                                      
        public string BrDescription { get; set; }
        public Nullable<byte> BrTypeId { get; set; }
        public Nullable<byte> BrCategoryId { get; set; }

        [BsonRepresentation(BsonType.String)]                           //Using attributes for other format representation
        public Nullable<int> BrUserCreation { get; set; }
        public Nullable<System.DateTime> BrCreationTime { get; set; }

        public Nullable<int> BrUserModification { get; set; }

        [BsonRepresentation(BsonType.String)]                           //Using attributes for other format representation
        public Nullable<System.DateTime> BrLastModification { get; set; }

        public Nullable<bool> BrDeprecated { get; set; }
        public List<Application> Aplications { get; set; }
        public string[] Tags { get; set; }
    }
}