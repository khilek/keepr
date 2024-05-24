
namespace keepr.Repositories;

public class VaultKeepRepository
{
  private readonly IDbConnection _db;

  public VaultKeepRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep CreateVaultKeep(int vaultId, int keepId, string creatorId)
  {
    string sql = @"
    INSERT INTO 
    vaultkeep(keepId, vaultId, creatorId)
    VALUES(@keepId, @vaultId, @creatorId);

    SELECT * FROM vaultkeep WHERE id = LAST_INSERT_ID();
    ";

    /**!SECTION
    SELECT
    keep.*,
    vaultkeep.*,
    accounts.*
    FROM vaultkeep
    JOIN keep on vaultkeep.keepId = keep.id
    JOIN accounts on accounts.id = vaultkeep.creatorId
    WHERE vaultkeep.id = LAST_INSERT_ID();
    */

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultId, keepId, creatorId }).FirstOrDefault();

    return vaultKeep;
  }
}