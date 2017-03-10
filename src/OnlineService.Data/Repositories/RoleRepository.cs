using OnlineService.Data.Abstracts;
using OnlineService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineService.Data.Repositories
{
    public class RoleRepository : EntityBaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(OnlineServiceContext context) : base(context)
        {
        }
    }
}
