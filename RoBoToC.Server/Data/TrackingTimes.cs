using BinanceRobot.DataAccess.Concrete;
using RoBoToC.DataAccess;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoBoToC.Server.Data
{
    public class TrackingTimes
    {
        public TrackingTimes(FullRobotocDbContext robotocDbContext)
        {
            Times = robotocDbContext.Times.Where(x => x.Active).OrderByDescending(x => x.Duration).ThenByDescending(x => x.Quantity).ToList();
        }

        public List<Time> Times = new List<Time>();
    }
}
