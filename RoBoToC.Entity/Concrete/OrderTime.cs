using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoBoToC.Entity.Concrete
{
    [Table("_OrderTimes")]
    public class OrderTime : IEntity
    {
        public int Id { get; set; }
        public int TimeId { get; set; }
        public int OrderId { get; set; }
        public virtual Time Time { get; set; }
        public virtual Order Order { get; set; }
    }
}
