using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.Entity.Abstract
{
    public interface IUserEntity:IEntity
    {
        public int UserId { get; set; }
    }
}
