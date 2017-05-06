using BusinessRuleApp_Models.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessRuleApp_Repository.Interfaces
{
    public interface IBusinessRulesRepository
    {
        BsonDocument getBusinessRules();
        Task InsertManyBusinessRules(BsonDocument brSample1, BsonDocument brSample2);
        Task<IMongoCollection<BsonDocument>> GetListOfBusinessRulesFromDb();
        BusinessRule GetBusinessRulesForMapping();
        Task<List<BusinessRule>> getListOfBusinessRulesByFilter(int filter, List<KeyValuePair<string, string>> filterKeyAndValue);
    }
}
