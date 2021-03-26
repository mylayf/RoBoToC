using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.DataAccess.Concrete.EntityFramework
{
    public class OrderTimeDal : BaseDal<OrderTime>, IOrderTimeDal
    {
        public OrderTimeDal(RobotocDbContext robotocDbContext) : base(robotocDbContext)
        {
        }
    }
}
