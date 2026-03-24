using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.UseCase.ProfileUseCases;
using Domain.Entities.ProfileEntity;
using Moq;

namespace ApplicationTests;

public class ChangeProfileUseCaseTests
{
    private readonly Mock<IProfileRepository> _mockRepository;
    private readonly Mock<IPasswordHasher> _mockHasher;
    private readonly Mock<IUserContext> _mockUserContext;
    public ChangeProfileUseCaseTests()
    {
        _mockRepository = new();
        _mockHasher = new();
        _mockUserContext = new();
    }
    [Fact]
    public async Task Execute_EnteringAnIncorrectPassword_Throws()
    {
        string passwordHash = "Hash";
        string password = "incorrectPassword";
        Guid newProfile = Guid.NewGuid();

        _mockRepository.Setup(r => r.GetByIdAsync(newProfile))
            .ReturnsAsync(new Profile(
                firstName: "testFirstName",
                lastName: "testLastName",
                dateOfBirth: new(1999, 03, 20),
                passwordHash: passwordHash
            ));

        _mockHasher.Setup(h => h.VerifyAsync(password, passwordHash))
            .ReturnsAsync(false);

        _mockUserContext.Setup(u => u.Set(It.IsAny<Guid>()));

        var repository = _mockRepository.Object;
        var hasher = _mockHasher.Object;
        var userContext = _mockUserContext.Object;

        try
        {
            ChangeProfileUseCase useCase = new(
            repository: repository,
            hasher: hasher,
            userContext: userContext,
            newProfile: newProfile,
            password: password);

            await Assert.ThrowsAsync<Exception>(() => useCase.Execute());
        }
        catch { }

        _mockUserContext.Verify(u => u.Set(It.IsAny<Guid>()), Times.Never);
    }
}