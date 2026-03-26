using Domain.Entities.ProfileEntity;
using Infrastructure;
using Infrastructure.EfRepository;

namespace InfrastructureTests;

public class EfProfileRepositoryTests
{
    private DatabaseCollection? _databaseCollection;
    private EfProfileRepository? _profileRepository;
    private EfUnitOfWork? _unitOfWork;
    [Fact]
    public async Task AddAsync_AddingNewUser_UserAddedSuccessfully()
    {
        using (_databaseCollection = new())
        {
            _profileRepository = new(_databaseCollection.TodoContext);
            _unitOfWork = new(_databaseCollection.TodoContext);
            Profile profile = new(
                clock: new Clock(),
                "test",
                "test",
                new Clock().Now().AddYears(-19),
                "password"
            );
            await _profileRepository.AddAsync(profile);
            await _unitOfWork.SaveChangesAsync();
            Assert.True(_databaseCollection.TodoContext.Profiles.First(p => p.DateOfBirth == profile.DateOfBirth).CreatedAt == profile.CreatedAt);
        }
    }
}