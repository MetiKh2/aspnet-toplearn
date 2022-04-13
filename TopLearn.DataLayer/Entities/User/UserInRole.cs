using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.User
{
  public  class UserInRole
    {
        public UserInRole()
        {

        }
        [Key]
        public long UR_ID { get; set; }

        public long UserID { get; set; }
        public long RoleID { get; set; }

        #region Relations
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        #endregion
    }
}
