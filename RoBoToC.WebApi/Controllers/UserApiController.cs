using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoBoToC.Business.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoBoToC.WebApi.Controllers
{
    [Route("UserApi")]
    [Authorize()]
    public class UserApiController : BaseController<UserApi,bool>
    {
        private IUserApiService userApiService;

        public UserApiController(IUserApiService userApiService):base(userApiService)
        {
            this.userApiService = userApiService;
        }
    }
}
