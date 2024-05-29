

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

  internal string EraseVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeepRelationship eraseVK = GetVaultKeepById(vaultKeepId);

    if (eraseVK.CreatorId != userId)
    {
      throw new Exception("NOT your VAULTKEEP to DELETE");
    }
    _repository.EraseVaultKeep(vaultKeepId);

    return "NO LONGER A VAULTKEEP";
  }




  internal VaultKeepRelationship GetVaultKeepById(int vaultKeepId)
  {
    VaultKeepRelationship vaultKeepRelationship = _repository.GetVaultKeepById(vaultKeepId);
    if (vaultKeepRelationship == null)
    {
      throw new Exception($"invalid id: {vaultKeepId}");
    }
    return vaultKeepRelationship;
  }





}

