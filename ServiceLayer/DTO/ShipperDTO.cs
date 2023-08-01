
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO
{
    public class ShipperDTO
    {
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; } = string.Empty;

        //public virtual ICollection<Orders> Orders { get; set; } = new HashSet<Orders>();
    }
}
