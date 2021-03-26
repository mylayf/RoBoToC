using RoBoToC.Core.Utilities.Results;
using RoBoToC.Core.Utilities.Security.Jwt;
using RoBoToC.Entity.Concrete;
using RoBoToC.Entity.Dto.User;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.Business.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        Task<IDataResult<User>> Login(UserLoginDto userLoginDto, CancellationToken cancellationToken);
        Task<IResult> Register(UserRegisterDto userRegisterDto, CancellationToken cancellationToken);
        Task<IResult> UsernameAvailability(string userName, CancellationToken cancellationToken);
        Task<IDataResult<AccessToken>> CreateAccessToken(User user);
    }
}
