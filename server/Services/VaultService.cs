

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

  internal Vault GetVaultById(int vaultId)
  {
    Vault vault = _repository.GetVaultById(vaultId);
    if (vault == null)
    {
      throw new Exception($"Invalid id: {vaultId}");
    }
    return vault;
  }
}