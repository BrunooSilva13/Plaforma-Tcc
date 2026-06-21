using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly ProductRepository _repository;

    public ProductsController(ProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _repository.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _repository.GetById(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        try
        {
            var product = new Product(request.Name, request.Price);

            await _repository.Create(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/price")]
    public async Task<IActionResult> UpdatePrice(Guid id, [FromBody] UpdatePriceRequest request)
    {
        var product = await _repository.GetById(id);

        if (product == null)
            return NotFound();

        try
        {
            product.ChangePrice(request.Price);

            await _repository.Update(product);

            return Ok(product); 
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/name")]
    public async Task<IActionResult> UpdateName(Guid id, [FromBody] UpdateNameRequest request)
    {
        var product = await _repository.GetById(id);

        if (product == null)
            return NotFound();

        try
        {
            product.ChangeName(request.Name);

            await _repository.Update(product);

            return Ok(product);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _repository.GetById(id);
        if (product == null)
            return NotFound();

        await _repository.Delete(id);
        return NoContent();
    }
}