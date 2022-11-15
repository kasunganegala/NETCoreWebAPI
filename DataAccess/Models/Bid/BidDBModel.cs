using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class BidDBModel
    {
        public int? Id { get; set; }
        public int? TenderId { get; set; }
        public int? ContractorId { get; set; }
        public string Name { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool IsSubmitted { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }
        public string? CreatedByUsername { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public List<BidTasksDBModel> BidTasks { get; set; }        
    }
}
