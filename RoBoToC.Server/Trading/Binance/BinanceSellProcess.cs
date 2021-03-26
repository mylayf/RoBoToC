using Binance.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binance.Net.Enums;
using RoBoToC.Server.Events;

namespace RoBoToC.Server.Trading.Binance
{
    public class BinanceSellProcess : ISellProcess
    {
        public string Currency { get; set; }
        public decimal LastPrice
        {
            get
            {
                return _lastPrice;
            }
            set
            {
                _lastPrice = value;
                Task.Run(async () =>
                {
                    if (Hook)
                    {
                        await HookTrack();
                    }
                    else
                    {
                        await Sell();
                    }
                });
            }
        }
        private decimal _lastPrice { get; set; }
        public bool Hook { get; set; }
        public decimal HookInterval { get; set; }
        public decimal HookPrice { get { return _hookPrice; } set { TargetPrice = value + (value * (HookInterval / 100)); _hookPrice = value; } }
        public decimal _hookPrice { get; set; }
        public decimal TargetPrice { get; set; }
        public decimal Quantity { get; set; }
        public User User { get; set; }
        public long OrderId { get; set; }

        public event TradingEvents.CompleteSell OnCompleted;
        public event TradingEvents.PartiallySell OnPartiallyCompleted;

        private IBinanceClient binanceClient;

        public BinanceSellProcess(IBinanceClient binanceClient)
        {
            this.binanceClient = binanceClient;
        }

        public async Task HookTrack()
        {
            if (TargetPrice < LastPrice)
            {
                HookPrice = TargetPrice;
            }
            else if (HookPrice > LastPrice)
            {
                await Sell();
            }
        }

        public async Task<bool> Sell()
        {
            var result = await binanceClient.Spot.Order.PlaceOrderAsync(Currency, OrderSide.Sell, OrderType.Limit, Quantity, null, null, LastPrice, TimeInForce.GoodTillCancel);
            if (result.Success)
            {
                if(OnCompleted!= null)
                {
                    OnCompleted(this);
                }
            }
            else
            {
            }
            await Task.Run(null);
            return true;
        }
    }
}
