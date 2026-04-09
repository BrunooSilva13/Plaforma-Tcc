using Microsoft.AspNetCore.Mvc;
using CustomerService.Infrastructure.Repositories;
using CustomerService.Domain;

namespace CustomerService.Controllers;

[ApiController]
[Route("customers")]
public class CustomersController : ControllerBase
{
    private readonly CustomerRepository _repository;

    public CustomersController(CustomerRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _repository.GetAll();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _repository.GetById(id);

        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Customer customer)
    {
        customer.Id = Guid.NewGuid();
        customer.CreatedAt = DateTime.UtcNow;

        await _repository.Create(customer);

        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Customer customer)
    {
        var existing = await _repository.GetById(id);

        if (existing == null)
            return NotFound();

        customer.Id = id;

        await _repository.Update(customer);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existing = await _repository.GetById(id);

        if (existing == null)
            return NotFound();

        await _repository.Delete(id);

        return NoContent();
    }
}