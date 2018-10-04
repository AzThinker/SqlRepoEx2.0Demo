using System;
using SqlRepoEx;
using SqlRepoEx.MsSqlServer.ConnectionProviders;
using SqlRepoEx.Static;

namespace GettingStartedStatic
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connectionProvider = new AppConfigFirstConnectionProvider();
            MsSqlRepoFactory.UseConnectionProvider(connectionProvider);
            var gettingStarted = new GettingStarted();
            // gettingStarted.DoIt();
            // gettingStarted.DoItJoin();
            gettingStarted.DoItUnion();
            // gettingStarted.DoItUpdata();
            //gettingStarted.DoItNested();
            //gettingStarted.DoItTaskRemark();
            Console.ReadLine();
        }
    }
}