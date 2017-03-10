using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineService.Model.Entities
{
    public class UserInRole : EntityBase
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
