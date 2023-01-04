using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Tender
{
    public class TenderSearchRequest
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public int? Customer { get; set; }
        public int? TenderType { get; set; }
        public int? ProjectType { get; set; }
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? UserRole { get; set; }
    }
}
