using Binance.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RoBoToC.Server.Events.TradingEvents;
using Binance.Net.Enums;

namespace RoBoToC.Server.Trading.Binance
{
    public class BinanceBuyProcess : IBuyProcess
    {
        public string Currency { get; set; }
        public decimal UsingAmount { get; set; }
        public decimal UsingAmountRate
        {
            get
            {
                return _usingAmountRate;
            }
            set
            {
                _usingAmountRate = value;
                UsingAmount = (User.WalletAmount / 100) * value;
            }
        }
        private decimal _usingAmountRate { get; set; }
        public bool Hook { get; set; }
        public User User { get; set; }
        public decimal HookPrice { get { return _hookPrice; } set { TargetPrice = value * 0.99m; _hookPrice = value; } }
        private decimal _hookPrice { get; set; }
        public decimal TargetPrice { get; set; }
        public decimal LastPrice
        {
            get { return _lastPrice; }
            set
            {
                _lastPrice = value;
                new Task(async () =>
                {
                    if (Hook)
                    {
                        await HookTrack();
                    }
                    else
                    {
                        await Buy();
                    }
                }).Start();
                return;
            }
        }
        private decimal _lastPrice { get; set; }
        public int DecreaseCounter { get; set; } = 0;
        public decimal Quantity { get; set; }
        public bool WaitingToBeFilled { get; set; }
        public long OrderId { get; set; }
        public int WaitTimeCounter { get; set; } = 0;

        public event HookBuyCancellation OnHookBuyAbort;
        public event CompleteBuy OnCompleted;
        public event PartiallyCompleted OnPartiallyCompleted;

        public IBinanceClient binanceClient;

        public BinanceBuyProcess(IBinanceClient binanceClient)
        {
            this.binanceClient = binanceClient;
        }

        public async Task HookTrack()
        {
            if (TargetPrice > LastPrice)
            {
                HookPrice = TargetPrice;
                DecreaseCounter++;
            }
            else if (HookPrice < LastPrice && DecreaseCounter < 3)
            {
                if (OnHookBuyAbort != null)
                {
                    OnHookBuyAbort(this);
                }
            }
            else if (HookPrice < LastPrice && DecreaseCounter >= 3)
            {
                await Buy();
            }
        }

        public async Task<bool> Buy()
        {
            var buyResult = await binanceClient.Spot.Order.PlaceOrderAsync(Currency, OrderSide.Buy, OrderType.Limit, Quantity, null, null, LastPrice);
            if (buyResult.Success)
            {
                OrderId = buyResult.Data.OrderId;
                Quantity = buyResult.Data.QuantityFilled;
                if (buyResult.Data.QuantityRemaining > 0)
                {
                    WaitingToBeFilled = true;
                    await CheckOrder();
                }
                if (OnCompleted != null)
                {
                    OnCompleted(this);
                }
            }
            return true;
        }

        public async Task<bool> CheckOrder()
        {
        CheckStart:
            await Task.Delay(TimeSpan.FromMinutes(1));
            WaitTimeCounter++;
            var Order = await binanceClient.Spot.Order.GetOrderAsync(Currency, OrderId);
            if (Order.Success)
            {
                if (Order.Data.QuantityRemaining == 0)
                {
                    if (OnCompleted != null)
                    {
                        OnCompleted(this);
                    }
                }
                else
                {
                    if (WaitTimeCounter > 14)
                    {
                        await CancelOrder();
                        if (Order.Data.QuantityFilled > 0)
                        {
                            Quantity = Order.Data.Quantity;
                            if (OnPartiallyCompleted != null)
                            {
                                OnPartiallyCompleted(this);
                            }
                        }
                        else
                        {
                            if (OnPartiallyCompleted != null)
                            {
                                OnPartiallyCompleted(this);
                            }
                        }
                        return false;
                    }
                    goto CheckStart;
                }
            }
            return false;
        }

        public async Task<bool> CancelOrder()
        {
            var Response = await binanceClient.Spot.Order.CancelOrderAsync(Currency, OrderId);
            Quantity = Response.Data.QuantityFilled;
            return Response.Success;
        }
    }
}
