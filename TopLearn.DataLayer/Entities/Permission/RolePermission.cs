using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Permission
{
   public class RolePermission
    {
        [Key]
        public int RP_ID { get; set; }

        public long RoleID { get; set; }
        public int PermissionID { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }

    }
}
