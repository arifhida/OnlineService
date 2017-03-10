using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineService.Model.Entities
{
    public class Role : EntityBase
    {
        public Role()
        {
            RoleUser = new List<UserInRole>();
        }
        public string RoleName { get; set; }
        public ICollection<UserInRole> RoleUser { get; set; }
    }
}
