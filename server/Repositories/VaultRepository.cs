
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





}