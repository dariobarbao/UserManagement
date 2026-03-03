

using UserManagement.Application.Common.Security;
using UserManagement.Application.Repositories;
using UserManagement.Domain.Entities;

namespace UserManagement.Persistence.Seed;

public class DbSeeder
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public DbSeeder(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        const string adminEmail = "admin@mail.com";

        var exists = await _userRepository.ExistsByEmailAsync(adminEmail, cancellationToken);
        if (exists)
            return;

        var admin = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Admin",
            LastName = "User",
            Email = adminEmail,
            PhoneNumber = "0000000000",
            DateOfBirth = new DateTime(1990, 1, 1),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            PasswordHash = _passwordHasher.Hash("Admin123!")
        };

        _userRepository.Create(admin);
        await _unitOfWork.Save(cancellationToken);
    }
}
