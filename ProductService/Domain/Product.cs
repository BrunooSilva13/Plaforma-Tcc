namespace ProductService.Domain;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Product(string name, decimal price)
    {
        ValidateName(name);
        ValidatePrice(price);

        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        CreatedAt = DateTime.UtcNow;
    }

    public void ChangeName(string newName)
    {
        ValidateName(newName);
        Name = newName;
    }

    public void ChangePrice(decimal newPrice)
    {
        ValidatePrice(newPrice);
        Price = newPrice;
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome é obrigatório");
    }

    private void ValidatePrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Preço deve ser maior que zero");
    }
}