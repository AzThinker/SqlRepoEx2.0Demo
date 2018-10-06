using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlRepoEx;
using SqlRepoEx.MsSqlServer;
using SqlRepoEx.MsSqlServer.ConnectionProviders;
using SqlRepoEx.Static;

namespace GettingStartedNorthwind
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            GettingStarted.DoJoin();

            Console.ReadLine();
        }

        /// <summary>
        /// init
        /// 创建初始方法，初始一个工厂类。
        /// </summary>
        static void Init()
        {
            // Set Connection String
            string ConnectionString = "Data Source=(Local);Initial Catalog=Northwind;User ID=test;Password=test";
            var connectionProvider = new ConnectionStringConnectionProvider(ConnectionString);
            MsSqlRepoFactory.UseConnectionProvider(connectionProvider);

            // this Demo is POJO ,So Using SimpleWritablePropertyMatcher()。
            // 本例中，使用的是简单类，所以用SimpleWritablePropertyMatcher()来操作属性。
            MsSqlRepoFactory.UseWritablePropertyMatcher(new SimpleWritablePropertyMatcher());
        }
    }
}
