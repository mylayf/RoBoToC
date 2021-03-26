using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoBoToC.DataAccess.Abstract
{
    public interface IUserDal:IBaseDal<User>
    {
        public Task<List<OperationClaim>> GetClaims(User user);
    }
}
