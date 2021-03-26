using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoBoToC.Core;
using RoBoToC.DataAccess;
using RoBoToC.DataAccess.Concrete.AdoNet;
using RoBoToC.Server.Data;
using RoBoToC.Server.Subscriber;
using RoBoToC.Server.Subscriber.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoBoToC.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserClaim, UserClaim>();
            services.AddDbContext<RobotocDbContext>(options => options.UseSqlServer());
            services.AddTransient<RobotocDbContext>();
            services.AddTransient<FullRobotocDbContext>();
            services.AddSingleton<CurrencyDal>();
            services.AddSingleton<TrackingTimes>();
            services.AddSingleton<ISubscriber, BinanceSubscriber>();
            ISubscriber subscriber =
                new BinanceSubscriber(
                    services.BuildServiceProvider().GetRequiredService<CurrencyDal>(),
                    services.BuildServiceProvider().GetRequiredService<TrackingTimes>()
                    );
            subscriber.StartSubscribe();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
        }
    }
}
