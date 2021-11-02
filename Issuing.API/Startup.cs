using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Issuing.Application;
using Issuing.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Linq;

namespace Issuing.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Issuing.API", Version = "v1" });
            });

            services.AddScoped<ICardService, CardService>();

            services.AddSingleton<ISessionFactory>(factory =>
            {
                var fluentConfiguration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                .UsingFile("OceanLocal.db"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CardMapping>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));

                ISessionFactory _session = fluentConfiguration.BuildSessionFactory();
                return _session;
            });

            services.AddScoped<ISession>(factory =>
               factory
                    .GetServices<ISessionFactory>()
                    .First()
                    .OpenSession()
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Issuing.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
