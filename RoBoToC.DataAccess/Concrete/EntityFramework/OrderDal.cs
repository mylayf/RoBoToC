using Microsoft.EntityFrameworkCore;
using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.DataAccess.Concrete.EntityFramework
{
    public class OrderDal : BaseDal<Order>, IOrderDal
    {

        public OrderDal(RobotocDbContext robotocDbContext) : base(robotocDbContext)
        {
        }
        protected override IQueryable<Order> Queryable => base.Queryable.Include(x => x.Times).Include(x => x.User);
    }
}
