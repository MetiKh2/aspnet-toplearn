using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.UserPanelViewModel;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.Pages.Admin.Users
{
    [PermissionChecker(5)]
    public class RemoveUserModel : PageModel
    {
        private readonly IUserService _userService;
        public RemoveUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public InformationUserViewModel InformationUserViewModel { get; set; }
        public void OnGet(long id)
        {
            ViewData["UserId"] = id;
            InformationUserViewModel = _userService.GetInformation(id);
        }

        public IActionResult OnPost(long userId)
        {
            _userService.DeleteUser(userId);
            return RedirectToPage("Index");
        }
    }
}
