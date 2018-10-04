using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingStartedNormal
{
    class Program
    {
        static void Main(string[] args)
        {

            var repository = NormalRepoFactory.Create<Customers>();
            var result = repository.Query()
                .Select(e => e.CustomerID, e => e.CompanyName, e => e.Address, e => e.EmployeeID)
                .Top(10)
                .Where(e => e.CustomerID == "ok")
                .And(e => e.Country == "china")
                .Where(e => e.Fax == "xxx")
                .InnerJoin<Employees>()
                .On<Employees>((r, l) => r.CustomerID == l.FirstName, l => l.EmployeeID);

            Console.WriteLine(result.Sql());
            //  var h=  result.Go();
            Console.ReadLine();
        }
    }
}
