using FIAP.TechChallenge.ContactsConsult.Domain.Entities;

namespace FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.Services
{
    public interface IContactService
    {
        Task<IReadOnlyCollection<Contact>> GetAllContactsElastic(int page, int size);

        Task<Contact> GetByIdAsync(int id);

        Task<IEnumerable<Contact>> GetAllAsync(bool useElastic = true);

        Task<IEnumerable<Contact>> GetByAreaCodeAsync(string areaCode);

        Task<Contact> GetEmailCodeAsync(string email);
    }
}