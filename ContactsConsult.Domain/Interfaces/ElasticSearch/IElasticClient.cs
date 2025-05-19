using Elastic.Clients.Elasticsearch;

namespace FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.ElasticSearch
{
    public interface IElasticClient<T>
    {
        Task<IReadOnlyCollection<T>> Get(int page, int size, IndexName index);

        Task<bool> Create(T log, IndexName index);

        Task<List<T>> GetByEmail(string email, IndexName index);

        Task<List<T>> GetByJsonId(int id, IndexName index);
    }
}
