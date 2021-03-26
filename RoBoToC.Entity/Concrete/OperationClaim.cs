using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RoBoToC.Entity.Concrete
{
    [Table("_OperationClaims")]
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

    }
}
