using BusinessRuleApp_Models.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessRuleApp_Repository.Interfaces
{
    public interface IApplicationRepository
    {
        BsonDocument getApplications();
        Application GetApplicationsForMapping();
        Task InsertOneApplication(BsonDocument appSample1);
        Task<IMongoCollection<BsonDocument>> GetListOfApplicationsFromDb();
        Task<List<BsonDocument>> SearchApplicationsByFilter(int filter, List<KeyValuePair<string, string>> filterKeyAndValue);
        Task<int> replaceApplicationValues(int filter);
        Task<int> deleteApplicationValues(int filter);
    }
}
