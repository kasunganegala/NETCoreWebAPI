using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Project
{
    public class ProjectTaskResponse
    {
        public int? Id { get; set; }
        public int? ProjectId { get; set; }
		public int? TaskId { get; set; }
		public int? ParentTaskId { get; set; }
		public string? Task { get; set; }
        public string? CreatedByUsername { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public string? Status { get; set; }
        public double? MaterialCost { get; set; }
        public double? LabourCost { get; set; }
        public double? EquipmentCost { get; set; }
        public double? TotalCost { get; set; }
        public string? ProjectName { get; set; }

        public List<ProjectTaskCostLineDBModel>? Materials { get; set; }
        public List<ProjectTaskCostLineDBModel>? Labours { get; set; }
        public List<ProjectTaskCostLineDBModel>? Equipments { get; set; }
        public List<dynamic>? WorkLogs { get; set; }

	}
}
