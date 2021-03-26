using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.DataAccess.Concrete.EntityFramework
{
    public class CompletedProcessDal : BaseDal<CompletedProcess>, ICompletedProcessDal
    {
        public CompletedProcessDal(RobotocDbContext robotocDbContext) : base(robotocDbContext)
        {

        }
    }
}
