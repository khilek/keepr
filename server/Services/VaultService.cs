


namespace keepr.Services;

public class VaultService
{
  private readonly VaultRepository _repository;

  public VaultService(VaultRepository repository)
  {
    _repository = repository;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repository.CreateVault(vaultData);
    return vault;
  }

  internal string EraseVault(int vaultId, string userId)
  {
    Vault VaultToErase = GetVaultById(vaultId, userId);

    if (VaultToErase.CreatorId != userId)
    {
      throw new Exception("NOT YOUR VAULT TO ERASE");
    }
    _repository.EraseVault(vaultId);
    return "VAULT has been ERASED";
  }

  internal List<Vault> GetAccountsVaults(string userId, int vaultId)
  {
    List<Vault> vaults = _repository.GetAccountsVaults(userId, vaultId);
    return vaults;
  }

  internal Vault GetVaultById(int vaultId, string userId)
  {
    Vault vault = _repository.GetVaultById(vaultId);
    //TODO Reference restaurants for checks on private - 


    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception($"Invalid id: {vaultId} 😉");
    }
    return vault;
  }

  internal Vault UpdateVault(int vaultId, string userId, Vault vaultData)
  {
    Vault VaultToUpdate = GetVaultById(vaultId, userId);
    if (VaultToUpdate.CreatorId != userId)
    {
      throw new Exception("You are not the creator of this Vault");
    }
    VaultToUpdate.Name = vaultData.Name ?? VaultToUpdate.Name;
    VaultToUpdate.IsPrivate = vaultData.IsPrivate ?? VaultToUpdate.IsPrivate;

    Vault updatedVault = _repository.UpdateVault(VaultToUpdate);
    return VaultToUpdate;
  }
}