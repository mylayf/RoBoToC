using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RoBoToC.Server.Events.TradingEvents;

namespace RoBoToC.Server.Trading
{
    public interface IBuyProcess
    {
        string Currency { get; set; }
        decimal LastPrice { get; set; }
        decimal UsingAmount { get; set; }
        decimal UsingAmountRate { get; set; }
        bool Hook { get; set; }
        decimal HookPrice { get; set; }
        decimal TargetPrice { get; set; }
        decimal Quantity { get; set; }
        int DecreaseCounter { get; set; }
        User User { get; set; }
        bool WaitingToBeFilled { get; set; }
        int WaitTimeCounter { get; set; }
        long OrderId { get; set; }
        Task<bool> Buy();

        event HookBuyCancellation OnHookBuyAbort;
        event CompleteBuy OnCompleted;
        event PartiallyCompleted OnPartiallyCompleted;
    }
}
