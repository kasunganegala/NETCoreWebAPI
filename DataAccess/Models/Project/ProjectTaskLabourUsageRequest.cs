﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Project
{
    public class ProjectTaskLabourUsageRequest
	{
        public int? ProjectId { get; set; }
        public int? TaskId { get; set; }
        public List<ProjectTaskLabourUsageDBModel>? Labours { get; set; }

    }
}
