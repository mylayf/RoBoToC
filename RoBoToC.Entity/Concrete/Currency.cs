using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoBoToC.Entity.Concrete
{
    [Table("_Currencies")]
    public class Currency : IEntity
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
    }
}
