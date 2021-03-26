using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
