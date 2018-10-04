using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlRepoEx.MySql.ConnectionProviders;
using SqlRepoEx.Static;

namespace GettingStartedMySql
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionProvider = new AppConfigFirstMysqlConnectionProvider();
            MySqlRepoFactory.UseConnectionProvider(connectionProvider);

            var gettingStarted = new GettingStarted();
            gettingStarted.DoIt();

            Console.ReadLine();
        }
    }
}
