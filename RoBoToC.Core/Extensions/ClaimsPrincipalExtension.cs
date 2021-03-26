using RoBoToC.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RoBoToC.Core.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string[] GetRoles(this ClaimsPrincipal User)
        {
            string[] roles = User.Claims?.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value)?.ToArray();
            return roles;
        }
        public static bool isAdmin(this ClaimsPrincipal User)
        {
            return User.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == RobotocRoles.Admin);
        }
        public static int UserId(this ClaimsPrincipal User)
        {
            return Convert.ToInt32(User.Claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
