using CustomerService.Domain.Interfaces;


namespace CustomerService.Domain;

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