using Dapper;

public class ProductRepository
{
    private readonly DbConnectionFactory _factory;

    public ProductRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        using var connection = _factory.CreateConnection();
        var sql = @"SELECT id, name, price, created_at as CreatedAt
            FROM products";
        return await connection.QueryAsync<Product>(sql);
    }

    public async Task<Product?> GetById(Guid id)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"SELECT id, name, price, created_at as CreatedAt
                    FROM products
                    WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
    }

    public async Task Create(Product product)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"INSERT INTO products (id, name, price, created_at)
                    VALUES (@Id, @Name, @Price, @CreatedAt)";

        await connection.ExecuteAsync(sql, product);
    }
    public async Task Update(Product product)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"
        UPDATE products
        SET name = @Name,
            price = @Price
        WHERE id = @Id;
    ";

        await connection.ExecuteAsync(sql, product);
    }

    public async Task<bool> Delete(Guid id)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"DELETE FROM products WHERE id = @Id";

        var rows = await connection.ExecuteAsync(sql, new { Id = id });

        return rows > 0;
    }
}