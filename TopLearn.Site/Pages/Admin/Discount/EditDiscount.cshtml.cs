using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.Course;

namespace TopLearn.Site.Pages.Admin.Discount
{
    public class EditDiscountModel : PageModel
    {
        private readonly IOrderService _orderService;
        public EditDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public CourseDiscount Discount { get; set; }
        public void OnGet(int id)
        {
            Discount = _orderService.GetCourseDiscountByID(id);
        }

        public IActionResult OnPost(string stDate, string edDate)
        {
            if (stDate != null)
            {
                string[] startDate = stDate.Split('/');

                Discount.StartDate = new DateTime(int.Parse(startDate[0]), int.Parse(startDate[1]), int.Parse(startDate[2]), new PersianCalendar());
            }
            if (edDate != null)
            {
                string[] endDate = edDate.Split('/');

                Discount.EndDate = new DateTime(int.Parse(endDate[0]), int.Parse(endDate[1]), int.Parse(endDate[2]), new PersianCalendar());
            }

            if (!ModelState.IsValid)

            {
                return Page();
            }

            _orderService.UpdateDiscount(Discount);

            return RedirectToPage("Index");
        }
    }
}
