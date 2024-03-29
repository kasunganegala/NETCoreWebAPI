﻿using DataAccess.Models.Cost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Bid
{
    public class BidModel
    {
        public int? Id { get; set; }
        public int? TenderId { get; set; }
        public int? ContractorId { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string? Status { get; set; }
        public bool IsSubmitted { get; set; }
        public string? Comment { get; set; }
        public string? CreatedByUsername { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public List<BidTasksDBModel>? BidTasks { get; set; }
        public List<BidMaterialDBModel>? Materials { get; set; }
        public List<BidEquipmentDBModel>? Equipments { get; set; }
        public List<BidLabourDBModel>? Labours { get; set; }
        public double? MaterialCostTotal { get; set; }
        public double? EquipmentCostTotal { get; set; }
        public double? LabourCostTotal { get; set; }
        public double? Tax { get; set; }
        public double? CostTotal { get; set; }
        public double? MaterialsProfit { get; set; }
        public double? EquipmentsProfit { get; set; }
        public double? LaboursProfit { get; set; }
        public double? ProfitTotal { get; set; }
    }
}
