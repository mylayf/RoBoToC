using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.DataAccess.Concrete.EntityFramework
{
    public class CurrentProcessDal:BaseDal<CurrentProcess>,ICurrentProcessDal
    {
        public CurrentProcessDal(RobotocDbContext robotocDbContext):base(robotocDbContext)
        {

        }
    }
}
