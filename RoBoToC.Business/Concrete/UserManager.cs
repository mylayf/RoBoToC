using RoBoToC.Business.Abstract;
using RoBoToC.Business.Constants;
using RoBoToC.Core.Utilities.Hashing;
using RoBoToC.Core.Utilities.Results;
using RoBoToC.Core.Utilities.Security.Jwt;
using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Concrete;
using RoBoToC.Entity.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.Business.Concrete
{
    public class UserManager : GenericManager<User>, IUserService
    {
        private IUserDal userDal;
        private ITokenHelper tokenHelper;

        public UserManager(IUserDal userDal, ITokenHelper tokenHelper) : base(userDal)
        {
            this.userDal = userDal;
            this.tokenHelper = tokenHelper;
        }
        public async Task<IDataResult<User>> Login(UserLoginDto userLoginDto, CancellationToken cancellationToken)
        {
            var user = await userDal.Get(cancellationToken, x => x.Username == userLoginDto.Username);
            if (user == null || !HashHelper.VerifyHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.InvalidLogin);
            }
            return new SuccessDataResult<User>(user);
        }
        public async Task<IResult> Register(UserRegisterDto userRegisterDto, CancellationToken cancellationToken)
        {
            HashHelper.GenerateHash(userRegisterDto.Password, out byte[] hash, out byte[] salt);
            await userDal.Add(new User
            {
                Username = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                Name = userRegisterDto.Name,
                Surname = userRegisterDto.Surname,
                PasswordHash = hash,
                PasswordSalt = salt,
                RegistrationDate = DateTime.Now,
            }, cancellationToken);
            return new SuccessResult(Messages.AccountCreated);
        }
        public async Task<IResult> UsernameAvailability(string userName, CancellationToken cancellationToken)
        {
            bool checkUser = await userDal.Any(cancellationToken, x => x.Username == userName);
            if (checkUser)
            {
                return new ErrorResult(Messages.UserExists);
            }
            else
            {
                return new SuccessResult();
            }
        }
        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var claims = await userDal.GetClaims(user);
            var accessToken = tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(Messages.AccessTokenCreated, accessToken);
        }
    }
}
