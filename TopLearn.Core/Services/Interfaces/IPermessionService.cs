using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Permission;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
   public interface IPermessionService
    {

        #region Roles
        List<Role> GetRoles();
        void AddUserRoles(long userId,List<long> rolesId);
        void EditUserRoles(long userId, List<long> rolesId);
        long AddRole(Role Role);
        Role GetRoleByID(long roleId);
        void UpdateRole(Role role);
        void RemoveRole(Role role);

        #endregion

        #region Permissions 
        List<Permission> GetPermissions();
        void AddPermissionsToRole(long roleId,List<int> permissionsId);
        List<int> GetRolePermissionsByRoleID(long roleId);
        void EditRolePermissions(long roleId,List<int> permissionsId);

        bool CheckPermission(int permissionId, string userName);
        #endregion
    }
}
