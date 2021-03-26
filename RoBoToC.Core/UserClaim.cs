using Microsoft.AspNetCore.Http;
using RoBoToC.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.Core
{
    public class UserClaim : IUserClaim
    {
        IHttpContextAccessor httpContextAccessor;
        public UserClaim(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public int? UserId
        {
            get
            {
                return httpContextAccessor.HttpContext.User.UserId();
            }
        }
    }
}
