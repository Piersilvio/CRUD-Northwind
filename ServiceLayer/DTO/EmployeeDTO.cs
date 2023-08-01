using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? TitleOfCourtesy { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public DateTime HireDate { get; set; } = DateTime.Now;
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Region { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? HomePhone { get; set; } = string.Empty;
        public string? Extension { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        public int ReportsTo { get; set; }
        public string? PhotoPath { get; set; } = string.Empty;

        //[ForeignKey("ReportsTo")]
        //[InverseProperty("InverseReportsToNavigation")]
        //public virtual Employees ReportsToNavigation { get; set; }
        //[InverseProperty("ReportsToNavigation")]
        //public virtual ICollection<Employees> InverseReportsToNavigation { get; set; } = new HashSet<Employees>();
        //public virtual ICollection<Orders> Orders { get; set; } = new HashSet<Orders>();
    }
}
