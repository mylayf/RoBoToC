using Microsoft.AspNetCore.Mvc;
using RoBoToC.Entity.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.WebUI.ApiProcess.Abstract
{
    public interface IUserApiService
    {
        Task<string> Login(UserLoginDto userLoginDto, CancellationToken cancellationToken);
    }
}
