using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoBoToC.Server.Subscriber.Abstract
{
    public interface ISubscriber
    {
        Task StartSubscribe();
    }
}
