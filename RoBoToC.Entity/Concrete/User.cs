using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoBoToC.Entity.Concrete
{
    [Table("_Users")]
    public class User : IEntity
    {
        public int Id { get; set; }
        [StringLength(25)]
        public string Username { get; set; }
        [StringLength(70)]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Status { get; set; }
        [NotMapped]
        public string Token { get; set; }
        public ICollection<UserApi> Apis { get; set; }
        public  ICollection<UserWhiteCurrency> WhiteCurrencies { get; set; }
        public  ICollection<UserOperationClaim> Claims { get; set; }
    }
}
