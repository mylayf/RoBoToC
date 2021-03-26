using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RoBoToC.Entity.Enums.TimeEnums;

namespace RoBoToC.Server.Events
{
    public class TrackerEvents
    {
        public delegate EventHandler LowestAlert(string Currency, decimal Price, int LowestOfDurationAmount, Durations LowestOfDuration);
    }
}
