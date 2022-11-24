using DataAccess.Models.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Project
{
    public class ProjectDBModel
    {
        public int? Id { get; set; }
        public int? BidId { get; set; }
        public int? ContractorId { get; set; }
		public string? ContractorName { get; set; }
		public string Name { get; set; }
        public DateTime? EstimatedStartDateTime { get; set; }
        public DateTime? EstimatedEndDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool IsSubmitted { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }
        public string? CreatedByUsername { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public List<ProjectTasksDBModel> ProjectTasks { get; set; }
        public List<ProjectMaterialDBModel>? Materials { get; set; }
        public List<ProjectEquipmentDBModel>? Equipments { get; set; }
        public List<ProjectLabourDBModel>? Labours { get; set; }
        public double? EstimatedMaterialCostTotal { get; set; }
        public double? EstimatedEquipmentCostTotal { get; set; }
        public double? EstimatedLabourCostTotal { get; set; }
        public double? EstimatedTax { get; set; }
        public double? EstimatedCostTotal { get; set; }
        public double? MaterialCostTotal { get; set; }
        public double? EquipmentCostTotal { get; set; }
        public double? LabourCostTotal { get; set; }
        public double? Tax { get; set; }
        public double? CostTotal { get; set; }
        public double? MaterialsProfit { get; set; }
        public double? EquipmentsProfit { get; set; }
        public double? LaboursProfit { get; set; }
        public double? ProfitTotal { get; set; }
        public string? BidName { get; set; }
        public int? CustomerId { get; set; }
        public int? TenderType { get; set; }
		public int? ProjectType { get; set; }
	}
}
