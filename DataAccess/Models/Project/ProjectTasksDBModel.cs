using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Project
{
    public class ProjectTasksDBModel
    {
        public int? Id { get; set; }
        public int? ProjectId { get; set; }
        public int TaskId { get; set; }
        public int? ParentTaskId { get; set; }
        public string Task { get; set; }
        public string CreatedByUsername { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public DateTime? ActualEndDateTime { get; set; }
        public DateTime? ActualStartDateTime { get; set; }
        public int? EstimatedTaskDuration { get; set; }
		public int? ActualTaskDuration { get; set; }
		public int? TaskProgress { get; set; }
		public int? RemaingTaskProgress { get; set; }
		public int? TaskOverDueBy { get; set; }

		public string? Status { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
