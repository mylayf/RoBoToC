using Binance.Net;
using Binance.Net.Interfaces;
using RoBoToC.DataAccess.Concrete.AdoNet;
using RoBoToC.Server.Data;
using RoBoToC.Server.Subscriber.Abstract;
using RoBoToC.Server.Tracker.Binance;
using RoBoToC.Server.Trading;
using RoBoToC.Server.Trading.Binance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RoBoToC.Entity.Enums.TimeEnums;

namespace RoBoToC.Server.Subscriber
{
    public class BinanceSubscriber : ISubscriber
    {
        BinanceSocketClient binanceSocketClient = new BinanceSocketClient();
        private List<BinanceTracker> trackers = new List<BinanceTracker>();
        private CurrencyDal _currencyDal;
        private TrackingTimes _trackingTimes;
        private Orders _orders;
        private IBinanceClient _binanceClient;
        private List<IBuyProcess> buyProcesses;
        private List<ISellProcess> sellProcesses;
        public BinanceSubscriber(CurrencyDal currencyDal, TrackingTimes trackingTimes, Orders orders, IBinanceClient binanceClient)
        {
            _currencyDal = currencyDal;
            _trackingTimes = trackingTimes;
            _orders = orders;
            _binanceClient = binanceClient;
        }

        public async Task StartSubscribe()
        {
            await binanceSocketClient.Spot.SubscribeToAllSymbolTickerUpdatesAsync((data) =>
            {
                foreach (var item in data)
                {
                    var tracker = CreateInstance(item.Symbol);
                    tracker.LastPrice = item.LastPrice;
                }
            });
        }

        private BinanceTracker CreateInstance(string Currency)
        {
            var tracker = trackers.Find(x => x.Currency == Currency);
            if (tracker == null)
            {
                tracker = new BinanceTracker(_currencyDal, _trackingTimes, Currency);
                tracker.onLowest += Tracker_onLowest;
                trackers.Add(tracker);
            }
            return tracker;
        }

        private EventHandler Tracker_onLowest(string Currency, decimal Price, int DurationAmount, Durations Duration)
        {
            var relatedOrders = _orders.GetOrdersAsync(Currency, Duration, DurationAmount);
            relatedOrders.ForEach((order) =>
            {
                var BuyProcess = new BinanceBuyProcess(_binanceClient)
                {
                    Currency = Currency,
                    LastPrice = Price,
                };
                BuyProcess.OnHookBuyAbort += BuyProcess_OnHookBuyAbort;
                BuyProcess.OnPartiallyCompleted += BuyProcess_OnPartiallyCompleted;
                BuyProcess.OnCompleted += BuyProcess_OnCompleted;

                buyProcesses.Add(BuyProcess);
            });
            return null;
        }

        private EventHandler BuyProcess_OnPartiallyCompleted(IBuyProcess buyProcess)
        {
            return null;
        }

        private EventHandler BuyProcess_OnHookBuyAbort(IBuyProcess buyProcess)
        {
            return null;
        }

        private EventHandler BuyProcess_OnCompleted(IBuyProcess buyProcess)
        {
            buyProcesses.Remove(buyProcess);
            buyProcess = null;
            var sellProcess = new BinanceSellProcess();
            sellProcesses.Add(sellProcess);
            return null;
        }

    }
}
