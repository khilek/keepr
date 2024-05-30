
namespace keepr.Services;

public class ProfileService
{
  private readonly ProfileRepository _repository;
  private readonly VaultService _vaultService;
  public ProfileService(ProfileRepository repository, VaultService vaultService)
  {
    _repository = repository;
    _vaultService = vaultService;
  }

  internal Profile GetAUsersProfile(string profileId)
  {
    Profile profile = _repository.GetAUsersProfile(profileId);
    if (profile == null)
    {
      throw new Exception($"Invalid id: {profileId}");
    }
    return profile;
  }

  internal List<Keep> GetAUsersProfileKeeps(string profileId)
  {
    List<Keep> keeps = _repository.GetAUsersProfileKeeps(profileId);

    return keeps;
  }

  internal List<Vault> GetAUsersProfileVaults(string profileId)
  {

    List<Vault> vaults = _repository.GetAUsersProfileVaults(profileId);

    return vaults.FindAll(vault => vault.IsPrivate == false);
  }
}