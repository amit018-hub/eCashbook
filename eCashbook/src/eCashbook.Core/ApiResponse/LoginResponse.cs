using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCashbook.Core.ApiResponse;
public class LoginResponse
{
  public bool success { get; set; }
  public string message { get; set; } = string.Empty;
  public string token { get; set; } = string.Empty;
}
