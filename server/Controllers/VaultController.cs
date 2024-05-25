namespace keepr.Controllers;

[ApiController]
[Route("api/vaults")]

public class VaultController : ControllerBase
{
  private readonly VaultService _vaultService;
  private readonly Auth0Provider _auth0Provider;
  private readonly KeepService _keepService;
  public VaultController(VaultService vaultService, Auth0Provider auth0Provider, KeepService keepService)
  {
    _vaultService = vaultService;
    _auth0Provider = auth0Provider;
    _keepService = keepService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault vault = _vaultService.CreateVault(vaultData);
      return Ok(vault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpGet("{vaultId}")]
  public ActionResult<Vault> GetVaultById(int vaultId)
  {
    try
    {
      Vault vault = _vaultService.GetVaultById(vaultId);
      return Ok(vault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  [Authorize]
  [HttpPut("{vaultId}")]

  public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultService.UpdateVault(vaultId, userInfo.Id, vaultData);
      return Ok(vault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{vaultId}")]
  public async Task<ActionResult<string>> EraseVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultService.EraseVault(vaultId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }

  }

  [HttpGet("{vaultId}/keeps")]
  public ActionResult<List<VaultKeepRelationship>> GetKeepsInVault(int vaultId)
  {
    try
    {
      List<VaultKeepRelationship> keeps = _keepService.GetKeepsInVault(vaultId);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


}
