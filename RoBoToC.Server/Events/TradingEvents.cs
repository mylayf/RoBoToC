using RoBoToC.Server.Trading;
using System;

namespace RoBoToC.Server.Events
{
    public class TradingEvents
    {
        public delegate EventHandler HookBuyCancellation(IBuyProcess buyProcess);
        public delegate EventHandler CompleteBuy(IBuyProcess buyProcess);
        public delegate EventHandler PartiallyCompleted(IBuyProcess buyProcess);

        public delegate EventHandler CompleteSell(ISellProcess sellProcess);
        public delegate EventHandler PartiallySell(ISellProcess sellProcess);
    }
}
