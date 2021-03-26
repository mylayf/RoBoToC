using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static RoBoToC.Entity.Enums.TimeEnums;

namespace RoBoToC.Entity.Concrete
{
    [Table("_Times")]
    public class Time : IEntity
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public short Quantity { get; set; }
        public Durations Duration { get; set; }
        public bool Active { get; set; }
    }
}
