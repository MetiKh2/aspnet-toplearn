using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.WalletViewModels;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly IUserService _userService;
        public WalletController(IUserService userService)
        {
            _userService = userService;
        }


        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            ViewBag.WalletList = _userService.GetUserWallets(User.Identity.Name);
            return View();
        }
        [HttpPost]
        [Route("UserPanel/Wallet")]
        public IActionResult Index(ChargeWalletViewModel charge)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.WalletList = _userService.GetUserWallets(User.Identity.Name);
                return View(charge);
            }

          long walletId=  _userService.ChargeWallet(User.Identity.Name,charge.Amount,"شارژ حساب");

            #region Online payment

            var payment = new ZarinpalSandbox.Payment((int)charge.Amount);

            var res = payment.PaymentRequest("شارژ کیف پول", "https://localhost:44393/OnlinePayment/" + walletId);

            if (res.Result.Status==100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/"+res.Result.Authority);
            }
            #endregion

            return null;
        }
    }
}
