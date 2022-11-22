using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Cost
{
    public class BidEquipmentDBModel
    {
        public int? Id { get; set; }
        public int? BidId { get; set; }
        public int? EquipmentId { get; set; }
        public string? Name { get; set; }
        public double? UnitCost { get; set; }
        public int? UOMId { get; set; }
        public double? Quantity { get; set; }
        public double? Profit { get; set; }
        public double? TotalCost { get; set; }
        public string? CreatedByUsername { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}
