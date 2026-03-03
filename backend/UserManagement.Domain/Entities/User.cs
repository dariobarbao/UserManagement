using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities;

public sealed class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    public string PasswordHash { get; set; }
}