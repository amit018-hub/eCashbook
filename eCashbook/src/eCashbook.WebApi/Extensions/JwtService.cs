using eCashbook.Core.Dto;
using eCashbook.Core.Enum;
using eCashbook.Core.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static eCashbook.WebApi.Config.IdentityServerSetting;

namespace eCashbook.WebApi.Extensions;

public class JwtService
{
  private readonly IConfiguration _configuration;
  private readonly IdentityServerSettings _serverSettings;
  //private readonly UserManager<ApplicationUser> _userManager;
  public JwtService(
     IConfiguration configuration, IdentityServerSettings serverSettings)
  {
    _serverSettings = serverSettings;
    _configuration = configuration;
  }
  public string GenerateJwtToken(UserDto user)
  {
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("IdentityServerSettings:ApiSecret")));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


    //var roleNames = await _userManager.GetRolesAsync(user);
    //var applicationRoles = ApplicationUserRoleExtensions.GetApplicationUserRoles().ToList();
    //var claims = await _userManager.GetClaimsAsync(user);

    //// Join User's roles with the application's roles.  We do this to get the ID of the role
    //var userAppRoles = roleNames.Join(applicationRoles,
    //    roleName => roleName,
    //    appRole => appRole.Name,
    //    (roleName, appRole) => new { appRole });

    //// Create claims for the RoleId
    //var roleIdClaims = userAppRoles.Select(r => new Claim(ApplicationClaims.RoleId, ((int)r.appRole.Id).ToString()));

    //// Create claims for the RoleName
    //var roleNameClaims = userAppRoles.Select(r => new Claim(ApplicationClaims.RoleName, r.appRole.Name));

    var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
            new Claim("RegId", user.RegId.ToString()),
            new Claim("IsActive", user.IsActive.ToString())
        };

    var token = new JwtSecurityToken(
        issuer: _serverSettings.ValidIssuer,
        audience: _serverSettings.ValidAudience,
        claims: claims,
        expires: DateTime.Now.AddMinutes(_serverSettings.Expiry),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
