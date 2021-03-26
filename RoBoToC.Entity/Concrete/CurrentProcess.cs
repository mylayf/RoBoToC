using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoBoToC.Entity.Concrete
{
    [Table("_CurrentProcesses")]
    public class CurrentProcess : IEntity
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public DateTime BoughtDate { get; set; }
        public decimal SpendRate { get; set; }
        public int OrderId { get; set; }
        public int TimeId { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual User User { get; set; }
        public virtual Time Time { get; set; }
        public virtual Order Order { get; set; }

    }
}
