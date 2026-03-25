using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.UseCase.ProfileUseCases;
using Domain.Entities.ProfileEntity;
using Domain.Interfaces;
using Moq;

namespace ApplicationTests;

public class DeletionProfileUseCaseTests
{
    private readonly Mock<IProfileRepository> _mockRepository;
    private readonly Mock<IPasswordHasher> _mockHasher;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IClock> _clock;
    public DeletionProfileUseCaseTests()
    {
        _clock = new();
        _mockRepository = new();
        _mockHasher = new();
        _mockUnitOfWork = new();
        _clock.Setup(c => c.Now())
            .Returns(new DateTimeOffset(new(1999, 9, 9), new(9, 9), new(9, 9, 0)));
    }
    [Fact]
    public async Task Execute_EnteringAnIncorrectPassword_ErrorDeletionProfile()
    {
        string passwordHash = "Hash";
        string password = "incorrectPassword";
        Guid profile = Guid.NewGuid();

        _mockRepository.Setup(r => r.GetByIdAsync(profile))
            .ReturnsAsync(new Profile(
                clock: _clock.Object,
                firstName: "testFirstName",
                lastName: "testLastName",
                dateOfBirth: _clock.Object.Now().AddYears(-19),
                passwordHash: passwordHash
            ));

        _mockHasher.Setup(h => h.VerifyAsync(password, passwordHash))
            .ReturnsAsync(false);

        var repository = _mockRepository.Object;
        var hasher = _mockHasher.Object;
        var userContext = _mockUnitOfWork.Object;

        try
        {
            DeletionProfileUseCase useCase = new(
            repository: repository,
            unitOfWork: userContext,
            idProfile: profile,
            hasher: hasher,
            password: password);

            await Assert.ThrowsAsync<Exception>(() => useCase.Execute());
        }
        catch { }

        _mockRepository.Verify(u => u.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
    }
    [Fact]
    public async Task Execute_EnteringAnCorrectPassword_DeletionProfile()
    {
        string passwordHash = "Hash";
        string password = "correctPassword";
        Guid profile = Guid.NewGuid();

        _mockRepository.Setup(r => r.GetByIdAsync(profile))
            .ReturnsAsync(new Profile(
                clock: _clock.Object,
                firstName: "testFirstName",
                lastName: "testLastName",
                dateOfBirth: _clock.Object.Now().AddYears(-19),
                passwordHash: passwordHash
            ));

        _mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);

        _mockHasher.Setup(h => h.VerifyAsync(password, passwordHash))
            .ReturnsAsync(true);

        var repository = _mockRepository.Object;
        var hasher = _mockHasher.Object;
        var userContext = _mockUnitOfWork.Object;

        try
        {
            DeletionProfileUseCase useCase = new(
            repository: repository,
            unitOfWork: userContext,
            idProfile: profile,
            hasher: hasher,
            password: password);

            await useCase.Execute();
        }
        catch { }

        _mockRepository.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}