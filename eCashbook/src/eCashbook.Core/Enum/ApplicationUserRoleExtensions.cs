﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCashbook.Core.Dto;

namespace eCashbook.Core.Enum;
public static class ApplicationUserRoleExtensions
{
  private static readonly Dictionary<ApplicationUserRole, string> FriendlyNames = new Dictionary<ApplicationUserRole, string>
            {
                // NOTE: These names must match exactly with the names in the database.
                { ApplicationUserRole.None, "None" },
                { ApplicationUserRole.SuperAdmin, "SuperAdmin" },
                { ApplicationUserRole.Admin, "Admin" },
                { ApplicationUserRole.AccountOfficer, "AccountOfficer" },
                { ApplicationUserRole.Vendor, "Vendor" }
            };

  public static IEnumerable<ApplicationUserRoleDto> GetApplicationUserRoles()
  {
    return FriendlyNames
        .Where(x => x.Key != ApplicationUserRole.None)
        .OrderBy(x => x.Value)
        .Select(x => new ApplicationUserRoleDto { Id = x.Key, Name = x.Value });
  }

  public static string ToFriendlyName(this ApplicationUserRole role)
  {
    if (!FriendlyNames.TryGetValue(role, out var friendlyName))
    {
      throw new ArgumentOutOfRangeException(nameof(role), $"{nameof(role)} has no friendly name.");
    }

    return friendlyName;
  }
}
