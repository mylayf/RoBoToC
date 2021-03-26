using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoBoToC.Entity.Concrete
{
    [Table("_UserApis")]
    public class UserApi : IUserEntity
    {
        [NotMapped]
        public int Id { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string Market { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
