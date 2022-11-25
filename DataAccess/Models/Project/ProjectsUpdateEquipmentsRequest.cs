﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Project
{
    public class ProjectsUpdateEquipmentsRequest
    { 

		public List<ProjectEquipmentDBModel>? ProjectEquipments { get; set; }
        public int? ProjectId { get; set; }
    }
}
