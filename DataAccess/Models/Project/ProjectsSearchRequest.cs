using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Project
{
    public class ProjectsSearchRequest
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public int? Customer { get; set; }
        public int? TenderType { get; set; }
        public int? ProjectType { get; set; }
        public int? Contractor { get; set; }
        public string? Status { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? UserRole { get; set; }
    }
}
