using Well.Loja.Dto;
using Well.Loja.Models;

namespace Well.Loja.Repository
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();

        Task<Contact> GetByIdAsync(int id);

        Task<int> PostAsync(ContactForCreationDto newContact);

        Task<int> UpdateAsync(int id, ContactForUpdateDto newContact);

        Task<int> DeleteAsync(int id);

    }
}
