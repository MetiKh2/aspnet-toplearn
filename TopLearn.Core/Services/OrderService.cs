using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.DiscountViewModels;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly TopLearnContext _context;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;

        public OrderService(TopLearnContext context, IUserService userService, ICourseService courseService)
        {
            _context = context;
            _userService = userService;
            _courseService = courseService;
        }

        public void AddDiscount(DataLayer.Entities.Course.CourseDiscount discount)
        {
            _context.CourseDiscounts.Add(discount);
            _context.SaveChanges();
        }

        public int AddOrder(string username, int courseId)
        {
            var userId = _userService.GetUserIDByUserName(username);

            var order = _context.Orders.FirstOrDefault(p => p.UserID == userId && !p.IsFinaly);

            var course = _courseService.GetCourseByID(courseId);
            if (order == null)
            {
                order = new Order
                {
                    IsFinaly = false,
                    CreateDate = DateTime.Now,
                    SumAmount = int.Parse(course.Price.ToString()),
                    UserID = userId,
                    OrderDetails = new List<OrderDetail> {
                new OrderDetail{
                Count=1,
                Price=int.Parse(course.Price.ToString()),
                CourseID=courseId,

                }
                },
                };

                _context.Add(order);
                _context.SaveChanges();
            }
            else
            {
                var detail = _context.OrderDetails.FirstOrDefault(p => p.CourseID == courseId && p.OrderID == order.OrderID);

                if (detail == null)
                {
                    detail = new OrderDetail
                    {
                        Count = 1,
                        OrderID = order.OrderID,
                        CourseID = courseId,
                        Price = (int)course.Price
                    };
                    _context.OrderDetails.Add(detail);
                }

                else
                {
                    detail.Count++;
                    _context.OrderDetails.Update(detail);
                }

                _context.SaveChanges();
                UpdatePriceOrder(order.OrderID);
            }


            return order.OrderID;

        }

        public DataLayer.Entities.Course.CourseDiscount GetCourseDiscountByID(int id)
        {
            return _context.CourseDiscounts.Find(id);
        }

        public List<DataLayer.Entities.Course.CourseDiscount> GetCourseDiscounts()
        {
            return _context.CourseDiscounts.ToList();
        }

        public Order GetOrderForUserPanel(int orderId, string username)
        {
            var userId = _userService.GetUserIDByUserName(username);
            return _context.Orders.Include(p => p.OrderDetails).ThenInclude(p => p.Course).FirstOrDefault(p => p.OrderID == orderId && p.UserID == userId);
        }

        public List<Order> GetOrdersForUserPanel(string username)
        {
            var userId = _userService.GetUserIDByUserName(username);
            return _context.Orders.Where(p => p.UserID == userId).ToList();
        }

        public bool IsExistDiscountCode(string code)
        {
            return _context.CourseDiscounts.Any(p=>p.DiscountCode==code);
        }

        public bool OrderFinally(string username, int orderId)
        {
            var userId = _userService.GetUserIDByUserName(username);
            var order = _context.Orders.Include(p => p.OrderDetails).ThenInclude(p => p.Course).FirstOrDefault(p => p.OrderID == orderId && p.UserID == userId);

            if (order == null || order.IsFinaly)
            {
                return false;
            }
            if (_userService.RemainUserWallet(username) >= order.SumAmount)
            {
                order.IsFinaly = true;
                Wallet wallet = new Wallet
                {
                    Amount = order.SumAmount,
                    DateTime = DateTime.Now,
                    Description = $"فاکتور شماره {order.OrderID}",
                    IsPay = true,
                    TypeID = 2,
                    UserID = userId,

                };
                _context.Wallets.Add(wallet);
                foreach (var item in order.OrderDetails)
                {
                    _context.UserCourses.Add(
                        new DataLayer.Entities.Course.UserCourse
                        {
                            CourseID = item.CourseID,
                            UserID = userId,

                        }
                        );
                }
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public void UpdateDiscount(DataLayer.Entities.Course.CourseDiscount courseDiscount)
        {
            _context.CourseDiscounts.Update(courseDiscount);
            _context.SaveChanges();
        }

        public void UpdatePriceOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            
            var orderDetails = _context.OrderDetails.Where(d => d.OrderID == orderId).ToList();
            var sum = 0;
            foreach (var item in orderDetails)
            {
              sum += (item.Count * item.Price);
            }
            order.SumAmount = sum;
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public DiscountUseType UseDiscount(string code, int orderId)
        {
            var discount = _context.CourseDiscounts.Where(p => p.DiscountCode == code).SingleOrDefault();

            if (discount==null)
            {
                return DiscountUseType.NotFound;
            }

            if (discount.StartDate != null && discount.StartDate >DateTime.Now)
                return DiscountUseType.ExpierDate;

            if (discount.EndDate != null && discount.EndDate <= DateTime.Now)
                return DiscountUseType.ExpierDate;


            if (discount.UsableCount != null && discount.UsableCount < 1)
                return DiscountUseType.Finished;

            var order = _context.Orders.Find(orderId);

            if (_context.UserDiscount.Any(p=>p.DiscountID==discount.DiscountID&&p.UserID==order.UserID))
            {
                return DiscountUseType.UserUsed;
            }


            int precent = (order.SumAmount * discount.DiscountPercent) / 100;
            order.SumAmount = order.SumAmount - precent;
            _context.Orders.Update(order);

            if (discount.UsableCount!=null)
            {
                discount.UsableCount -= 1;
            }

            _context.CourseDiscounts.Update(discount);
            _context.UserDiscount.Add(new DataLayer.Entities.User.UserDiscount { 
            UserID=order.UserID,
            DiscountID=discount.DiscountID
            });
            _context.SaveChanges();
            return DiscountUseType.succsess;
        }

    
    }
}
