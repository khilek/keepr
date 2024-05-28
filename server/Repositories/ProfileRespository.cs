


namespace keepr.Repositories;

public class ProfileRepository
{

  private readonly IDbConnection _db;

  public ProfileRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Profile GetAUsersProfile(string profileId)
  {
    string sql = @"
        SELECT
        accounts.*
        FROM accounts
        WHERE id = @profileId;";

    Profile profile = _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();

    return profile;
  }

  internal List<Keep> GetAUsersProfileKeeps(string profileId)
  {
    string sql = @" 
        SELECT 
        keep.*,
        accounts.*
        FROM keep
        JOIN accounts ON accounts.id = keep.creatorId
        WHERE keep.creatorId = @profileId;";

    var keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { profileId }).ToList();
    return keeps;
  }

  internal List<Vault> GetAUsersProfileVaults(string profileId)
  {
    string sql = @" 
        SELECT 
        vault.*,
        accounts.*
        FROM vault
        JOIN accounts ON accounts.id = vault.creatorId
        WHERE vault.creatorId = @profileId;";

    var vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
  {
    vault.Creator = profile;
    return vault;
  }, new { profileId }).ToList();
    return vaults;
  }
}