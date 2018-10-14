using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SqlRepoEx;
using SqlRepoEx.Static;

namespace GettingStartedNorthwind
{
    public static class GettingStarted
    {


        public static void DoProcedure()
        {
            var repoHist = MsSqlRepoFactory.Create<CustOrderHist>();

            var paramDefs = new ParameterDefinition[]
                       {
                               new ParameterDefinition
                               {
                                   Name = "CustomerID",
                                   Value = "ALFKI"
                               }

                       };



            var paramDefs2 = new ParameterDefinition[]
                      {
                               new ParameterDefinition
                               {
                                   Name = "CustomerID",
                                   Value = "ALFKI"
                               },
                               new ParameterDefinition
                               {
                                   Name = "Cust",
                                   Value = "Cust",
                                   Direction=ParameterDirection.InputOutput,
                                   Size=100

                               },
                               new ParameterDefinition
                               {
                                   Name = "Cust2",
                                   Value = "Cust2",
                                   Direction=ParameterDirection.Input,
                                   Size=23

                               }


                      };


            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            var json1 = javaScriptSerializer.Serialize(paramDefs2);

            Console.WriteLine(json1);

            var obj = javaScriptSerializer.Deserialize<ParameterDefinition[]>(json1);




            var hist = repoHist.ExecuteQueryProcedure().WithName("CustOrderHist").WithParameters(paramDefs).Go();

            foreach (var item in hist)
            {
                Console.WriteLine($"{item.ProductName}\t{item.Total};");
            }
        }



        /// <summary>
        /// Join 演示
        /// </summary>
        public static void DoJoin()
        {
            var repoCustomers = MsSqlRepoFactory.Create<Orders>();


            var cust = repoCustomers.Query().Select(c => c.OrderID, c => c.CompanyName, c => c.FirstName, c => c.LastName, c => c.OrderDate)
                                  .InnerJoin<Customers>()
                                  .On<Customers>((r, l) => r.CustomerID == l.CustomerID, l => l.CompanyName)
                                  .InnerJoin<Employees>()
                                  .On<Employees>((k, q) => k.EmployeeID == q.EmployeeID, q => q.FirstName, q => q.LastName)
                                  .Top(10);

            Console.WriteLine(cust.Sql());

            foreach (var item in cust.Go())
            {
                Console.WriteLine($"{item.OrderID}\t{item.CompanyName}\t{item.FirstName}\t{item.LastName}\t{item.OrderDate};");
            }

        }



    }
}
