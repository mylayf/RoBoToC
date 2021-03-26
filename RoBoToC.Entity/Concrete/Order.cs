using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoBoToC.Entity.Concrete
{
    [Table("_Orders")]
    public class Order : IUserEntity, IEntity
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int UserId { get; set; }
        public decimal ProfitRate { get; set; }
        public bool BuyingHook { get; set; }
        public bool SellingHook { get; set; }
        public decimal SellingHookRate { get; set; }
        public bool StopLoss { get; set; }
        public decimal StopLossRate { get; set; }
        public decimal SpendRate { get; set; }
        public decimal WalletAmount { get; set; }
        public virtual List<OrderTime> Times { get; set; }
        public virtual User User { get; set; }
    }
}
