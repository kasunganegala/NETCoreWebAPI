﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class TenderDBModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? TenderType { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? CustomerId { get; set; }
        public int? BidsCount { get; set; }
        public string? Status { get; set; }
        public int? ProjectType { get; set; }
        public int? ProjectBudget { get; set; }
        public string? Comment { get; set; }
        public string? CreatedByUsername { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public List<TenderTasksDBModel> TenderTasks { get; set; }
    }
}
