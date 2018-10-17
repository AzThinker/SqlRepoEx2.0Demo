using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlRepoEx;
using Dapper;
using SqlRepoEx.Adapter.Dapper;
using SqlRepoEx.Core;
using SqlRepoEx.Static;
using MsSQLP = SqlRepoEx.MsSqlServer.ConnectionProviders;
using MySQLP = SqlRepoEx.MySql.ConnectionProviders;

namespace GettingStartedDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            // TestMsSql();
            TestMySql();
            Console.ReadLine();
        }

        public static void TestMySqlUpdate()
        {
            string ConnectionString = "datasource=127.0.0.1;username=test;password=test;database=sqlrepotest;charset=gb2312;SslMode = none;";
            var connectionProvider = new MySQLP.ConnectionStringConnectionProvider(ConnectionString);
            MySqlRepoFactory.UseConnectionProvider(connectionProvider);
            MySqlRepoFactory.UseStatementExecutor(new DapperStatementExecutor(connectionProvider));
            MySqlRepoFactory.UseDataReaderEntityMapper(new DapperEntityMapper());
            MySqlRepoFactory.UseWritablePropertyMatcher(new SimpleWritablePropertyMatcher());
            var repository11 = MySqlRepoFactory.Create<ToDo>();
            var results11 = repository11.Query().Where(c => c.Id == 2).Go().FirstOrDefault();

            results11.Task = "B21";


            var rest2 = repository11.Update().For(results11);


            Console.WriteLine(rest2.ParamSql());

            var rest3 = rest2.ParamSqlWithEntity();

            IDbConnection dbConnection = repository11.GetConnectionProvider.GetDbConnection;

            // with dapper
            dbConnection.Execute(rest3.paramsql, rest3.entity);


        }

        public static void TestMySql()
        {
            string ConnectionString = "datasource=127.0.0.1;username=test;password=test;database=sqlrepotest;charset=gb2312;SslMode = none;";
            var connectionProvider = new MySQLP.ConnectionStringConnectionProvider(ConnectionString);
            MySqlRepoFactory.UseConnectionProvider(connectionProvider);
            MySqlRepoFactory.UseStatementExecutor(new DapperStatementExecutor(connectionProvider));
            MySqlRepoFactory.UseDataReaderEntityMapper(new DapperEntityMapper());
            var repository11 = MySqlRepoFactory.Create<ToDo>();
            var results11 = repository11.Query().Select(e => e.Id, e => e.Task, e => e.CreatedDate).Top(6);

            foreach (var item in results11.Go())
            {
                Console.WriteLine($"{item.Id}\t {item.Task}\t {item.CreatedDate}\t {item.Remark}");
            }

            Console.WriteLine();
            Console.WriteLine();
            var repository = MySqlRepoFactory.Create<ToDo>();
            var results = repository.Query().Select(e => e.Id, e => e.Task, e => e.CreatedDate, e => e.Remark);


            var results5 = repository.Query()
                                  .InnerJoin<TaskRemark>()
                                  .On<TaskRemark>((r, l) => r.Task == l.Task, l => l.Remark)
                                  .Select(e => e.Id, e => e.Task, e => e.CreatedDate, e => e.Remark)
                                  .WhereBetween(e => e.Id, 1, 5);


            var results6 = repository.Query()

                                 .InnerJoin<TaskRemark>()
                                 .On<TaskRemark>((r, l) => r.Task == l.Task, l => l.Remark)
                                 .Select(e => e.Id, e => e.Task, e => e.CreatedDate, e => e.Remark)
                                 .WhereBetween(e => e.Id, 10, 15);


            //   Console.WriteLine(results.UnionSql(new List<UnionSql> { new UnionSql { Sql = results5.Sql(), UnionType = UnionType.Union } }));
            var results2 = results.Union(new List<UnionSql> {
                     UnionSql.New(  results5,UnionType.Union ),
                     UnionSql.New(  results6,UnionType.Union )  });

            foreach (var item in results2)
            {
                Console.WriteLine($"{item.Id}\t {item.Task}\t {item.CreatedDate}\t {item.Remark}");
            }

        }
        public static void TestMsSql()
        {
            string ConnectionString = "Data Source=(Local);Initial Catalog=Northwind;User ID=test;Password=test";
            var connectionProvider = new MsSQLP.ConnectionStringConnectionProvider(ConnectionString);
            MsSqlRepoFactory.UseConnectionProvider(connectionProvider);
            MsSqlRepoFactory.UseStatementExecutor(new DapperStatementExecutor(connectionProvider));
            MsSqlRepoFactory.UseDataReaderEntityMapper(new DapperEntityMapper());

            var repository = MsSqlRepoFactory.Create<Customers>();
            var result = repository.Query().Select(e => e.CustomerID, e => e.CompanyName, e => e.Address).Top(10);

            Console.WriteLine(result.Sql());

            var rs = result.Go();

            foreach (var r in rs)
            {
                Console.WriteLine($"{r.CustomerID}\t {r.CompanyName}\t\t\t {r.Address}");
            }
        }
    }
}
