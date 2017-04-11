using MongoDB.Bson;
using BusinessRuleApp_Models.Models;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace BusinessRuleApp_Repository
{
    public class BusinessRulesRepository
    {
        public BusinessRulesRepository() { }

        //Getting Business Rules From MongoDB directly (BSonDocument)  
        public BsonDocument getBusinessRules()
        {
            var docBr = new BsonDocument
            {
                { "BrName", "Business Rule 1" }                               //Creating a new business rule from scratch
            };
            docBr["BrDescription"] = "This is a business rule blah blah";     //Adding another row ([key] = value)
            docBr.Add("BrTypeId", 1);                                         //Adding a new row (key, value)
            docBr.Add("BrCategoryId", 1);                                     //Adding a new row (key, value)
            docBr.Add("BrUserCreation", 1);                                   //Adding a new row (key, value)
            docBr["BrCreationTime"] = System.DateTime.Now.ToString();         //Adding another row ([key] = value)

            //Object's arrays:
            var nestedBrPerAppArray = new BsonArray();
            nestedBrPerAppArray.Add(new BsonDocument("ApplicationId", "1"));    //Creating an array
            docBr.Add("ApplicationsPerBusinessRules", nestedBrPerAppArray);     //Adding an array to the business rules

            return docBr;
        }

        //Getting Business Rules using POCO Representation - From C# class to BsonDocument mapping
        public BusinessRule GetBusinessRulesForMapping() {

            //Applying convention pack for a group mapping or for all the elements
            //There are several predefined mappings to use
            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camelCase", conventionPack, t => true);    //To apply to all my types

            BsonClassMap.RegisterClassMap<BusinessRule>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.BrName).SetIsRequired(true);                //Applying POCO mapping for Application class
                cm.MapMember(x => x.BrDescription).SetDefaultValue("");         //Applying POCO mapping for Application class
                cm.MapMember(x => x.BrLastModification).SetIgnoreIfNull(true);  //Applying POCO mapping for Application class
                cm.MapMember(x => x.BrUserModification).SetIgnoreIfNull(true);  //Applying POCO mapping for Application class
            }
            ); 

            BusinessRule br = new BusinessRule
            {
                BrId = 1,
                BrName = "Business Rule 1",
                BrDescription = "This is a business rule blah blah",
                BrTypeId = 1,
                BrCategoryId = 1,
                BrCreationTime = System.DateTime.Now,
                BrUserCreation = 1,
                BrDeprecated = true,
                Aplications = new List<Application>(){
                    new Application {
                        ApplicationId = 1,
                        ApplicationName = "Application 1",
                        ApplicationDescription = "This is an application blah blah",
                        ApplicationUrlSource = "/root/GitRepository/Application1/",
                        CreationTime = System.DateTime.Now,
                        UserCreation = 1
                    }
                },
                Tags = new string[] { "Clients", "sql business rule", "Eligibility", "Integrity" }
            };

            return br;
        }

    }
}