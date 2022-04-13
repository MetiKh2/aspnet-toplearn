using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.DiscountViewModels;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn.Core.Services.Interfaces
{
   public interface IOrderService
    {
        int AddOrder(string username,int courseId);
        void UpdatePriceOrder(int orderId);

        Order GetOrderForUserPanel(int orderId,string username);

        bool OrderFinally(string username,int orderId);

        List<Order> GetOrdersForUserPanel(string username);

        #region Discount

        DiscountUseType UseDiscount(string code, int orderId);

        void AddDiscount(CourseDiscount discount);
        List<CourseDiscount> GetCourseDiscounts();
        CourseDiscount GetCourseDiscountByID(int id);
        void UpdateDiscount(CourseDiscount courseDiscount);
        bool IsExistDiscountCode(string code);
        #endregion
    }
}
