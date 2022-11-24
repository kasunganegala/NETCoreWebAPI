using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Project
{
    public class ProjectsSearchResponse
    {
        public int? Id { get; set; }
        public int? TenderId { get; set; }
        public int? ContractorId { get; set; }
        public string? Name { get; set; }
        public string? CustomerName { get; set; }
        public string? ContractorName { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool IsSubmitted { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }
        public string? CreatedByUsername { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
    }
}
