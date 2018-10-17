using System;
using System.Collections.Generic;
using System.Linq;
using SqlRepoEx.Core;
using SqlRepoEx.Static;

namespace GettingStartedStatic
{
    public class GettingStarted
    {
        public void DoIt()
        {
            var repository = MsSqlRepoFactory.Create<ToDo>();

            var results = repository.Query().Select(e => e.Id, e => e.Task)
                                 .Where(c => c.Id == 9)
                                  .Where(c => c.Id > 12)
                                  .And(c => c.Id > 90);



            Console.WriteLine(results.Sql());


            //foreach (var item in results2)
            //{
            //    Console.WriteLine($"{item.Id}\t {item.Task} ");
            //}

        }


        public void  DoParam()
        {

            var repository = MsSqlRepoFactory.Create<ToDo>();

            var results = repository.Query().Where(c => c.Id == 6).Go().FirstOrDefault();

            ToDo toDo = new ToDo();
            toDo.Task = "Atk";


            var resultinsert = repository.Insert().For(results);//.With(c => c.Task, "nkk");
            Console.WriteLine(resultinsert.ParamSql());
            var v = resultinsert.ParamSqlWithEntity();
            Console.WriteLine(v.paramsql);
        }


        public void DoTransactionIt()
        {
            var repository = MsSqlRepoFactory.Create<ToDo>();



            var results = repository.Query().Where(c => c.Id < 6);


            foreach (var item in results.Go())
            {
                Console.WriteLine($"{item.Id}\t {item.Task} ");
            }

            using (var tranc = repository.GetConnectionProvider.BeginTransaction())
            {
                repository.Update().Set(c => c.Task, "A01").Where(c => c.Id == 1).Go();// A1
                repository.Update().Set(c => c.Task, "B01").Where(c => c.Id == 2).Go();// B2

                tranc.Rollback();
            }



            foreach (var item in results.Go())
            {
                Console.WriteLine($"{item.Id}\t {item.Task} ");
            }
            Console.WriteLine(results.Sql());

        }

        public void DoItUnion()
        {
            var repository = MsSqlRepoFactory.Create<ToDo>();

            var results = repository.Query().Select(e => e.Id, e => e.Task);


            var results5 = repository.Query().Select(e => e.Id, e => e.Task)

                          .Where(c => c.Id > 0 && c.Id < 7);

            var results6 = repository.Query()
                           .Select(e => e.Id, e => e.Task)
                          .Where(c => c.Id > 10 && c.Id < 15);

            var results2 = results.Union(new List<UnionSql> {
                     UnionSql.New(  results5,UnionType.Union ),
                     UnionSql.New(  results6,UnionType.Union )  });

            var results3 = results.UnionSql(new List<UnionSql> {
                     UnionSql.New(  results5,UnionType.Union ),
                     UnionSql.New(  results6,UnionType.Union )  });

            Console.WriteLine(results3);


            foreach (var item in results2)
            {
                Console.WriteLine($"{item.Id}\t {item.Task} ");
            }

        }

        /// <summary>
        /// 带括号条件
        /// </summary>
        public void DoItNested()
        {


            var repository = MsSqlRepoFactory.Create<ToDo>();


            var results = repository.Query()

                            .Where(c => c.Id > 0 && c.Id == 3)
                            .NestedAnd(c => c.Id == 2)
                            .Or(c => c.Remark.Contains("a"))
                            .EndNesting()
                            .NestedOr(c => c.IsCompleted == true)
                            .And(c => c.Id == 5)
                            .EndNesting()
                            .OrderBy(e => e.Id);
            Console.WriteLine(results.Sql());
            //foreach (var item in results.Go())
            //{
            //    Console.WriteLine($"{item.Id},{item.Task} ");
            //}
            //   SELECT[dbo].[ToDo].[Remark]
            //, [dbo].[ToDo].[CreatedDate]
            //, [dbo].[ToDo].[IsCompleted]
            //, [dbo].[ToDo].[Task]
            //, [dbo].[ToDo].[Id]
            //        FROM[dbo].[ToDo]
            //        WHERE([dbo].[ToDo].[Id] > 0
            //AND ([dbo].[ToDo].[Id] = 2
            //       OR[dbo].[ToDo].[Id] = 2)
            //OR([dbo].[ToDo].[IsCompleted] = 1
            //AND[dbo].[ToDo].[Id] = 5))
            //ORDER BY[dbo].[ToDo].[Id] ASC;
        }

