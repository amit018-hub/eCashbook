using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCashbook.Core.Dto;
using eCashbook.Core.Entity;

namespace eCashbook.Infrastructure.AutoMapper;
public class AutoMapperProfile : Profile
{
  public AutoMapperProfile()
  {
    CreateMap<FinMaster, FinMasterDto>().ReverseMap();
    CreateMap<UserCredential, UserDto>().ReverseMap();
  }
}
