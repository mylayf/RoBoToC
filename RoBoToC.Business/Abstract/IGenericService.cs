using RoBoToC.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.Business.Abstract
{
    public interface IGenericService<Entity>
    {
        Task<IDataResult<Entity>> Add(Entity entity, CancellationToken cancellationToken);
        Task<IDataResult<Entity>> Update(Entity entity, CancellationToken cancellationToken);
        Task<IDataResult<int>> Remove(Entity entity, CancellationToken cancellationToken);
        Task<IDataResult<Entity>> Get(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter);
        Task<IDataResult<Entity[]>> GetAll(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter = null);
    }
}
