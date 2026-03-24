using Application.Interfaces;
using Application.Interfaces.Command;
using Application.Interfaces.Repository;
using Domain.Entities.ProfileEntity;

namespace Application.UseCase.ProfileUseCases;

public class ChangeProfileUseCase : ICommandWithUndo
{
	private readonly IProfileRepository _repo;
	private readonly IPasswordHasher _hasher;
	private readonly Profile _newProfile;
	private readonly Guid? _oldProfileId;
	private readonly IUserContext _userContext;
	private readonly bool _verifyPassword;
	public ChangeProfileUseCase(
		IProfileRepository repository,
		IPasswordHasher hasher,
		IUserContext userContext,
		Guid newProfile,
		string password
		)
	{
		_repo = repository;
		_hasher = hasher;
		_userContext = userContext;
		_oldProfileId = _userContext.UserId;
		_newProfile = _repo.GetByIdAsync(newProfile).Result
			?? throw new Exception("This profile does not exist.");
		_verifyPassword = _hasher.VerifyAsync(
			password: password,
			hashedPassword: _newProfile.PasswordHash).Result;
		if (!_verifyPassword)
		{
			throw new Exception(message: "Incorrect password.");
		}
	}
	public async Task Execute()
	{
		_userContext.Set(_newProfile.ProfileId);
	}

	public async Task Undo()
	{
		_userContext.Set(_oldProfileId);
	}
}