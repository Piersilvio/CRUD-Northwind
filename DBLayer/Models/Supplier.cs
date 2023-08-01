
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Models
{
    [Index("CompanyName", Name = "CompanyName")]
    [Index("PostalCode", Name = "PostalCode")]
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }
        [StringLength(30)]
        public string? ContactName { get; set; } = string.Empty;
        [StringLength(30)]
        public string? ContactTitle { get; set; } = string.Empty;
        [StringLength(60)]
        public string? Address { get; set; } = string.Empty;
        [StringLength(15)]
        public string? City { get; set; } = string.Empty;
        [StringLength(15)]
        public string? Region { get; set; } = string.Empty;
        [StringLength(10)]
        public string? PostalCode { get; set; } = string.Empty;
        [StringLength(15)]
        public string? Country { get; set; } = string.Empty;
        [StringLength(24)]
        public string? Phone { get; set; } = string.Empty;
        [StringLength(24)]
        public string? Fax { get; set; } = string.Empty;
        [Column(TypeName = "ntext")]
        public string? HomePage { get; set; } = string.Empty;

        //[InverseProperty("Supplier")]
        //public virtual ICollection<Products> Products { get; set; } = new HashSet<Products>();
    }
}