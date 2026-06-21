using System.ComponentModel.DataAnnotations;

namespace CustomerService.Application.Requests;

public class CreateCustomerRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Document { get; set; } = string.Empty;
}