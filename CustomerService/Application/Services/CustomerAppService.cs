using CustomerService.Domain;
using CustomerService.Infrastructure.Repositories;


namespace CustomerService.Application.Services;



public class CustomerAppService
{
    private readonly CustomerRepository _repository;

    public CustomerAppService(CustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Customer> Create(string name, string email, string phone, string document)
    {
        var existing = await _repository.GetByDocument(document);

        if (existing != null)
            throw new ArgumentException("Documento já cadastrado");

        var customer = new Customer(name, email, phone, document);

        await _repository.Create(customer);

        return customer;
    }
}