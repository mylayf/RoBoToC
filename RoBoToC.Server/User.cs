using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RoBoToC.Entity.Enums.MarketEnums;

namespace RoBoToC.Server
{
    public class User
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public decimal WalletAmount { get; set; }
        public Markets Market { get; set; }
        public string Email { get; set; }
    }
}
