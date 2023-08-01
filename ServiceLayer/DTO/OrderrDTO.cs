
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using DBLayer.Models;

namespace ServiceLayer.DTO
{
    public class OrderrDTO
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; } = string.Empty;
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime RequiredDate { get; set; } = DateTime.Now;
        public DateTime ShippedDate { get; set; } = DateTime.Now;
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; } = string.Empty;
        public string ShipAddress { get; set; } = string.Empty;
        public string ShipCity { get; set; } = string.Empty;
        public string ShipRegion { get; set; } = string.Empty;
        public string ShipPostalCode { get; set; } = string.Empty;
        public string ShipCountry { get; set; } = string.Empty;

        //public virtual Customers? Customer { get; set; }
        //public virtual Employees? Employee { get; set; }
        //public virtual Shippers? ShipViaNavigation { get; set; }
    }
}
