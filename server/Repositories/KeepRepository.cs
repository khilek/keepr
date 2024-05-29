






namespace keepr.Repositories;

public class KeepRepository
{

  private readonly IDbConnection _db;

  public KeepRepository(IDbConnection db)
  {
    _db = db;
  }
  private Keep PopulateCreator(Keep keep, Profile profile)
  {
    keep.Creator = profile;
    return keep;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"
        INSERT INTO
        keep(name, description, img, views, creatorId)
        VALUES(@Name, @Description, @Img, @Views, @CreatorId);
        
        SELECT
        keep.*,
        accounts.*
        FROM keep
        JOIN accounts ON accounts.id = keep.creatorId
        WHERE keep.id = LAST_INSERT_ID();";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator, keepData).FirstOrDefault();
    return keep;
  }
  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT
    keep.*,
    accounts.*
    FROM keep
    JOIN accounts ON keep.creatorId = accounts.id;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator).ToList();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
        SELECT
        keep.*,
        accounts.*
        FROM keep
        JOIN accounts ON accounts.id = keep.creatorId
        WHERE keep.id = @keepId;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator, new { keepId }).FirstOrDefault();

    return keep;
  }

  internal Keep UpdateKeep(Keep KeepToUpdate)
  {
    string sql = @"
        UPDATE keep
        SET
        description = @Description,
        name = @Name
        WHERE id = @Id;

        SELECT
        keep.*,
        accounts.*
        FROM keep
        JOIN accounts ON accounts.id = keep.creatorId
        WHERE keep.id = @Id;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator, KeepToUpdate).FirstOrDefault();
    return keep;
  }

  internal void DestroyKeep(int keepId)
  {
    string sql = "DELETE FROM keep WHERE id = @keepId;";

    _db.Execute(sql, new { keepId });
  }

  internal List<VaultKeepRelationship> GetKeepsInVault(int vaultId)
  {
    string sql = @"
        SELECT
        vaultkeep.*,
        keep.*,
        accounts.*
        FROM vaultkeep
        JOIN keep ON vaultkeep.keepId = keep.id
        JOIN accounts on accounts.id = vaultkeep.creatorId
        WHERE vaultkeep.vaultId = @vaultId;
        ";

    List<VaultKeepRelationship> vaultKeeps = _db.Query<VaultKeep, VaultKeepRelationship, Profile, VaultKeepRelationship>(sql, (VaultKeep vaultKeep, VaultKeepRelationship vk, Profile profile) =>
    {
      vk.vaultKeepId = vaultKeep.Id;
      vk.Creator = profile;
      return vk;
    }, new { vaultId }).ToList();
    return vaultKeeps;
  }

  internal void IncrementViews(int keepId)
  {
    string sql = @"UPDATE keep SET views = views + 1 WHERE id = @keepId LIMIT 1;";

    _db.Execute(sql, new { keepId });
  }
}