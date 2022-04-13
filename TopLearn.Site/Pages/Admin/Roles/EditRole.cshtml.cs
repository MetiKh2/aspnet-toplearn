using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Site.Pages.Admin.Roles
{
    [PermissionChecker(9)]
    public class EditRoleModel : PageModel
    {
        private readonly IPermessionService _permessionService;
        public EditRoleModel(IPermessionService permessionService)
        {
            _permessionService = permessionService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(long id)
        {
            Role = _permessionService.GetRoleByID(id);
            ViewData["SelectedPermissions"] = _permessionService.GetRolePermissionsByRoleID(id);
            ViewData["Permissions"] = _permessionService.GetPermissions();
        }

        public IActionResult OnPost(List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _permessionService.UpdateRole(Role);

            _permessionService.EditRolePermissions(Role.RoleID, selectedPermissions);
            
            return RedirectToPage("Index");
        }
    }
}
