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

            services.AddMvc();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:37374",
                                        "https://fzapphmg.azurewebsites.net")
                           .AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePages();
            }

            app.UseExceptionHandler();
            app.UseStatusCodePages();
            app.UseMiddleware<ErrorHandlingMiddleware>();  
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc();

            ConfigureSitef();

        }

        private void ConfigureSitef()
        {
            var config = Configuration.GetSection("AppSettings").Get<AppSettings>();

            ConfigfSitefInterativoModel confSitefModel = new ConfigfSitefInterativoModel(config.SiTefIp, config.IdLoja, config.IdTerminal);

            var value = CliSiTefMethods.ConfiguraIntSiTefInterativo(confSitefModel.IpSiTef, confSitefModel.IdLoja, confSitefModel.IdTerminal, confSitefModel.Reservado);

            if (value != 0)
            {
                TefMessages.ConfigErrors(value);
            }

            var keepAlive = CliSiTefMethods.KeepAlivePinPad();
            TefMessages.PinpadErrors(keepAlive);

            Console.WriteLine("Endereço IP SiTef: {0} ", config.SiTefIp);
            Console.WriteLine("Id da Loja: {0} ", config.IdLoja);
            Console.WriteLine("Id do Terminal (PDV): {0} ", config.IdTerminal);
        }
        
    }
}
