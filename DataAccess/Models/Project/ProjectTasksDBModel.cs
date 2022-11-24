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
	}
}
