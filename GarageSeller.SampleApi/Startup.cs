using GarageSeller.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GarageSeller.Context.Interfaces;

using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace GarageSeller.SampleApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GarageSellerContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DockerComposeConnection")));

            /// PAUSE !
            /// 
            /// With this AddDbContext, we are tied in the WHOLE application to an implementation (GarageSellerContext) 
            /// so we cannot leverage on a REAL dependency injection.
            /// Currently, asking for a GarageSellerContext will use the configuration done in AddDbContext
            /// 
            /// (taken from source)
            /// 
            /// public virtual EntityFrameworkServicesBuilder AddDbContext<TContext>(
            ///        [CanBeNull] Action<DbContextOptionsBuilder> optionsAction = null) where TContext : DbContext
            ///{
            ///    _serviceCollection.AddSingleton(_ => DbContextOptionsFactory<TContext>(optionsAction));
            ///    _serviceCollection.AddSingleton<DbContextOptions>(p => p.GetRequiredService<DbContextOptions<TContext>>());
            ///
            ///    _serviceCollection.AddScoped(typeof(TContext), DbContextActivator.CreateInstance<TContext>);
            ///
            ///    return this;
            ///}
            /// ... so basically it registers the GarageSellerContext 
            /// We are now tempted to register now its interface:
            /// 
            /// services.AddScoped<IGarageSellerContext, GarageSellerContext>();
            /// 
            /// But this will try to create context by its parameterless constructor (protected) which 
            /// calls OnConfigured and which in turn seem to fail because no WhateverOptionBuilder was passed
            /// So the solution is to register instead the "resolving" of the implementation which will use the 
            /// beforementioned pipeline
            /// 
            /// Processed and understood from https://www.jerriepelser.com/blog/resolve-dbcontext-as-interface-in-aspnet5-ioc-container/
            /// once again in my quest to understand what I am really using

            services.AddScoped<IGarageSellerContext>(provider => provider.GetService<GarageSellerContext>());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
