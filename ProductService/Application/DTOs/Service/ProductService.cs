// Application/DTOs/Service/ProductService.cs
using ProductService.Domain; // ← aqui ele confunde com o próprio arquivo

namespace ProductService.Application.Services; // ← adicione isso

public class ProductAppService
{
    private readonly IProductRepository _repository;

    public ProductAppService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> Create(CreateProductRequest request)
    {
        var name = request.Name.Trim();
        var exists = await _repository.ExistsByName(name);
        if (exists)
            throw new ArgumentException("Produto já cadastrado");

        var product = new Product(name, request.Price);
        await _repository.Create(product);
        return product;
    }
}