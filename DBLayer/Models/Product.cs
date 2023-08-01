
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Models
{
    [Index("CategoryID", Name = "CategoriesProducts")]
    [Index("CategoryID", Name = "CategoryID")]
    [Index("ProductName", Name = "ProductName")]
    [Index("SupplierID", Name = "SupplierID")]
    [Index("SupplierID", Name = "SuppliersProducts")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        [StringLength(40)]
        public string ProductName { get; set; } = string.Empty;
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        [StringLength(20)]
        public string QuantityPerUnit { get; set; } = string.Empty;
        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        [Required]
        public bool Discontinued { get; set; }

        //[ForeignKey("SupplierID")]
        //[InverseProperty("Products")]
        //public virtual Suppliers? Supplier { get; set; }
    }
}