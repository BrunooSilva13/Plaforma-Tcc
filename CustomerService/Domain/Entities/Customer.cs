using System;
using CustomerService.Domain.Interfaces;

namespace CustomerService.Domain.Entities;

public class Customer
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string Phone { get; private set; }

    public string Document { get; private set; }

    public DateTime CreatedAt { get; private set; }



    public Customer(
        string name,
        string email,
        string phone,
        string document)
    {
        ValidateName(name);
        ValidateEmail(email);
        ValidateDocument(document);

        Id = Guid.NewGuid();

        Name = name;
        Email = email;
        Phone = phone;
        Document = document;
        CreatedAt = DateTime.UtcNow;
    }


    // Construtor para reconstruir do banco
    public Customer(
        Guid id,
        string name,
        string email,
        string phone,
        string document,
        DateTime createdAt)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        Document = document;
        CreatedAt = createdAt;
    }


    public void UpdateName(string name)
    {
        ValidateName(name);

        Name = name;
    }


    public void UpdateEmail(string email)
    {
        ValidateEmail(email);

        Email = email;
    }


    public void UpdatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone is required");

        Phone = phone;
    }



    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");


        if (name.Length < 3)
            throw new ArgumentException("Name must have at least 3 characters");
    }



    private static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");


        if (!email.Contains("@"))
            throw new ArgumentException("Invalid email");
    }



    private static void ValidateDocument(string document)
    {
        if (string.IsNullOrWhiteSpace(document))
            throw new ArgumentException("Document is required");
    }
}