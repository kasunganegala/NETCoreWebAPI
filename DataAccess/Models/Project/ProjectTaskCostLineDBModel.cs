using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Project
{
    public class ProjectTaskCostLineDBModel
    {
        public string? Name { get; set; }
        public int? UOMId { get; set; }
        public double? UnitCost { get; set; }
        public double? CostTotal { get; set; }
        public double? Quantity { get; set; }
    }
}
