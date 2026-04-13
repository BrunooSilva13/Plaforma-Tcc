public class CreateProductRequest
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
}

public class UpdatePriceRequest
{
    public decimal Price { get; set; }
}

public class UpdateNameRequest
{
    public string Name { get; set; }
}