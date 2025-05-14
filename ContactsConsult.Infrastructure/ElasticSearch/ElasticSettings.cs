using FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.ElasticSearch;

namespace FIAP.TechChallenge.ContactsConsult.Infrastructure.ElasticSearch
{
    public class ElasticSettings : IElasticSettings
    {
        public string ApiKey { get; set; }

        public string CloudId { get; set; }
    }
}
