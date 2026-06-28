using CustomerService.Domain.Interfaces;    
using CustomerService.Domain.Entities;


namespace CustomerService.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer?> GetById(Guid id);
    Task<Customer?> GetByDocument(string document);

    Task<bool> ExistsByDocument(string document);
    Task<bool> ExistsByEmail(string email);

    Task Create(Customer customer);
    Task Update(Customer customer);
    Task<bool> Delete(Guid id);
    
}