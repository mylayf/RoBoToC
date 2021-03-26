using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoBoToC.Entity.Concrete
{
    [Table("_CompletedProcesses")]
    public class CompletedProcess : IEntity
    {
        public int Id { get; set; }
        [StringLength(20)]
        public int CurrencyId { get; set; }
        [Column(TypeName = "money")]
        public decimal BoughtPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal SoldPrice { get; set; }
        public DateTime SoldDate { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
    }
}
