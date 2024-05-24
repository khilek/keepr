
namespace keepr.Services;

public class VaultKeepService
{
  private readonly VaultKeepRepository _repository;

  public VaultKeepService(VaultKeepRepository repository)
  {
    _repository = repository;
  }
  internal VaultKeep CreateVaultKeep(int vaultId, int keepId, Profile userInfo)
  {
    VaultKeep vaultKeeps = _repository.CreateVaultKeep(vaultId, keepId, userInfo.Id);
    return vaultKeeps;
  }
}

