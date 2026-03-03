

using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.Common.DTOs;

public class RegisterUserRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required] 
    public DateTime DateOfBirth { get; set; }

    [Required] 
    public string PasswordHash { get; set; }
}
