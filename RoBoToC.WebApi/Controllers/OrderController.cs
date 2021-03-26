using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoBoToC.Business.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.WebApi.Controllers
{
    [Authorize()]
    [Route("Order")]
    public class OrderController : BaseController<Order, bool>
    {
        private IOrderService orderService;

        public OrderController(IOrderService orderService) : base(orderService)
        {
            this.orderService = orderService;
        }
        [HttpPost("Save")]
        public async Task<IActionResult> Save(CancellationToken cancellationToken)
        {
            Order order = new()
            {
                BuyingHook = true,
                Name = "SAFA",
                ProfitRate = 2,
                SellingHook = true,
                SellingHookRate = 2.5m,
                SpendRate = 15,
                StopLoss = true,
                StopLossRate = 50,
                Times = new List<OrderTime>()
                {
                    new OrderTime
                    {
                        TimeId=1,

                    }
                },
                WalletAmount = 50
            };
            await base.Create(cancellationToken, order);
            return Ok(order);
        }
    }
}
