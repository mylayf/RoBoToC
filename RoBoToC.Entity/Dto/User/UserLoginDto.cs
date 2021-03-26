using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.Entity.Dto.User
{
    public class UserLoginDto:IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
