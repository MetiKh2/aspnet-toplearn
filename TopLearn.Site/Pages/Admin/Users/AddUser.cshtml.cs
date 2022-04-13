using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.UserInAdminViewModels;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.Pages.Admin.Users
{
    [PermissionChecker(3)]
    public class AddUserModel : PageModel
    {
        private readonly IPermessionService _permessionService;
        private readonly IUserService _userService;
        public AddUserModel(IPermessionService permessionService, IUserService userService)
        {
            _permessionService = permessionService;
            _userService = userService;
        }
        [BindProperty]
        public AddUserViewModel AddUserViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permessionService.GetRoles();
        }

        public IActionResult OnPost(List<long> selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = _userService.AddUserForAdmin(AddUserViewModel);

            _permessionService.AddUserRoles(userId,selectedRoles);

            return Redirect("/Admin/Users");
        }
    }
}
