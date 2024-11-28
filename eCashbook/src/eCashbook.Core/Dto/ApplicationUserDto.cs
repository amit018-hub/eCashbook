using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCashbook.Core.Dto;
public class ApplicationUserDto
{
  /// <summary>
  /// Primary key.
  /// </summary>
  public int RegId { get; set; }

  /// <summary>
  /// Email.
  /// </summary>
  public string EmailId { get; set; }

  /// <summary>
  /// First name.
  /// </summary>
  public string FirstName { get; set; }

  /// <summary>
  /// Last name.
  /// </summary>
  public string LastName { get; set; }

  /// <summary>
  /// Property for User is Enbaled or not
  /// </summary>
  public bool IsActive { get; set; }

  public string UserName { get; set; }

  public List<ApplicationUserRoleDto> UserRoles { get; set; }

  public string Password { get; set; }
}
