
using System.Reflection.Metadata.Ecma335;

namespace keepr.Repositories;

public class VaultRepository
{
  private readonly IDbConnection _db;

  public VaultRepository(IDbConnection db)
  {
    _db = db;
  }
  private Vault PopulateCreator(Vault vault, Profile profile)
  {
    vault.Creator = profile;
    return vault;
  }
  internal Vault CreateVault(Vault vaultData)
  {
    string sql = @"
        INSERT INTO
        vault(name, description, img, isPrivate, creatorId)
        VALUES(@Name, @Description, @Img, @IsPrivate, @CreatorId);

        SELECT
        vault.*,
        accounts.*
        FROM vault
        JOIN accounts ON accounts.id = vault.creatorId
        WHERE vault.Id = LAST_INSERT_ID();";

    Vault vault = _db.Query<Vault, Profile, Vault>(sql, PopulateCreator, vaultData).FirstOrDefault();
    return vault;
  }

  internal Vault GetVaultById(int vaultId)
  {
    string sql = @"
        SELECT
        vault.*,
        accounts.*
        FROM vault
        JOIN accounts ON accounts.id = vault.creatorId
        WHERE vault.id = @vaultId;";

    Vault vault = _db.Query<Vault, Profile, Vault>(sql, PopulateCreator, new { vaultId }).FirstOrDefault();

    return vault;
  }

  internal Vault UpdateVault(Vault VaultToUpdate)
  {
    string sql = @"
        UPDATE vault
        SET
        name = @Name,
        isPrivate = @IsPrivate
        WHERE id = @Id;

        SELECT
        vault.*,
        accounts.*
        FROM vault
        JOIN accounts ON accounts.id = vault.creatorId
        WHERE vault.id = @Id;";

    Vault vault = _db.Query<Vault, Profile, Vault>(sql, PopulateCreator, VaultToUpdate).FirstOrDefault();

    return vault;
  }

  internal void EraseVault(int vaultId)
  {
    string sql = "DELETE FROM vault WHERE id = @vaultId;";

    _db.Execute(sql, new { vaultId });
  }

  internal List<Vault> GetAccountsVaults(string userId, int vaultId)
  {
    string sql = @"
    SELECT 
    vault.*,
    accounts.*
    FROM vault
    JOIN accounts ON accounts.id = vault.creatorId
    WHERE vault.creatorId = @userId;";
    List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { userId, vaultId }).ToList();

    return vaults;
  }
}