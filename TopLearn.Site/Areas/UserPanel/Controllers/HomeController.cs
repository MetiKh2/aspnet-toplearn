using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.UserPanelViewModel;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetInformation(User.Identity.Name));
        }

        #region EditProfile
        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            return View(_userService.GetDataForEditProfile(User.Identity.Name));
        }

        [HttpPost]
        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile(EditProfileViewModel profile)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }

            _userService.EditProfile(User.Identity.Name, profile);

            //LogOut User
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login?EditProfile=true");
        }
        #endregion


        #region ChangePassword
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("UserPanel/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel change)
        {
            if (!ModelState.IsValid)
            {
                return View(change);
            }

            if (!_userService.CompareCurrentPassword(User.Identity.Name,change.CurrentPassword))
            {
                ModelState.AddModelError("CurrentPassword", "رمز عبور صحیح نمی باشد");
                return View(change);
            }

            if (change.NewPassword!=change.RePassword)
            {
                ModelState.AddModelError("CurrentPassword", "رمز عبور و تکرار آن برابر نیستند");
                return View(change);
            }

            _userService.ChangeUserPassword(User.Identity.Name,change.NewPassword);
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion
    }
}
