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
    [PermissionChecker(4)]
    public class EditUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IPermessionService _permessionService;

        public EditUserModel(IUserService userService,IPermessionService permessionService)
        {
            _userService = userService;
            _permessionService = permessionService;
        }

        [BindProperty]
        public EditUserForAdminViewModel EditUserViewModel { get; set; }
        public void OnGet(long id)
        {
            EditUserViewModel = _userService.ShowUserInEditMode(id); 
            ViewData["Roles"] = _permessionService.GetRoles();
        }

        public IActionResult OnPost(List<long> selectedRoles)
        {
            //if (!ModelState.IsValid)
            //{
            //   // ViewData["Roles"] = _permessionService.GetRoles();
            //    return Page();
            //}
            _userService.EditUserForAdmin(EditUserViewModel);

            //Edit Roles
            _permessionService.EditUserRoles(EditUserViewModel.UserID, selectedRoles);
            return RedirectToPage("Index");

        }
    }
}
