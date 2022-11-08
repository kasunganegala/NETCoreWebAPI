using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class TenderTasksDBModel
    {
        public int Id { get; set; }
        public int TenderId { get; set; }
        public int? ParentTenderTaskId { get; set; }
        public string Task { get; set; }
        public string CreatedByUsername { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
	}
}
