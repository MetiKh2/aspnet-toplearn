using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.DiscountViewModels;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Site.Areas.UserPanel.Controllers

{
    [Authorize]
    [Area("UserPanel")]
    public class MyOrdersController : Controller
    {
        private readonly IOrderService _orderService;
        public MyOrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View(_orderService.GetOrdersForUserPanel(User.Identity.Name));
        }


        public IActionResult ShowOrders(int id,bool finaly=false,string type="")
        {
            var order = _orderService.GetOrderForUserPanel(id, User.Identity.Name);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.typeDiscount = type;
            ViewBag.finaly = finaly;
            return View(order);
        }

        public IActionResult FinallyOrder(int id)
        {
            if (_orderService.OrderFinally(User.Identity.Name, id))
            {
                return Redirect("/UserPanel/myOrders/ShowOrders/"+id+"?finaly=true");
            }
            return BadRequest();

        }

        public IActionResult UseDiscount(int orderId,string code)
        {
           var type= _orderService.UseDiscount(code,orderId);

            return Redirect("/userPanel/myorders/showorders/"+orderId + "?type=" + type.ToString());
        }
    }
}
