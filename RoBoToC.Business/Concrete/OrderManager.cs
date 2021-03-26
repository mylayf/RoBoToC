using RoBoToC.Business.Abstract;
using RoBoToC.Core.Utilities.Results;
using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.Business.Concrete
{
    public class OrderManager : GenericManager<Order>, IOrderService
    {
        private IOrderDal _orderDal;
        private IOrderTimeDal _orderTimeDal;
        public OrderManager(IOrderDal orderDal, IOrderTimeDal orderTimeDal) : base(orderDal)
        {
            _orderDal = orderDal;
            _orderTimeDal = orderTimeDal;
        }
        public override async Task<IDataResult<Order>> Add(Order entity, CancellationToken cancellationToken)
        {
            var newOrder = await base.Add(entity, cancellationToken);
            if (newOrder.Success)
            {
                newOrder.Data.Times.ForEach(async time =>
                {
                    time.OrderId = newOrder.Data.Id;
                    time = await _orderTimeDal.Add(time, cancellationToken);
                });
            }
            return newOrder;
        }
    }
}
