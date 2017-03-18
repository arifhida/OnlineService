using AutoMapper;
using OnlineService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineService.API.Models.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile 
    {
        protected override void Configure()
        {
            Mapper.CreateMap<UserViewModel, User>();
            Mapper.CreateMap<RoleViewModel, Role>();
        }
    }
}
