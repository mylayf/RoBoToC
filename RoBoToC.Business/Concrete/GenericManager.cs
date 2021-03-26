using RoBoToC.Business.Abstract;
using RoBoToC.Core.Utilities.Results;
using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.Business.Concrete
{
    public class GenericManager<Entity> : IGenericService<Entity> where Entity : class, IEntity, new()
    {
        private IBaseDal<Entity> baseDal;

        public GenericManager(IBaseDal<Entity> baseDal)
        {
            this.baseDal = baseDal;
        }

        public virtual async Task<IDataResult<Entity>> Add(Entity entity, CancellationToken cancellationToken)
        {
            var newRecord = await baseDal.Add(entity, cancellationToken);
            return new DataResult<Entity>(newRecord != null, newRecord);
        }

        public virtual async Task<IDataResult<Entity>> Get(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter)
        {
            var Record = await baseDal.Get(cancellationToken, Filter);
            return new DataResult<Entity>(Record != null, Record);
        }

        public virtual async Task<IDataResult<Entity[]>> GetAll(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter = null)
        {
            var Records = await baseDal.GetAll(cancellationToken, Filter);
            return new DataResult<Entity[]>(Records != null, Records);
        }

        public virtual async Task<IDataResult<int>> Remove(Entity entity, CancellationToken cancellationToken)
        {
            var DeletedRows = await baseDal.Remove(entity, cancellationToken);
            return new DataResult<int>(DeletedRows > 0, DeletedRows);
        }

        public virtual async Task<IDataResult<Entity>> Update(Entity entity, CancellationToken cancellationToken)
        {
            var UpdatedRecord =  await baseDal.Update(entity, cancellationToken);
            return new DataResult<Entity>(UpdatedRecord != null, UpdatedRecord);
        }
    }
}
