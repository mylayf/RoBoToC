using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoBoToC.Business.Abstract;
using RoBoToC.Core.Extensions;
using RoBoToC.Entity.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.WebApi.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto, CancellationToken cancellationToken)
        {
            var result = await userService.Login(userLoginDto, cancellationToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var token = await userService.CreateAccessToken(result.Data);
            if (token.Success)
            {
                return Ok(token.Data);
            }
            return BadRequest(token.Message);

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto, CancellationToken cancellationToken)
        {
            var isAvailable = await userService.UsernameAvailability(userRegisterDto.Username, cancellationToken);
            if (!isAvailable.Success)
            {
                return BadRequest(isAvailable.Message);
            }
            var result = await userService.Register(userRegisterDto, cancellationToken);
            return Ok(result.Message);

        }
        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("test");

        }
    }
}
