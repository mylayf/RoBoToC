using BinanceRobot.DataAccess.Concrete;
using RoBoToC.Core.Models;
using RoBoToC.DataAccess.Concrete.AdoNet;
using RoBoToC.Server.Data;
using RoBoToC.Server.Trading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RoBoToC.Entity.Enums.TimeEnums;
using static RoBoToC.Server.Events.TrackerEvents;

namespace RoBoToC.Server.Tracker
{
    public abstract class TrackerBase<BuyProcessor,SellProcessor> 
        where BuyProcessor: IBuyProcess
        where SellProcessor: ISellProcess
    {
        private CurrencyDal _currencyDal;
        private TrackingTimes _trackingTimes;
        public TrackerBase(CurrencyDal currencyDal, TrackingTimes trackingTimes, string Currency)
        {
            _currencyDal = currencyDal;
            this.Currency = Currency;
            if (!currencyDal.CheckTableExistance(Currency))
            {
                currencyDal.CreateTable(Currency);
            }
            else
            {
                Prices = currencyDal.GetAll(Currency);
            }
            Prices = new List<PriceModel>();
            _trackingTimes = trackingTimes;
        }
        public virtual List<BuyProcessor> buyProcesses { get; set; }

        public event LowestAlert onLowest;

        private DateTime LastSaveTime { get; set; }

        public string Currency { get; set; }

        public decimal LastPrice
        {
            get
            {
                return _LastPrice;
            }
            set
            {
                CalculateVariance(_LastPrice, value);
                _LastPrice = value;
                InformProcessors();

                if (ComparePrice(value))
                {
                    if (onLowest != null)
                    {
                        onLowest(Currency, value, LowestOfDurationAmount, LowestOfDuration);
                    }
                }

                Prices.Add(new PriceModel()
                {
                    Price = value,
                    Date = DateTime.Now
                });
                SaveDate();
            }
        }

        private decimal _LastPrice { get; set; }

        public List<PriceModel> Prices { get; set; }

        public bool ComparePrice(decimal lastPrice)
        {

            if (Prices.Count > 0)
            {
                foreach (var item in _trackingTimes.Times)
                {
                    if (Prices.Min(x => x.Price) > lastPrice)
                    {
                        LowestOfDuration = item.Duration;
                        LowestOfDurationAmount = item.Quantity;
                        return true;
                    }
                }
            }
            return false;
        }

        protected virtual void MinimizePriceList()
        {
            var LastDayMins = Prices.Where(x => x.Date.Date == DateTime.Today).GroupBy(x => x.Date.Hour).Select(x => x.OrderBy(y => y.Price).FirstOrDefault());
            Prices.RemoveAll(x => x.Date.Date >= DateTime.Today);
            Prices.AddRange(LastDayMins);
        }

        public int LowestOfDurationAmount { get; set; }
        public Durations LowestOfDuration { get; set; }

        public decimal RiseRate { get; set; }

        public decimal RiseAmount { get; set; }

        private void CalculateVariance(decimal prevPrice, decimal lastPrice)
        {
            if (prevPrice != 0)
            {
                RiseAmount = lastPrice - prevPrice;
                RiseRate = 100 * lastPrice / prevPrice;
            }
        }

        protected virtual Task InformProcessors()
        {
            return Task.CompletedTask;
        }

        private async void SaveDate()
        {
            if (LastSaveTime.AddMinutes(15) <= DateTime.Now)
            {
                LastSaveTime = DateTime.Now;
                var prices = Prices?.Where(x => !x.IsRecorded).ToList();
                await Task.Run(() =>
                 {
                     if (prices != null)
                         foreach (var priceModel in prices)
                         {
                             _currencyDal.Save(Currency, priceModel);
                             priceModel.IsRecorded = true;
                         }
                 });
            }
        }
    }
}
