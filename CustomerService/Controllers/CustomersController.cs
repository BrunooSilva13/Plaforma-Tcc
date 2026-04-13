using Microsoft.AspNetCore.Mvc;
using CustomerService.Infrastructure.Repositories;
using CustomerService.Domain;
using CustomerService.Application;
using CustomerService.Application.Services;


namespace CustomerService.Controllers;

[ApiController]
[Route("customers")]
public class CustomersController : ControllerBase
{
    private readonly CustomerRepository _repository;
    private readonly CustomerAppService _service;

    public CustomersController(CustomerRepository repository)
    {
        _repository = repository;
    }

    public CustomersController(CustomerAppService service)
    {
        _service = service;
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

    // ✅ CREATE com DTO + validação
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
    {
    
        try
        {
            var customer = new Customer(
                request.Name,
                request.Email,
                request.Phone,
                request.Document
            );

            await _repository.Create(customer);

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // ✅ PATCH nome
    [HttpPatch("{id}/name")]
    public async Task<IActionResult> UpdateName(Guid id, [FromBody] UpdateCustomerNameRequest request)
    {
        var customer = await _repository.GetById(id);

        if (customer == null)
            return NotFound();

        try
        {
            customer.ChangeName(request.Name);

            await _repository.Update(customer);

            return Ok(customer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // ✅ PATCH email
    [HttpPatch("{id}/email")]
    public async Task<IActionResult> UpdateEmail(Guid id, [FromBody] UpdateCustomerEmailRequest request)
    {
        var customer = await _repository.GetById(id);

        if (customer == null)
            return NotFound();

        try
        {
            customer.ChangeEmail(request.Email);

            await _repository.Update(customer);

            return Ok(customer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // ✅ PATCH phone
    [HttpPatch("{id}/phone")]
    public async Task<IActionResult> UpdatePhone(Guid id, [FromBody] UpdateCustomerPhoneRequest request)
    {
        var customer = await _repository.GetById(id);

        if (customer == null)
            return NotFound();

        try
        {
            customer.ChangePhone(request.Phone);

            await _repository.Update(customer);

            return Ok(customer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // ✅ DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _repository.Delete(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}    