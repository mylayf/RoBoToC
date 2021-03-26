using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoBoToC.Core.Models
{
    public class PriceModel
    {
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public bool IsRecorded { get; set; }
    }
}
