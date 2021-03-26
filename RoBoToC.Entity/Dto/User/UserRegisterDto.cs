using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.Entity.Dto.User
{
    public class UserRegisterDto:IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
