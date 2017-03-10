using OnlineService.Data.Abstracts;
using OnlineService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineService.Data.Repositories
{
    public class UserInRoleRepository : EntityBaseRepository<UserInRole>, IUserInRoleRepository
    {
        public UserInRoleRepository(OnlineServiceContext context) : base(context)
        {
        }
    }
}
