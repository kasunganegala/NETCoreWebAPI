using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Project
{
    public class ProjectTaskMaterialUsageDBModel
    {
        public int? Id { get; set; }
        public int? MaterialId { get; set; }
        public string? Name { get; set; }
        public double? Quantity { get; set; }
        public string? UOM { get; set; }
        public int? UOMId { get; set; }

    }
}
