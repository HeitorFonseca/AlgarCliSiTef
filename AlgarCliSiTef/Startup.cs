using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlgarCliSiTef.Filters;
using AlgarCliSiTef.Middleware;
using Core.TEF.Model;
using Core.TEF.Service;
using Core.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlgarCliSiTef
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new BusinessExceptionFilter());
            //});

            services.AddMvc();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:37374").AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                //app.UseStatusCodePages();

            }
           
            app.UseExceptionHandler("/Errors");
            app.UseStatusCodePages();
            app.UseMiddleware<ErrorHandlingMiddleware>();  
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc();

            ConfigfSitefInterativoModel confSitefModel = new ConfigfSitefInterativoModel("localhost", "00000000", "PV000001");

            var value = CliSiTefMethods.ConfiguraIntSiTefInterativo(confSitefModel.IpSiTef, confSitefModel.IdLoja, confSitefModel.IdTerminal, confSitefModel.Reservado);

            if (value != 0)
            {
                TefMessages.ConfigErrors(value);
            }

            var keepAlive = CliSiTefMethods.KeepAlivePinPad();
            TefMessages.PinpadErrors(keepAlive);
        }
    }
}
