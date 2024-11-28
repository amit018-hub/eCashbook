using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCashbook.Core.Dto;
using eCashbook.Core.Utility;

namespace eCashbook.Infrastructure.Abstract;
public interface IUserService
{
  Task<Result<UserDto>> GetUser(loginDto request);
}
