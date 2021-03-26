using RoBoToC.DataAccess.Concrete.AdoNet;
using RoBoToC.Server.Data;
using RoBoToC.Server.Trading.Binance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoBoToC.Server.Tracker.Binance
{
    public class BinanceTracker : TrackerBase<BinanceBuyProcess, BinanceSellProcess>
    {
        public override List<BinanceBuyProcess> buyProcesses { get => base.buyProcesses; set => base.buyProcesses = value; }

        public BinanceTracker(CurrencyDal currencyDal, TrackingTimes trackingTimes, string Currency) : base(currencyDal, trackingTimes, Currency)
        {

        }

        protected override Task InformProcessors()
        {
            foreach (var item in buyProcesses)
            {
                if (!item.WaitingToBeFilled)
                {
                    item.LastPrice = LastPrice;
                }
            }
            return base.InformProcessors();
        }
    }
}
