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
    [PermissionChecker(8)]
    public class RemoveRoleModel : PageModel
    {
        private readonly IPermessionService _permessionService;
        public RemoveRoleModel(IPermessionService permessionService)
        {
            _permessionService = permessionService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(long id)
        {
            Role = _permessionService.GetRoleByID(id);
        }

        public IActionResult OnPost()
        {
            _permessionService.RemoveRole(Role);
            return RedirectToPage("Index");
        }
    }
}
