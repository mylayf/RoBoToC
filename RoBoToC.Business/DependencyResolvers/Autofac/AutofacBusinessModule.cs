using Autofac;
using Microsoft.AspNetCore.Http;
using RoBoToC.Business.Abstract;
using RoBoToC.Business.Concrete;
using RoBoToC.Core;
using RoBoToC.Core.Utilities.Security.Jwt;
using RoBoToC.DataAccess;
using RoBoToC.DataAccess.Abstract;
using RoBoToC.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>()
            .As<IHttpContextAccessor>()
            .SingleInstance();

            builder.RegisterType<UserClaim>().As<IUserClaim>();

            builder.RegisterType<RobotocDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FullRobotocDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<UserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();

            builder.RegisterType<UserApiDal>().As<IUserApiDal>();
            builder.RegisterType<UserApiManager>().As<IUserApiService>();

            builder.RegisterType<OrderTimeDal>().As<IOrderTimeDal>();
            builder.RegisterType<OrderDal>().As<IOrderDal>();
            builder.RegisterType<OrderManager>().As<IOrderService>();

            builder.RegisterType<CurrentProcessDal>().As<ICurrentProcessDal>();
            builder.RegisterType<CurrentProcessManager>().As<ICurrentProcessService>();

            builder.RegisterType<CompletedProcessDal>().As<ICompletedProcessDal>();
            builder.RegisterType<CompletedProcessManager>().As<ICompletedProcessService>();

            base.Load(builder);
        }
    }
}
