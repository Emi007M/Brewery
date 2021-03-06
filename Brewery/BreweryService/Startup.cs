﻿using BreweryService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BreweryService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // requires using BreweryService.Model; and
        // using Microsoft.EntityFrameworkCore;
        public void ConfigureServices(IServiceCollection services)
        {
            ;
            services.AddDbContext<BeerContext>(opt => opt.UseSqlite(Configuration.GetValue<string>("DatabaseConnectionString", "Data Source=brewery.db")));

            services.AddMvc();

            // services.AddScoped<IBeerRepository, BeerRepository>();
            services.AddSingleton<IBeerRepository, BeerRepository>();
            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            app.UseMvc();
        }
    }
}