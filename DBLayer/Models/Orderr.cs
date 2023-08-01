
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Models
{
    [Index("CustomerID", Name = "CustomerID")]
    [Index("CustomerID", Name = "CustomersOrders")]
    [Index("EmployeeID", Name = "EmployeeID")]
    [Index("EmployeeID", Name = "EmployeesOrders")]
    [Index("OrderDate", Name = "OrderDate")]
    [Index("ShipPostalCode", Name = "ShipPostalCode")]
    [Index("ShippedDate", Name = "ShippedDate")]
    [Index("ShipVia", Name = "ShippersOrders")]
    public class Orderr
    {
        [Key]
        public int OrderID { get; set; }
        [StringLength(5)]
        public string? CustomerID { get; set; } = string.Empty;
        public int EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime RequiredDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime ShippedDate { get; set; } = DateTime.Now;
        public int? ShipVia { get; set; }
        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }
        [StringLength(40)]
        public string? ShipName { get; set; } = string.Empty;
        [StringLength(60)]
        public string? ShipAddress { get; set; } = string.Empty;
        [StringLength(15)]
        public string? ShipCity { get; set; } = string.Empty;
        [StringLength(15)]
        public string? ShipRegion { get; set; } = string.Empty;
        [StringLength(10)]
        public string? ShipPostalCode { get; set; } = string.Empty;
        [StringLength(15)]
        public string? ShipCountry { get; set; } = string.Empty;

        //[ForeignKey("CustomerID")]
        //[InverseProperty("Orders")]
        //public virtual Customer? Customer { get; set; }
        //[ForeignKey("EmployeeID")]
        //[InverseProperty("Orders")]
        //public virtual Employee? Employee { get; set; }
        //[ForeignKey("ShipVia")]
        //[InverseProperty("Orders")]
        //public virtual Shipper? ShipViaNavigation { get; set; }
    }
}