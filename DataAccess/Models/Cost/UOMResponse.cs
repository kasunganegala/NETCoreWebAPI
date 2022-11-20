using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Cost
{
    public class UOMResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? UOM { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public bool? IsCost { get; set; }
        public int? MeasurementId { get; set; }
        public bool? UsedInMaterials { get; set; }
        public bool? UsedInEquipment { get; set; }
        public bool? UsedInLabour { get; set; }
        
    }
}
