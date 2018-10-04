using Autofac;
using SqlRepoEx.Abstractions;
using SqlRepoEx.MsSqlServer.Abstractions;
using SqlRepoEx.MsSqlServer.ConnectionProviders;
using SqlRepoEx.Autofac;
using System;
using System.Linq.Expressions;

namespace GettingStartedIoC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<MsSqlRepoSqlServerAutofacModule>();

            var connectionProvider = new AppConfigFirstConnectionProvider();
            containerBuilder.RegisterInstance(connectionProvider)
                            .As<IMsSqlConnectionProvider>();

            containerBuilder.RegisterType<GettingStarted>()
                            .As<IGettingStarted>();

            // ... other registrations

            var container = containerBuilder.Build();

            var gettingStarted = container.Resolve<IGettingStarted>();
            gettingStarted.DoIt();
           // gettingStarted.DoIt2();


            //  var exp= Expression<Func<

            Console.ReadLine();
        }
    }
}