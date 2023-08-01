
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Models
{
    [Index("City", Name = "City")]
    [Index("CompanyName", Name = "CompanyName")]
    [Index("PostalCode", Name = "PostalCode")]
    [Index("Region", Name = "Region")]
    public class Customer
    {
        [Key]
        [StringLength(5)]
        public string CustomerID { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }
        [StringLength(30)]
        public string ContactName { get; set; } = string.Empty;
        [StringLength(30)]
        public string ContactTitle { get; set; } = string.Empty;
        [StringLength(60)]
        public string Address { get; set; } = string.Empty;
        [StringLength(15)]
        public string City { get; set; } = string.Empty;
        [StringLength(15)]
        public string Region { get; set; } = string.Empty;
        [StringLength(10)]
        public string PostalCode { get; set; } = string.Empty;
        [StringLength(15)]
        public string Country { get; set; } = string.Empty;
        [StringLength(24)]
        public string Phone { get; set; } = string.Empty;
        [StringLength(24)]
        public string Fax { get; set; } = string.Empty;

        //[InverseProperty("Customer")]
        //public virtual ICollection<Orders> Orders { get; set; } = new HashSet<Orders>();
    }
}