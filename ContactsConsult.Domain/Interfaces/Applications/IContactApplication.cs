using FIAP.TechChallenge.ContactsConsult.Domain.DTOs.EntityDTOs;
using FIAP.TechChallenge.ContactsConsult.Domain.Entities;

namespace FIAP.TechChallenge.ContactsConsult.Domain.Interfaces.Applications
{
    public interface IContactApplication
    {
        Task<IReadOnlyCollection<Contact>> GetContactsElastic(int page, int size);

        Task<IEnumerable<ContactDto>> GetAllContactsAsync(bool useElastic = true);

        Task<ContactDto?> GetContactByIdAsync(int id);

        Task<IEnumerable<ContactDto>> GetContactsByAreaCodeAsync(string areaCode);

        Task<ContactDto?> GetContactByEmailAsync(string email);
    }
}