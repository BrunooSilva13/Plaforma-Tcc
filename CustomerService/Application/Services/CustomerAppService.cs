using CustomerService.Domain;
using CustomerService.Domain.Interfaces;
using CustomerService.Application.Requests;

namespace CustomerService.Application.Services;

public class CustomerAppService
{
    private readonly ICustomerRepository _repository;

    public CustomerAppService(
        ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Customer> Create(
        CreateCustomerRequest request)
    {
        var email = request.Email
            .Trim()
            .ToLowerInvariant();

        if (await _repository.ExistsByDocument(request.Document))
            throw new ArgumentException("Documento já cadastrado");

        if (await _repository.ExistsByEmail(email))
            throw new ArgumentException("Email já cadastrado");

        var customer = new Customer(
            request.Name,
            email,
            request.Phone,
            request.Document
        );

        await _repository.Create(customer);

        return customer;
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await _repository.GetAll();
    }
    public async Task<Customer?> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task Update(Customer customer)
    {
        var exists = await _repository.GetById(customer.Id);

        if (exists == null)
            throw new ArgumentException("Cliente não encontrado");

        await _repository.Update(customer);
    }

    public async Task<bool> Delete(Guid id)
    {
        var deleted = await _repository.Delete(id);

        return deleted;
    }

    public async Task<Customer?> UpdateName(
    Guid id,
    UpdateCustomerNameRequest request)
    {
        var customer = await _repository.GetById(id);

        if (customer == null)
            return null;


        customer.UpdateName(request.Name);

        await _repository.Update(customer);

        return customer;
    }

    public async Task<Customer?> UpdateEmail(
        Guid id,
        UpdateCustomerEmailRequest request)
    {
        var customer = await _repository.GetById(id);

        if (customer == null)
            return null;

        var email = request.Email
            .Trim()
            .ToLowerInvariant();

        if (await _repository.ExistsByEmail(email))
            throw new ArgumentException("Email já cadastrado");

        customer.UpdateEmail(email);

        await _repository.Update(customer);

        return customer;
    }

    public async Task<Customer?> UpdatePhone(
        Guid id,
        UpdateCustomerPhoneRequest request)
    {
        var customer = await _repository.GetById(id);

        if (customer == null)
            return null;

        customer.UpdatePhone(request.Phone);

        await _repository.Update(customer);

        return customer;
    }
}


