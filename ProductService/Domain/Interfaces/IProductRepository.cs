namespace ProductService.Domain;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(Guid id);
    Task Create(Product product);
    Task Update(Product product);
    Task Delete(Guid id);
}