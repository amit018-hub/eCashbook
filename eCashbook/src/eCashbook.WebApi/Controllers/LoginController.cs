using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eCashbook.Core.ApiResponse;
using eCashbook.Core.Dto;
using eCashbook.Core.Enum;
using eCashbook.Infrastructure.Abstract;
using eCashbook.Infrastructure.Concrete;
using eCashbook.SharedKernel.Utility;
using eCashbook.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using static eCashbook.WebApi.Config.IdentityServerSetting;

namespace eCashbook.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
  private readonly IConfiguration _configuration;
  private readonly IdentityServerSettings _serverSettings;
  private readonly JwtService _jwtService;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IUserService _iuserService;
  public LoginController(
     IdentityServerSettings serverSettings,
     JwtService jwtService,
     IUserService userService,
     IPasswordHasher passwordHasher,
     IConfiguration configuration)
  {
    _serverSettings = serverSettings;
    _configuration = configuration;
    _jwtService = jwtService;
    _passwordHasher = passwordHasher;
    _iuserService = userService;
  }

  //[HttpPost]
  //public async Task<IActionResult> Post([FromBody] loginDto model)
  //{
  //  var user = await _userManager.FindByNameAsync(model.Username);
  //  if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
  //  {
  //    var roleNames = await _userManager.GetRolesAsync(user);
  //    var applicationRoles = ApplicationUserRoleExtensions.GetApplicationUserRoles().ToList();
  //    var claims = await _userManager.GetClaimsAsync(user);

  //    // Join User's roles with the application's roles.  We do this to get the ID of the role
  //    var userAppRoles = roleNames.Join(applicationRoles,
  //        roleName => roleName,
  //        appRole => appRole.Name,
  //        (roleName, appRole) => new { appRole });

  //    // Create claims for the RoleId
  //    var roleIdClaims = userAppRoles.Select(r => new Claim(ApplicationClaims.RoleId, ((int)r.appRole.Id).ToString()));

  //    // Create claims for the RoleName
  //    var roleNameClaims = userAppRoles.Select(r => new Claim(ApplicationClaims.RoleName, r.appRole.Name));

  //    var authClaims = new List<Claim>()
  //              {
  //                     new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
  //                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
  //              };

  //    // Add the roleIdClaims and roleNameClaims to the authClaims list
  //    authClaims.AddRange(claims);
  //    authClaims.AddRange(roleIdClaims);
  //    authClaims.AddRange(roleNameClaims);




  //    var token = GetToken(authClaims);

  //    return Ok(new
  //    {
  //      token = new JwtSecurityTokenHandler().WriteToken(token),
  //      expiration = token.ValidTo
  //    });
  //  }
  //  return Unauthorized();
  //}


  [HttpPost]
  public async Task<ActionResult<LoginResponse>> Post([FromBody] loginDto request)
  {
    try
    {

      if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
      {
        return BadRequest(new LoginResponse
        {
          success = false,
          message = "Username and password are required"
        });
      }

      var user = await _iuserService.GetUser(request);

      if (user.Entity == null)
      {
        return NotFound(new LoginResponse
        {
          success = false,
          message = "User not found"
        });
      }


      if (!user.Entity.IsActive || user.Entity.IsDeleted)
      {
        return BadRequest(new LoginResponse
        {
          success = false,
          message = "Account is inactive or deleted"
        });
      }


      if (!_passwordHasher.VerifyBase64Password(request.Password, user.Entity.Password))
      {
        return Unauthorized(new LoginResponse
        {
          success = false,
          message = "Invalid password"
        });
      }

      var token = _jwtService.GenerateJwtToken(user.Entity);

      return Ok(new LoginResponse
      {
        success = true,
        message = "Login successful",
        token = token
      });
    }
    catch (Exception ex)
    {
      return StatusCode(500, new LoginResponse
      {
        success = false,
        message = "An error occurred during login"
      });
    }
  }

  [HttpGet]
  [Route("Encrypt")]
  public async Task<ActionResult<ApiResponse<string>>> Enc(string password)
  {
    try
    {
      return Ok(new ApiResponse<string>
      {
        success = true,
        message = "Encrypt Password",
        data = ExternalHelper.Encrypt(password)
      });
    }
    catch(Exception ex)
    {
      return StatusCode(500, new ApiResponse<string>
      {
        success = false,
        message = ex.Message
      });
    }
  }
  [HttpGet]
  [Route("Decrypt")]
  public async Task<ActionResult<ApiResponse<string>>> Dec(string password)
  {
    try
    {
      return Ok(new ApiResponse<string>
      {
        success = true,
        message = "Decrypt Password",
        data = ExternalHelper.Decrypt(password)
      });
    }
    catch (Exception ex)
    {
      return StatusCode(500, new ApiResponse<string>
      {
        success = false,
        message = ex.Message
      });
    }
  }
  [Authorize]
  [HttpGet]
  [Route("Claim")]
  public async Task<ActionResult<ApiResponse<List<Claim>>>> Claim()
  {
    try
    {
      var claims = User.Claims.ToList();
      return Ok(new ApiResponse<List<Claim>>
      {
        success = true,
        message = "Decrypt Password",
        data = claims
      });
    }
    catch (Exception ex)
    {
      return StatusCode(500, new ApiResponse<string>
      {
        success = false,
        message = ex.Message
      });
    }
  }

}
