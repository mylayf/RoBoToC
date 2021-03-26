using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoBoToC.Entity.Concrete
{
    [Table("_UserWhiteCurrencies")]
    public class UserWhiteCurrency:IUserEntity
    {
        [NotMapped]
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public int UserId { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual User User { get; set; }
    }
}
