namespace keepr.Controllers;

[ApiController]
[Route("api/profiles")]

public class ProfileController : ControllerBase
{
  private readonly ProfileService _profileService;
  private readonly VaultService _vaultService;
  private readonly Auth0Provider _auth0Provider;
  private readonly KeepService _keepService;
  public ProfileController(ProfileService profileService, VaultService vaultService, Auth0Provider auth0Provider, KeepService keepService)
  {
    _profileService = profileService;
    _auth0Provider = auth0Provider;
    _vaultService = vaultService;
    _keepService = keepService;
  }


  [HttpGet("{profileId}")]
  public async Task<ActionResult<Profile>> GetAUsersProfile(string profileId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Profile profile = _profileService.GetAUsersProfile(profileId);
      return Ok(profile);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  [HttpGet("{profileId}/keeps")]

  public async Task<ActionResult<Keep>> GetAUsersProfileKeeps(string profileId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> keeps = _profileService.GetAUsersProfileKeeps(profileId);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]

  public async Task<ActionResult<Vault>> GetAUsersProfileVaults(string profileId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _profileService.GetAUsersProfileVaults(profileId);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }




}
