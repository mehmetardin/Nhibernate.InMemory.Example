using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Issuing.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Data.SQLite;
using System.Linq;

namespace Issuing.API.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        private const string ConnectionString = "FullUri=file:memorydb.db?mode=memory&cache=shared";
        private static SQLiteConnection _connection;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureServices(services =>
            {

                // Remove real DB
                var iSession = services.SingleOrDefault(d => d.ServiceType == typeof(ISession));
                if (iSession != null)
                {
                    services.Remove(iSession);
                }

                // Remove real DB
                var iSessionFactory = services.SingleOrDefault(d => d.ServiceType == typeof(ISessionFactory));
                if (iSessionFactory != null)
                {
                    services.Remove(iSessionFactory);
                }

                services.AddSingleton<ISessionFactory>(factory =>
                {
                    var configuration = Fluently.Configure()
                                .Database(SQLiteConfiguration.Standard.ConnectionString(ConnectionString))
                                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CardMapping>())
                                .ExposeConfiguration(x => x.SetProperty("current_session_context_class", "call"))
                                .BuildConfiguration();

                    // Create the schema in the database
                    // Because it's an in-memory database, we hold this connection open until all the tests are finished
                    var schemaExport = new SchemaExport(configuration);
                    _connection = new SQLiteConnection(ConnectionString);
                    _connection.Open();
                    schemaExport.Execute(false, true, false, _connection, null);

                    return configuration.BuildSessionFactory();
                });

                services.AddScoped<ISession>(factory =>
                   factory
                        .GetServices<ISessionFactory>()
                        .First()
                        .OpenSession()
                );
            });

        }
    }
}
