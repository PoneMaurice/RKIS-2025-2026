using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.UseCase.ProfileUseCases;
using Domain.Entities.ProfileEntity;
using Domain.Interfaces;
using Moq;

namespace ApplicationTests;

public class ChangeProfileUseCaseTests
{
    private readonly Mock<IProfileRepository> _mockRepository;
    private readonly Mock<IPasswordHasher> _mockHasher;
    private readonly Mock<IUserContext> _mockUserContext;
    private readonly Mock<IClock> _clock;
    public ChangeProfileUseCaseTests()
    {
        _clock = new();
        _mockRepository = new();
        _mockHasher = new();
        _mockUserContext = new();
        _clock.Setup(c => c.Now())
            .Returns(new DateTimeOffset(new(1999, 9, 9), new(9, 9), new(9, 9, 0)));
    }
    [Fact]
    public async Task Execute_EnteringAnIncorrectPassword_ErrorChangingProfile()
    {
        string passwordHash = "Hash";
        string password = "incorrectPassword";
        Guid newProfile = Guid.NewGuid();

        _mockRepository.Setup(r => r.GetByIdAsync(newProfile))
            .ReturnsAsync(new Profile(
                clock: _clock.Object,
                firstName: "testFirstName",
                lastName: "testLastName",
                dateOfBirth: _clock.Object.Now().AddYears(-19),
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
    [Fact]
    public async Task Execute_EnteringAnCorrectPassword_ChangingProfile()
    {
        string passwordHash = "Hash";
        string password = "correctPassword";
        Guid newProfile = Guid.NewGuid();

        _mockRepository.Setup(r => r.GetByIdAsync(newProfile))
            .ReturnsAsync(new Profile(
                clock: _clock.Object,
                firstName: "testFirstName",
                lastName: "testLastName",
                dateOfBirth: _clock.Object.Now().AddYears(-19),
                passwordHash: passwordHash
            ));

        _mockHasher.Setup(h => h.VerifyAsync(password, passwordHash))
            .ReturnsAsync(true);

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

            await useCase.Execute();
        }
        catch { }

        _mockUserContext.Verify(u => u.Set(It.IsAny<Guid>()), Times.Once);
    }
}