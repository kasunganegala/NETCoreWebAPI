using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Project
{
    public class ProjectLabourDBModel
    {
        public int? Id { get; set; }
        public int? ProjectId { get; set; }
        public int? LabourId { get; set; }
        public string? Name { get; set; }
        public int? UOMId { get; set; }
        public double? EstimatedUnitCost { get; set; }
        public double? EstimatedQuantity { get; set; }
        public double? EstimatedTotalCost { get; set; }
        public double? UnitCost { get; set; }
        public double? Quantity { get; set; }
        public double? TotalCost { get; set; }
        public double? Profit { get; set; }
        public string? CreatedByUsername { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}
