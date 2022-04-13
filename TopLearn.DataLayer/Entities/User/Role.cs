using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Permission;

namespace TopLearn.DataLayer.Entities.User
{
  public  class Role
    {
        public Role()
        {

        }

        [Key]
        public long RoleID { get; set; }

        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string RoleTitle { get; set; }

        public bool IsRemoved { get; set; }

        #region Relations
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
