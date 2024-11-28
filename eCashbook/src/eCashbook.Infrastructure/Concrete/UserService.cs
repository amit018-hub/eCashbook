using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCashbook.Core.Dto;
using eCashbook.Core.Utility;
using eCashbook.Infrastructure.Abstract;
using eCashbook.Infrastructure.Data;
using eCashbook.SharedKernel.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace eCashbook.Infrastructure.Concrete;
public class UserService:IUserService
{
 
  private readonly AppDbContext Db;
  private readonly IMapper _mapper;
  private readonly IPasswordHasher _passwordHasher;
  public UserService(AppDbContext db, IHttpContextAccessor context,  IMapper mapper, IPasswordHasher passwordHasher)
  {
    Db = db;
    _mapper = mapper;
    _passwordHasher = passwordHasher;
  }
  public async Task<Result<UserDto>> GetUser(loginDto request)
  {
    try
    {
      var user = await Db.UserCredential.Where(x => x.Username == request.Username).SingleOrDefaultAsync();
      var result = _mapper.Map<UserDto>(user);
      return Result<UserDto>.Success(result);
    }
    catch (Exception ex) { return Result<UserDto>.Failure("Error in getting User"); }
  }


}
