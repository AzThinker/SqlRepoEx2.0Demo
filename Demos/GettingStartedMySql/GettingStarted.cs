using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SqlRepoEx;
using SqlRepoEx.Abstractions;
using SqlRepoEx.Core;
using SqlRepoEx.Core.Abstractions;
using SqlRepoEx.Static;
using SqlRepoEx.MySql;
using SqlRepoEx.MySql.ConnectionProviders;

namespace GettingStartedMySql

{

    public class GettingStarted
    {
        public void DoIt()
        {
            var repository = MySqlRepoFactory.Create<ToDo>();
            var results = repository.Query().Select(e => e.Id, e => e.Task, e => e.CreatedDate, e => e.Remark);
            //                       .InnerJoin<TaskRemark>()
            //                       .On<TaskRemark>((r, l) => r.Task == l.Task)

            //                       //.Where(e => e.Id > 0 && e.IsCompleted == true)
            //                       // .WhereIn(e => e.Id, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
            //                       .WhereBetween(e => e.Id, 3, 18)
            //                       .OrderBy(e => e.Id);//.Page(5, 2);


            //Console.WriteLine(results.Sql());

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

            //var paramDef2 = new ParameterDefinition[]
            //             {
            //                    new ParameterDefinition
            //                   {
            //                       Name = "@p1",
            //                       DbType=DbType.Int32,
            //                       Direction=ParameterDirection.Output,
            //                       Value=99,

            //                   },
            //                     new ParameterDefinition
            //                   {
            //                       Name = "@p2",
            //                       DbType=DbType.String,
            //                       Direction=ParameterDirection.Output,
            //                       Size=50

            //                   }

            //             };
            ////IStatementExecutor target = new MySqlStatementExecutor(new SqlLogger(new List<ISqlLogWriter>() { new NoOpSqlLogger() }), new AppConfigFirstMysqlConnectionProvider());
            ////target.ExecuteNonQueryStoredProcedureAsync("mytestp", paramDef2);

            //MySqlRepoFactory.Create().ExecuteNonQueryProcedure().WithName("mytestp").WithParameters(paramDef2).Go();


            //var dataReader2 = MySqlRepoFactory.Create<ToDo>()
            //      .ExecuteQueryProcedure().WithName("mytestp").WithParameters(paramDef2).Go();
            //foreach (var t in dataReader2)
            //{
            //    Console.WriteLine($"{t.Id}\t{t.Task}");
            //}

            //Console.WriteLine();
            //Console.WriteLine($"p1= {paramDef2[0].Value}\tp2={paramDef2[1].Value}");

            //Console.ReadLine();
            //.WithParameter(new ParameterDefinition
            //{
            //    Name = "@p1",
            //    DbType = DbType.Int32,
            //    Direction = ParameterDirection.Output,
            //    Value = 99,

            //}).WithParameter(new ParameterDefinition
            //{
            //    Name = "@p2",
            //    DbType = DbType.String,
            //    Direction = ParameterDirection.Output,
            //    Size = 50

            //}).Go();

            // IDataReader dataReader = target.ExecuteStoredProcedure("CustOrderHist", paramDef);
            //var dataReader2 = target.ExecuteStoredProcedure("mytestp", paramDef2);
            //dataReader2.GetParameterCollection(paramDef2);

            //while(dataReader2.Read())
            //{
            //    Console.WriteLine($"{dataReader2[0]}\t{dataReader2[1]}");
            //}
            // target.ExecuteNonQueryStoredProcedure("mytestp", paramDef2);
            //string cnstr = "datasource=127.0.0.1;username=test;password=test;database=sqlrepotest;charset=gb2312";


            //MySqlConnection conn = new MySqlConnection(cnstr);
            //conn.Open();
            //MySqlCommand MyCommand = new MySqlCommand("mytestp", conn);
            //MyCommand.CommandType = CommandType.StoredProcedure;
            //MyCommand.Parameters.Add(new MySqlParameter("@p1", MySqlDbType.Int32,11));
            //MyCommand.Parameters["@p1"].Direction = ParameterDirection.Output;
            //MyCommand.Parameters.Add(new MySqlParameter("@p2", MySqlDbType.VarChar,50));
            //MyCommand.Parameters["@p2"].Direction = ParameterDirection.Output;
            //MyCommand.ExecuteNonQuery();

            //Console.WriteLine(MyCommand.Parameters["@p1"].Value.ToString());


            Console.ReadLine();
        }


    }
}