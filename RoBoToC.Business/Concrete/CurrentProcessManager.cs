using RoBoToC.Business.Abstract;
using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.Business.Concrete
{
    public class CurrentProcessManager : GenericManager<CurrentProcess>, ICurrentProcessService
    {
        private ICurrentProcessDal currentProcessDal;
        public CurrentProcessManager(ICurrentProcessDal currentProcessDal) : base(currentProcessDal)
        {
            this.currentProcessDal = currentProcessDal;
        }
    }
}
