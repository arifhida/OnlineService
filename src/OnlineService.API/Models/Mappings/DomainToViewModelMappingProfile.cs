using AutoMapper;
using OnlineService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineService.API.Models.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Role, RoleViewModel>();
            Mapper.CreateMap<User, UserViewModel>().ForMember(x => x.Roles,
                r => r.MapFrom(o => o.UserRole.Select(k => k.Role)));

        }
    }
}
