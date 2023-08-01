
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Models
{
    public class Shipper
    {
        [Key]
        public int ShipperID { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; } = string.Empty;
        [StringLength(24)]
        public string Phone { get; set; } = string.Empty;

        //[InverseProperty("ShipViaNavigation")]
        //public virtual ICollection<Orders> Orders { get; set; } = new HashSet<Orders>();
    }
}