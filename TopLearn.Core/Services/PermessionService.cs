using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Permission;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public class PermessionService : IPermessionService
    {
        private readonly TopLearnContext _context;
        public PermessionService(TopLearnContext topLearnContext)
        {
            _context = topLearnContext;
        }

        public void AddPermissionsToRole(long roleId, List<int> permissionsId)
        {
            foreach (var item in permissionsId)
            {
                _context.RolePermissions.Add(new RolePermission { 
               RoleID=roleId,
               PermissionID=item
               });
            }
            _context.SaveChanges();
        }

        public long AddRole(Role Role)
        {
            Role.IsRemoved = false;
            _context.Roles.Add(Role);
            _context.SaveChanges();
            return Role.RoleID;
        }

        public void AddUserRoles(long userId, List<long> rolesId)
        {
            foreach (var item in rolesId)
            {
                UserInRole userRole = new UserInRole
                { 
               RoleID=item,
               UserID=userId
                };
                _context.UserInRoles.Add(userRole);
            }

            _context.SaveChanges();
        }

        public void EditUserRoles(long userId, List<long> rolesId)
        {
            //Delete All Roles User
            _context.UserInRoles.Where(r => r.UserID == userId).ToList().ForEach(r => _context.UserInRoles.Remove(r));

            //Add New Roles
            AddUserRoles(userId,rolesId);
        }

        public List<Permission> GetPermissions()
        {
            return _context.Permissions.ToList();
        }

        public List<int> GetRolePermissionsByRoleID(long roleId)
        {
            return _context.RolePermissions.Where(p => p.RoleID == roleId).Select(p => p.PermissionID).ToList();
        }

        public Role GetRoleByID(long roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public void RemoveRole(Role role)
        {
            role.IsRemoved = true;
            UpdateRole(role);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public void EditRolePermissions(long roleId, List<int> permissionsId)
        {
            //Delete All permissons role
            _context.RolePermissions.Where(r => r.RoleID == roleId).ToList().ForEach(r => _context.RolePermissions.Remove(r));

            //addPermissions
            AddPermissionsToRole(roleId,permissionsId);
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            long userId = _context.Users.Single(u => u.UserName == userName).UserID;

            List<long> UserRoles = _context.UserInRoles.Where(p => p.UserID == userId).Select(p => p.RoleID).ToList();

            if (!UserRoles.Any())
            {
                return false;
            }

            List<long> RolePermissions = _context.RolePermissions.Where(p => p.PermissionID == permissionId).Select(p => p.RoleID).ToList();

            return RolePermissions.Any(p=>UserRoles.Contains(p));
        }
    }
}
