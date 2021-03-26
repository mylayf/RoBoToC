using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.DataAccess.Concrete.EntityFramework
{
    public class UserApiDal : BaseDal<UserApi>, IUserApiDal
    {
        private RobotocDbContext RobotocDbContext;
        public UserApiDal(RobotocDbContext robotocDbContext, FullRobotocDbContext fullRobotocDbContext) : base(robotocDbContext, fullRobotocDbContext)
        {

        }
    }
}
