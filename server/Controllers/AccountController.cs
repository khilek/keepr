namespace keepr.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultService _vaultService;

  public AccountController(AccountService accountService, VaultService vaultService, Auth0Provider auth0Provider)
  {
    _accountService = accountService;
    _vaultService = vaultService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpGet("{vaults}")]
  public async Task<ActionResult<List<Vault>>> GetAccountsVaults(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _vaultService.GetAccountsVaults(userInfo.Id, vaultId);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPut]
  public async Task<ActionResult<Account>> Edit([FromBody] Account editData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

      return Ok(_accountService.Edit(editData, userInfo.Email));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }



}
