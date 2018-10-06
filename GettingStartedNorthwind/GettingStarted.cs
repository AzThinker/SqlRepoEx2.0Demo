using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlRepoEx.Static;

namespace GettingStartedNorthwind
{
    public static class GettingStarted
    {
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

            foreach(var item in cust.Go())
            {
                Console.WriteLine($"{item.OrderID}\t{item.CompanyName}\t{item.FirstName}\t{item.LastName}\t{item.OrderDate};");
            }

        }



    }
}
