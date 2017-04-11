using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusinessRuleApp_Models.Models
{
    public partial class Application
    {
        public int ApplicationId { get; set; }

        [BsonElement("AppName")]                                        //Using attributes to apply POCO mapping (customization)
        public string ApplicationName { get; set; }

        [BsonElement("AppDescription")]                                 //Using attributes to apply POCO mapping (customization)
        public string ApplicationDescription { get; set; }

        [BsonElement("AppUrlSource")]                                   //Using attributes to apply POCO mapping (customization)
        public string ApplicationUrlSource { get; set; }

        [BsonRepresentation(BsonType.String)]                           //Using attributes for other format representation
        public Nullable<System.DateTime> CreationTime { get; set; }     

        public Nullable<int> UserCreation { get; set; }

    }
}