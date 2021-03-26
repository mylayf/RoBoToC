using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoBoToC.Entity.Concrete
{
    [Table("_UserOperationClaims")]
    public class UserOperationClaim : IUserEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
        public virtual User User{ get; set; }
    }
}
