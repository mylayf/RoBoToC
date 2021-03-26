using Microsoft.EntityFrameworkCore;
using RoBoToC.Entity.Abstract;
using RoBoToC.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Query;
using RoBoToC.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;

namespace RoBoToC.DataAccess
{
    public class RobotocDbContext : DbContext
    {
        private IUserClaim UserClaim;
        public RobotocDbContext(IUserClaim userClaim)
        {
            UserClaim = userClaim;
        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserApi> UserApis { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<UserWhiteCurrency> UserWhiteCurrencies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderTime> OrderTimes { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<CompletedProcess> CompletedProcesses { get; set; }
        public DbSet<CurrentProcess> CurrentProcesses { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost\MSSQLSERVER16;Initial Catalog=Robotoc;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserWhiteCurrency>().HasKey(x => new { x.UserId, x.CurrencyId });
            modelBuilder.Entity<UserApi>().HasKey(x => new { x.UserId, x.Market });

            modelBuilder.Entity<User>()
                .HasMany(x => x.WhiteCurrencies)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Claims)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            base.OnModelCreating(modelBuilder);

            var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(x => x.GetName().Name == "RoBoToC.Entity");
            var UserEntities = assembly.GetTypes()
                .Where(x => x.IsClass
                && x.Namespace == "RoBoToC.Entity.Concrete"
                && typeof(IUserEntity).IsAssignableFrom(x));
            foreach (var item in UserEntities)
            {
                if (UserClaim != null)
                {
                    Expression<Func<IUserEntity, bool>> filter = x => x.UserId == UserClaim.UserId;
                    var typeParam = Expression.Parameter(item);
                    var expression = ReplacingExpressionVisitor.Replace(filter.Parameters.Single(), typeParam, filter.Body);
                    modelBuilder.Entity(item).HasQueryFilter(Expression.Lambda(expression, typeParam));
                }
            }
        }
    }
}
