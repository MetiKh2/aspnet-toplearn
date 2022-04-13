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
    [PermissionChecker(7)]
    public class AddRoleModel : PageModel
    {
        private readonly IPermessionService _permessionService;
        public AddRoleModel(IPermessionService permessionService)
        {
            _permessionService = permessionService;
        }

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet()
        {
            ViewData["Permissions"] = _permessionService.GetPermissions();
        }

        public IActionResult OnPost(List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
          long roleID=  _permessionService.AddRole(Role);

            _permessionService.AddPermissionsToRole(roleID,selectedPermissions);

            return RedirectToPage("Index");
        }
    }
}
