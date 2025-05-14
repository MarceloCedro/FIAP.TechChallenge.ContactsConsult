using Elastic.Clients.Elasticsearch.Nodes;
using FIAP.TechChallenge.ContactsConsult.Domain.Entities;
using FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.ElasticSearch;
using FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChallenge.ContactsConsult.Domain.Services
{
    public class ContactService(
        IContactRepository contactRepository, 
        ILogger<ContactService> logger,
        IElasticClient<Contact> elasticClient) : IContactService
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly ILogger<ContactService> _logger = logger;
        private readonly IElasticClient<Contact> _elasticClient = elasticClient;

        public async Task<IReadOnlyCollection<Contact>> GetContactsElastic(int page, int size)
        {
            var contact = new Contact
            {
                Id = 3,
                Name = "Marcelo Henrique",
                AreaCode = "11",
                Phone = "974895236",
                Email = "testefiap@gmail.com"
            };

            var result = await _elasticClient.Create(contact, "contacts");

            var documents = await _elasticClient.Get(page, size, "contacts");
            return documents;
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            try
            {
                return await _contactRepository.GetByIdAsync(id);
            }
            catch (Exception)
            {
                var message = $"Some error occour when trying to get a contact with Id: {id} Contact.";
                _logger.LogError(message);
                throw new Exception(message);
            }
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            try
            {
                return await _contactRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                var message = $"Some error occour when trying to get all contacts in database.";
                _logger.LogError(message, e);
                throw new Exception(message);
            }
        }

        public async Task<IEnumerable<Contact>> GetByAreaCodeAsync(string areaCode)
        {
            try
            {
                return await _contactRepository.GetByAreaCodeAsync(areaCode);
            }
            catch (Exception e)
            {
                var message = $"Some error occour when trying to get a contact by Area Code with Code: {areaCode} Contact.";
                _logger.LogError(message, e);
                throw new Exception(message);
            }
        }

        public async Task<Contact> GetEmailCodeAsync(string email)
        {
            try
            {
                var contactEmailList = await _contactRepository.GetByEmailAsync(email);
                return contactEmailList != null && contactEmailList.Any() ? 
                       contactEmailList.FirstOrDefault() : 
                       null;
            }
            catch (Exception e)
            {
                var message = $"Some error occour when trying to get a contact by Area Code with Email: {email} Contact.";
                _logger.LogError(message, e);
                throw new Exception(message);
            }
        }
    }
}