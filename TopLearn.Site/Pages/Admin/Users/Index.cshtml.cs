using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs.UsersInAdminViewModel;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.Pages.Admin.Users
{
    [PermissionChecker(2)]
    public class IndexModel : PageModel
    {
        private readonly IUserService _usersService;
        public IndexModel(IUserService userService)
        {
            _usersService = userService;
        }

        public UserForAdminViewModel userForAdminViewModel { get; set; }
        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            userForAdminViewModel = _usersService.GetUsers(pageId,filterUserName,filterEmail);
        }
    }
}
