using MongoDB.Bson;

namespace BusinessRuleApp_Repository
{
    public class BusinessRulesRepository
    {
        public BusinessRulesRepository() { }

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

    }
}