using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RoBoToC.Server.Events.TradingEvents;

namespace RoBoToC.Server.Trading
{
    public interface ISellProcess
    {
        string Currency { get; set; }
        decimal LastPrice { get; set; }
        bool Hook { get; set; }
        decimal HookInterval { get; set; }
        decimal HookPrice { get; set; }
        decimal TargetPrice { get; set; }
        decimal Quantity { get; set; }
        User User { get; set; }
        long OrderId { get; set; }
        Task<bool> Sell();

        event CompleteSell OnCompleted;
        event PartiallySell OnPartiallyCompleted;
    }
}