        /// <summary>
        /// NoLocks
        /// </summary>
        public void DoItNoLocks()
        {
            Console.WriteLine();
            var repository = MsSqlRepoFactory.Create<ToDo>();

            var results = repository.Query()
                                    .NoLocks();

            Console.WriteLine(results.Sql());
        }

        /// <summary>
        /// Insert
        /// </summary>
        public void DoItInsert()
        {
            ToDo toDo = new ToDo() { Id = 10, Task = "B3", IsCompleted = true, CreatedDate = DateTime.Now };
            Console.WriteLine();

            var repository = MsSqlRepoFactory.Create<ToDo>();

            var results1 = repository.Insert().For(toDo);


            Console.WriteLine(results1.Sql());



            Console.WriteLine();

            var results2 = repository.Insert()
                                    .With(e => e.Remark, "this remark")
                                    .With(e => e.Task, "H7")
                                    .With(e => e.CreatedDate, DateTime.Now);

            Console.WriteLine(results2.Sql());

        }

        /// <summary>
        /// Update
        /// </summary>
        public void DoItUpdata()
        {
            ToDo toDo = new ToDo() { Id = 10, Task = "B3", IsCompleted = true, CreatedDate = DateTime.Now };
            Console.WriteLine();

            var repository = MsSqlRepoFactory.Create<ToDo>();

            var results1 = repository.Update().For(toDo);


            Console.WriteLine(results1.Sql());



            Console.WriteLine();

            var results2 = repository.Update()
                                    .Set(e => e.Remark, "this remark")
                                    .Set(e => e.Task, "H7")
                                    .Set(e => e.CreatedDate, DateTime.Now)
                                    .Where(e => e.Id == 10);

            Console.WriteLine(results2.Sql());

        }


        /// <summary>
        /// 分页
        /// </summary>
        public void DoItTopAndPage()
        {
            Console.WriteLine();
            var repository = MsSqlRepoFactory.Create<ToDo>();

            var results1 = repository.Query()
                                    .Top(20);


            Console.WriteLine(results1.Sql());

            var results2 = repository.Query()
                                    .Page(10, 3);

            Console.WriteLine(results2.Sql());
        }

        /// <summary>
        /// Join
        /// </summary>
        public void DoItJoin()
        {
            Console.WriteLine();
            var repository = MsSqlRepoFactory.Create<ToDo>();
            var results1 = repository.Query()
                                    .InnerJoin<TaskRemark>()
                                    // 增加附加条件，如果主选择有此属性，则查询本句中所设置
                                    .On<TaskRemark>((r, l) => r.Task == l.Task, l => l.Remark);

            Console.WriteLine(results1.Sql());


            Console.WriteLine();

            var results2 = repository.Query()
                           .LeftOuterJoin<TaskRemark>()
                           .On<TaskRemark>((r, l) => r.Task == l.Task, l => l.Remark);
            Console.WriteLine(results2.Sql());


            Console.WriteLine();

            var results3 = repository.Query()
                           .RightOuterJoin<TaskRemark>()
                           .On<TaskRemark>((r, l) => r.Task == l.Task, l => l.Remark);
            Console.WriteLine(results3.Sql());
        }

        /// <summary>
        /// 显示TaskRemark表数据
        /// </summary>
        public void DoItTaskRemark()
        {
            var repository = MsSqlRepoFactory.Create<TaskRemark>();
            var results = repository.Query()
                                    .Select(e => e.Id, e => e.Task, e => e.Remark);


            Console.WriteLine(results.Sql());
            foreach (var item in results.Go())
            {
                Console.WriteLine($"{item.Id},{item.Task},{item.Remark} ");
            }

        }
    }
}