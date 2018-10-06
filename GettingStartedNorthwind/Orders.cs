using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlRepoEx.Core.CustomAttribute;

namespace GettingStartedNorthwind
{
    public class Orders
    {
        [KeyFiled]
        public int OrderID { get; set; }

        public string CustomerID { get; set; }

        /// <summary>
        /// 关联 Customer 表用
        /// </summary>

        public string CompanyName { get; set; }

        public int? EmployeeID { get; set; }

        /// <summary>
        /// 关联Employee表用
        /// </summary>

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int? ShipVia { get; set; }

        public decimal? Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipCity { get; set; }

        public string ShipRegion { get; set; }

        public string ShipPostalCode { get; set; }

        public string ShipCountry { get; set; }
    }
}
