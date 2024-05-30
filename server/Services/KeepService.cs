namespace keepr.Services;

public class KeepService
{
  private readonly KeepRepository _repository;
  private readonly VaultService _vaultService;
  public KeepService(KeepRepository repository, VaultService vaultService)
  {
    _repository = repository;
    _vaultService = vaultService;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    Keep keep = _repository.CreateKeep(keepData);
    return keep;
  }

  internal string DestroyKeep(int keepId, string userId)
  {
    Keep KeepToDestroy = GetKeepById(keepId);
    if (KeepToDestroy.CreatorId != userId)
    {
      throw new Exception("NOT YOUR KEEP TO DESTROY");
    }
    _repository.DestroyKeep(keepId);
    return "KEEP has been DESTROYED";
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repository.GetAllKeeps();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repository.GetKeepById(keepId);
    if (keep == null)
    {
      throw new Exception($"Invalid id: {keepId}");
    }
    return keep;
  }

  internal List<VaultKeepRelationship> GetKeepsInVault(int vaultId, string userId)
  {

    _vaultService.GetVaultById(vaultId, userId);
    List<VaultKeepRelationship> keeps = _repository.GetKeepsInVault(vaultId);

    return keeps;
  }

  internal Keep UpdateKeep(int keepId, string userId, Keep keepData)
  {
    Keep KeepToUpdate = GetKeepById(keepId);
    if (KeepToUpdate.CreatorId != userId)
    {
      throw new Exception("You are not the creator of this KEEP");
    }

    KeepToUpdate.Description = keepData.Description ?? KeepToUpdate.Description;
    KeepToUpdate.Name = keepData.Name ?? KeepToUpdate.Name;


    Keep UpdatedKeep = _repository.UpdateKeep(KeepToUpdate);

    return KeepToUpdate;


  }

  internal Keep IncrementViews(int keepId)
  {
    _repository.IncrementViews(keepId);

    Keep keep = GetKeepById(keepId);

    return keep;
  }


}