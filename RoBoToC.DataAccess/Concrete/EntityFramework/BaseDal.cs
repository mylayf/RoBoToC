using Microsoft.EntityFrameworkCore;
using RoBoToC.DataAccess.Abstract;
using RoBoToC.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.DataAccess.Concrete.EntityFramework
{
    public class BaseDal<Entity> : IBaseDal<Entity> where Entity : class, IEntity, new()
    {
        protected RobotocDbContext RobotocDbContext { get; }
        protected FullRobotocDbContext FullRobotocDbContext { get; set; }

        protected virtual DbSet<Entity> Entities => RobotocDbContext.Set<Entity>();
        protected virtual IQueryable<Entity> Queryable => Entities;

        public BaseDal(RobotocDbContext robotocDbContext, FullRobotocDbContext fullRobotocDbContext = null)
        {
            RobotocDbContext = robotocDbContext;
            FullRobotocDbContext = fullRobotocDbContext;
        }

        public virtual async Task<Entity> Add(Entity entity, CancellationToken cancellationToken)
        {
            var entry = RobotocDbContext.Entry(entity);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await RobotocDbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual async Task<Entity> Get(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter)
        {
            return await Queryable.SingleOrDefaultAsync(Filter, cancellationToken);
        }

        public virtual async Task<Entity[]> GetAll(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter = null)
        {
            return await (Filter != null ? RobotocDbContext.Set<Entity>().Where(Filter).ToArrayAsync(cancellationToken) : Queryable.ToArrayAsync(cancellationToken));
        }

        public virtual async Task<int> Remove(Entity entity, CancellationToken cancellationToken)
        {
            var entry = RobotocDbContext.Entry(entity);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return await RobotocDbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<Entity> Update(Entity entity, CancellationToken cancellationToken)
        {
            var entry = RobotocDbContext.Entry(entity);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await RobotocDbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }
        public virtual async Task<bool> Any(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter)
        {
            return await Queryable.AnyAsync(Filter, cancellationToken);
        }
        public virtual async Task<bool> AnyInAll(CancellationToken cancellationToken, Expression<Func<Entity, bool>> Filter)
        {
            return await FullRobotocDbContext.Set<Entity>().AnyAsync(Filter, cancellationToken);
        }
    }
}
