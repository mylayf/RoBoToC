using Microsoft.EntityFrameworkCore;
using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoBoToC.DataAccess.Concrete.EntityFramework
{
    public class UserDal : BaseDal<User>, IUserDal
    {
        public UserDal(RobotocDbContext robotocDbContext) : base(robotocDbContext)
        {
        }
        protected override IQueryable<User> Queryable => base.Queryable;

        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            return await RobotocDbContext.UserOperationClaims.Include(x => x.OperationClaim).Where(x => x.UserId == user.Id).Select(x => x.OperationClaim).ToListAsync();
        }
    }
}
