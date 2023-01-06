using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Project
{
    public class ProjectTaskWorklogRequest
	{
        public int? ProjectId { get; set; }
        public int? TaskId { get; set; }
        public int? Effort { get; set; }
		
		public DateTime? LogDate { get; set; }
        public string? Comment { get; set; }
		public List<ProjectTaskLabourUsageDBModel>? Labours { get; set; }
		public List<ProjectTaskEquipmentUsageDBModel>? Equipments { get; set; }
		public List<ProjectTaskMaterialUsageDBModel>? Materials { get; set; }

	}
}
