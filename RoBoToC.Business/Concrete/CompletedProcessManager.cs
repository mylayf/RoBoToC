using RoBoToC.Business.Abstract;
using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.Business.Concrete
{
    public class CompletedProcessManager : GenericManager<CompletedProcess>, ICompletedProcessService
    {
        private ICompletedProcessDal completedProcessDal;
        public CompletedProcessManager(ICompletedProcessDal completedProcessDal):base(completedProcessDal)
        {
            this.completedProcessDal = completedProcessDal;
        }
    }
}
