using RoBoToC.Business.Abstract;
using RoBoToC.Business.Constants;
using RoBoToC.Core.Utilities.Hashing;
using RoBoToC.Core.Utilities.Results;
using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.Business.Concrete
{
    public class UserApiManager : GenericManager<UserApi>, IUserApiService
    {
        private IUserApiDal userApiDal;
        public UserApiManager(IUserApiDal userApiDal) : base(userApiDal)
        {
            this.userApiDal = userApiDal;
        }
        public override async Task<IDataResult<UserApi>> Add(UserApi entity, CancellationToken cancellationToken)
        {
            bool isAlreadyExists = await userApiDal.Any(cancellationToken, x => x.Market == entity.Market && x.UserId == entity.UserId);
            if (isAlreadyExists)
            {
                return new ErrorDataResult<UserApi>(Messages.SameMarketApi);
            }
            var isAlreadyInUse = await userApiDal.AnyInAll(cancellationToken, x => x.ApiKey == entity.ApiKey && x.ApiSecret == entity.ApiSecret && x.UserId != entity.UserId);
            if (isAlreadyInUse)
            {
                return new ErrorDataResult<UserApi>(Messages.AlreadyUsingAPI);
            }

            return await base.Add(entity, cancellationToken);
        }
    }
}
