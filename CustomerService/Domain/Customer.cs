namespace CustomerService.Domain;



public class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Document { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Customer() { } // para o Dapper

    public Customer(string name, string email, string phone, string document)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome é obrigatório");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("Email inválido");

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Telefone é obrigatório");

        if (string.IsNullOrWhiteSpace(document))
            throw new ArgumentException("Documento é obrigatório");

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Phone = phone;
        Document = document;
        CreatedAt = DateTime.UtcNow;
    }

    public void ChangeEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@"))
            throw new ArgumentException("Email inválido");

        Email = newEmail;
    }

    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Nome é obrigatório");

        Name = newName;
    }

    public void ChangePhone(string newPhone)
    {
        if (string.IsNullOrWhiteSpace(newPhone))
            throw new ArgumentException("Telefone é obrigatório");

        Phone = newPhone;
    }
}
