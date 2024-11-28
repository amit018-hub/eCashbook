﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eCashbook.Core.Utility;
public static class HttpContextHelper
{
  private static IHttpContextAccessor m_httpContextAccessor;

  public static HttpContext Current => m_httpContextAccessor.HttpContext;

  /// <summary>
  /// Returns base path of application
  /// </summary>
  public static string AppBaseUrl => $"{Current.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}/";

  internal static void Configure(IHttpContextAccessor contextAccessor)
  {
    m_httpContextAccessor = contextAccessor;
  }
}
