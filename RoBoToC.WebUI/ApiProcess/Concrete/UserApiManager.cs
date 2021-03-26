using Microsoft.AspNetCore.Mvc;
using RoBoToC.Entity.Concrete;
using RoBoToC.Entity.Dto.User;
using RoBoToC.WebUI.ApiProcess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.WebUI.ApiProcess.Concrete
{
    public class UserApiManager : GenericApiManager<User>, IUserApiService
    {
        public async Task<string> Login(UserLoginDto userLoginDto, CancellationToken cancellationToken)
        {
            var content = ApiHelper.SerializeContent(userLoginDto);
            var response = await ApiHelper.Post(content, cancellationToken, false, "User/Login");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
