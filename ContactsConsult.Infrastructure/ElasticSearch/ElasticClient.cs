using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using FIAP.TechChallenge.ContactsConsult.Domain.Entities;
using FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.ElasticSearch;

namespace FIAP.TechChallenge.ContactsConsult.Infrastructure.ElasticSearch
{
    public class ElasticClient<T> : IElasticClient<T>
    {
        private readonly ElasticsearchClient _client;

        public ElasticClient(IElasticSettings settings)
        {
            this._client = new ElasticsearchClient(settings.CloudId, new ApiKey(settings.ApiKey));
        }

        public async Task<bool> Create(T log, IndexName index)
        {
            var response = await _client.IndexAsync<T>(log, index);

            return response.IsValidResponse;
        }

        public async Task<IReadOnlyCollection<T>> Get(int page, int size, IndexName index)
        {
            var response = await _client.SearchAsync<T>(s => s.Index(index)
                                                              .From(page)
                                                              .Size(size));
            return response.Documents;
        }
    }
}
