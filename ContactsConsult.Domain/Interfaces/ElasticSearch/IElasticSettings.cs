namespace FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.ElasticSearch
{
    public interface IElasticSettings
    {
        string ApiKey { get; set; }

        string CloudId { get; set; }
    }
}
