using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RoBoToC.Core.Extensions;
using RoBoToC.Core.Utilities.Security.Encryption;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RoBoToC.Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private IConfiguration configuration;
        TokenOptions TokenOptions;
        DateTime accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
            TokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            accessTokenExpiration = DateTime.Now.AddMinutes(TokenOptions.AccessTokenExpiration);
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(TokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(TokenOptions, user, signingCredentials, operationClaims);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            string token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken
            {
                Token = token,
                Expiration = accessTokenExpiration
            };

        }
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: GetClaims(user, operationClaims),
                notBefore: DateTime.Now,
                expires: accessTokenExpiration,
                signingCredentials: signingCredentials);
            return jwt;
        }
        public IEnumerable<Claim> GetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddName(user.Name + " " + user.Surname);
            claims.AddEmail(user.Email);
            claims.AddRoles(operationClaims.Select(x => x.Name).ToArray());
            return claims;
        }
    }
}
