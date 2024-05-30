

namespace keepr.Services;

public class VaultKeepService
{
  private readonly VaultKeepRepository _repository;
  private readonly VaultService _vaultService;
  public VaultKeepService(VaultKeepRepository repository, VaultService vaultService)
  {
    _repository = repository;
    _vaultService = vaultService;
  }
  internal VaultKeep CreateVaultKeep(int vaultId, int keepId, Profile userInfo)
  {
    Vault vault = _vaultService.GetVaultById(vaultId, userInfo.Id);
    if (vault.CreatorId != userInfo.Id)
    {
      throw new Exception("FAIL");
    }

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

