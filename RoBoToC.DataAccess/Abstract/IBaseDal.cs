using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.DataAccess.Abstract
{
    public interface IBaseDal<Entity> where Entity : class, IEntity, new()
    {
        Task<Entity> Add(Entity entity, CancellationToken cancellationToken);
        Task<Entity> Update(Entity entity, CancellationToken cancellationToken);
        Task<int> Remove(Entity entity, CancellationToken cancellationToken);
        Task<Entity> Get(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter);
        Task<Entity[]> GetAll(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter = null);
        Task<bool> Any(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter);
        Task<bool> AnyInAll(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter);
    }
}
