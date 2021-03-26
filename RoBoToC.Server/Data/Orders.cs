using Microsoft.EntityFrameworkCore;
using RoBoToC.DataAccess;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RoBoToC.Entity.Enums.TimeEnums;

namespace RoBoToC.Server.Data
{
    public class Orders
    {
        public List<Order> OrderList;
        public Orders(RobotocDbContext robotocDbContext)
        {
            OrderList = robotocDbContext.Orders
                .Include(x => x.User)
                    .ThenInclude(x => x.Apis)
                .Include(x => x.User)
                    .ThenInclude(x => x.WhiteCurrencies)
                .Include(x => x.Times)
                    .ThenInclude(x => x.Time)
                .Where(x => x.User.Status == true).ToList();
        }

        public  List<Order> GetOrdersAsync(string Currency, Durations duration, int Amount)
        {
            return  OrderList.Where(x => x.User.WhiteCurrencies.Any(c => c.Currency.Name == Currency) && x.Times.Any(t => t.Time.Duration == duration && t.Time.Quantity == Amount)).ToList();
        }
    }
}
