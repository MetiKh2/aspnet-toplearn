using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.DTOs.AccountViewModels;
using TopLearn.Core.Generators;
using TopLearn.Core.Security;
using TopLearn.Core.Senders;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.Controllers
{
    public class AccountController : Controller
    {
        IViewRenderService _viewRenderService;
        private readonly IUserService _userService;
        
        public AccountController(IUserService userService,IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
            _userService = userService;
        }
        #region Register


        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری تکراری می باشد");
                return View(register);
            }
            if (_userService.IsExistEmail(register.Email))
            {
                ModelState.AddModelError("Email", "ایمیل تکراری می باشد");
                return View(register);
            }
            if (register.Password != register.RePassword)
            {
                ModelState.AddModelError("Password", "رمز عبور و تکرار رمز عبور یکسان نیستند");
                return View(register);
            }
            DataLayer.Entities.User.User user = new DataLayer.Entities.User.User
            {

                Email = FixText.FixEmail(register.Email),
                UserName = register.UserName,
                Password = MyPasswordHasher.EncodePasswordMd5(register.Password),
                IsActive = false,
                UserAvatar = "Defult.jpg",
                ActiveCode = NameGenerator.GenerateUniqCode(),
                RegisterTime = DateTime.Now
            };

            _userService.AddUser(user);
            #region Send Email Activation Code
            string emailBody = _viewRenderService.RenderToStringAsync("_ActiveEmail",user);
            SendEmail.Send(user.Email, "فعالسازی", emailBody);
            #endregion

            return View("SuccessRegister", user);
        }
        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login(bool EditProfile=false)
        {
            ViewBag.EditProfile = EditProfile;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userService.LoginUser(login.Email, login.Password);
            if (user == null)
            {
                ModelState.AddModelError("Password", "کاربری با این مشخصات در سایت ثبت نام نکرده است .");
            }
            else
            {
                if (user.IsActive)
                {
                    var Claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName),

                    };
                    var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.ReMemberMe
                    };
                    HttpContext.SignInAsync(principal, properties);
                    ViewBag.IsSuccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Password", "این حساب کاربری فعال نشده است");
                }
            }

            return View();
        }
        #endregion


        #region ActiveUser
        public IActionResult ActiveUser(string id)
        {
            ViewBag.IsActive = _userService.ActiveUser(id);
            return View();
        }
        #endregion

        #region LogOut
        [Route("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/login");
        }
        #endregion

        #region ForgotPassword
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [Route("ForgotPassword")]
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
                return View(forgot);

            string fixedEmail = FixText.FixEmail(forgot.Email);
            var user = _userService.GetUserByEmail(fixedEmail);
            if (user==null)
            {
                ModelState.AddModelError("Email","کاربری با این ایمیل یافت نشد");
                return View(forgot);
            }
            string bodyEmail = _viewRenderService.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.Send(user.Email, "بازیابی حساب کاربری", bodyEmail);
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion

        #region ResetPassword
        
        public IActionResult ResetPassword (string id)
        {
            return View(new ResetPasswordViewModel { ActivationCode=id});
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = _userService.GetUserByActiveCode(resetPassword.ActivationCode);
            if (user == null)
                return NotFound();
            if (resetPassword.Password!=resetPassword.RePassword)
            {
                ModelState.AddModelError("RePassword","رمز عبور و تکرار آن یکسان نیستند");
                return View(resetPassword);
            }

            var hashPassword = MyPasswordHasher.EncodePasswordMd5(resetPassword.Password);
            user.Password = hashPassword;
            _userService.UpdateUser(user);

            return Redirect("/Login");

        }
        #endregion
    }
}

