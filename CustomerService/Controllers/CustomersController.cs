using Microsoft.AspNetCore.Mvc;
using CustomerService.Application.Requests;
using CustomerService.Application.Services;
using CustomerService.Application;

namespace CustomerService.Controllers;

[ApiController]
[Route("customers")]
public class CustomersController : ControllerBase
{
    private readonly CustomerAppService _service;

    public CustomersController(CustomerAppService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _service.GetAll();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customer = await _service.GetById(id);

        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCustomerRequest request)
    {
        try
        {
            var customer = await _service.Create(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.Id },
                customer
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/name")]
    public async Task<IActionResult> UpdateName(
        Guid id,
        [FromBody] UpdateCustomerNameRequest request)
    {
        try
        {
            var customer = await _service.UpdateName(id, request);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/email")]
    public async Task<IActionResult> UpdateEmail(
        Guid id,
        [FromBody] UpdateCustomerEmailRequest request)
    {
        try
        {
            var customer = await _service.UpdateEmail(id, request);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/phone")]
    public async Task<IActionResult> UpdatePhone(
        Guid id,
        [FromBody] UpdateCustomerPhoneRequest request)
    {
        try
        {
            var customer = await _service.UpdatePhone(id, request);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.Delete(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}