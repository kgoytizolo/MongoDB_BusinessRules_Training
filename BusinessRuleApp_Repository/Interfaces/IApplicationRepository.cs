using MongoDB.Bson;

namespace BusinessRuleApp_Repository.Interfaces
{
    public interface IApplicationRepository
    {
        BsonDocument getApplications();
    }
}
