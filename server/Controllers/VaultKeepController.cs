namespace keepr.Controllers;

[ApiController]
[Route("api/vaultkeeps")]

public class VaultKeepController : ControllerBase
{
  private readonly VaultKeepService _vaultKeepService;
  private readonly Auth0Provider _auth0Provider;

  public VaultKeepController(VaultKeepService vaultKeepService, Auth0Provider auth0Provider)
  {
    _vaultKeepService = vaultKeepService;
    _auth0Provider = auth0Provider;
  }


  [Authorize]
  [HttpPost]

  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepData.CreatorId = userInfo.Id;
      var profile = _vaultKeepService.CreateVaultKeep(vaultKeepData.VaultId, vaultKeepData.KeepId, userInfo);
      return Ok(profile);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }









}