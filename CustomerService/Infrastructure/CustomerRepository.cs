using Dapper;
using CustomerService.Domain;

namespace CustomerService.Infrastructure.Repositories;

public class CustomerRepository
{
    private readonly DbConnectionFactory _factory;

    public CustomerRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        using var connection = _factory.CreateConnection();

        var sql = "SELECT * FROM customers";

        return await connection.QueryAsync<Customer>(sql);
    }

    public async Task<Customer?> GetById(Guid id)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"SELECT 
                        id,
                        name,
                        email,
                        phone,
                        document,
                        created_at as CreatedAt
                    FROM customers 
                    WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
    }

    public async Task<Customer?> GetByDocument(string document)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"
        SELECT 
            id,
            name,
            email,
            phone,
            document,
            created_at as CreatedAt
        FROM customers
        WHERE document = @Document
    ";

        return await connection.QueryFirstOrDefaultAsync<Customer>(
            sql,
            new { Document = document }
        );
    }

    public async Task Create(Customer customer)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"INSERT INTO customers (id, name, email, phone, document, created_at)
                    VALUES (@Id, @Name, @Email, @Phone, @Document, @CreatedAt)";

        await connection.ExecuteAsync(sql, customer);
    }

    public async Task Update(Customer customer)
    {
        using var connection = _factory.CreateConnection();

        var sql = @"UPDATE customers 
                SET name = @Name,
                    email = @Email,
                    phone = @Phone,
                    document = @Document
                WHERE id = @Id";

        await connection.ExecuteAsync(sql, customer);
    }

    public async Task<bool> Delete(Guid id)
    {
        using var connection = _factory.CreateConnection();

        var sql = "DELETE FROM customers WHERE id = @Id";

        var rows = await connection.ExecuteAsync(sql, new { Id = id });

        return rows > 0;
    }
}