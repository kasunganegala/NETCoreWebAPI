using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Cost
{
    public class LabourResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int? UOMId { get; set; }
        public string? UOMName { get; set; }
        public string? UOM { get; set; }
        public string? UOMType { get; set; }
        public string? UOMDescription { get; set; }
        public bool UOMIsCost { get; set; }
        public bool UOMUsedInMaterials { get; set; }
        public bool UOMUsedInEquipment { get; set; }
        public bool UOMUsedInLabour { get; set; }

        public string? UnitMeasurementUOM { get; set; }
        public string? UnitMeasurementType { get; set; }
    }
}
