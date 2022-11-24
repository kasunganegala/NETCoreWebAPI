using DataAccess.Models;
using DataAccess.Models.Tender;
using DataAccess.Models.Constants;
using System.Collections.Generic;
using DataAccess.Models.Project;
using DataAccess.Models.Cost;
using System;

namespace NETCoreWebAPI.BusinessRules.Project
{
    public static class ProjectBusinessRules
    {
        public static BidDBModel GenerateBidModel(ProjectModel request)
        {
            BidDBModel bidDBModel = new BidDBModel
            {
                Id = request.Id,
                TenderId = request.TenderId,
                ContractorId = request.ContractorId,
                Name = request.Name,
                StartDateTime = (System.DateTime)request.StartDateTime,
                EndDateTime = (System.DateTime)request.EndDateTime,
                IsSubmitted = request.IsSubmitted,
                Status = BidStatus.Submitted,
                Comment = request.Comment,
                CreatedByUsername = request.CreatedByUsername,
                CreatedDateTime = System.DateTime.Now,
                BidTasks = new List<BidTasksDBModel>(),
                Materials = new List<BidMaterialDBModel>(),
                Equipments = new List<BidEquipmentDBModel>(),
                Labours = new List<BidLabourDBModel>(),
                MaterialCostTotal = request.MaterialCostTotal,
                EquipmentCostTotal = request.EquipmentCostTotal,
                LabourCostTotal = request.LabourCostTotal,
                Tax = request.Tax,
                CostTotal = request.CostTotal,
                MaterialsProfit = request.MaterialsProfit,
                EquipmentsProfit = request.EquipmentsProfit,
                LaboursProfit = request.LaboursProfit,
                ProfitTotal = request.ProfitTotal,
            };

            foreach (BidTasksDBModel item in request.BidTasks)
            {
                bidDBModel.BidTasks.Add(
                    new BidTasksDBModel
                    {
                        BidId = item.BidId,
                        TaskId = item.TaskId,
                        ParentTaskId = item.ParentTaskId,
                        Task = item.Task,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        CreatedByUsername = request.CreatedByUsername,
                        CreatedDateTime = System.DateTime.Now
                    });
            }

            foreach (BidMaterialDBModel item in request.Materials)
            {
                bidDBModel.Materials.Add(
                    new BidMaterialDBModel
                    {
                        MaterialId = item.MaterialId,
                        Name = item.Name,
                        UnitCost = item.UnitCost,
                        UOMId = item.UOMId,
                        Quantity = item.Quantity,
                        Profit = item.Profit,
                        TotalCost = item.TotalCost,
                        CreatedByUsername = request.CreatedByUsername,
                        CreatedDateTime = System.DateTime.Now
                    });
            }

            foreach (BidEquipmentDBModel item in request.Equipments)
            {
                bidDBModel.Equipments.Add(
                    new BidEquipmentDBModel
                    {
                        EquipmentId = item.EquipmentId,
                        Name = item.Name,
                        UnitCost = item.UnitCost,
                        UOMId = item.UOMId,
                        Quantity = item.Quantity,
                        Profit = item.Profit,
                        TotalCost = item.TotalCost,
                        CreatedByUsername = request.CreatedByUsername,
                        CreatedDateTime = System.DateTime.Now
                    });
            }

            foreach (BidLabourDBModel item in request.Labours)
            {
                bidDBModel.Labours.Add(
                    new BidLabourDBModel
                    {
                        LabourId = item.LabourId,
                        Name = item.Name,
                        UnitCost = item.UnitCost,
                        UOMId = item.UOMId,
                        Quantity = item.Quantity,
                        Profit = item.Profit,
                        TotalCost = item.TotalCost,
                        CreatedByUsername = request.CreatedByUsername,
                        CreatedDateTime = System.DateTime.Now
                    });
            }

            return bidDBModel;
        }

        public static List<BidEquipmentDBModel> NormalizeEquipments(List<BidEquipmentDBModel> request) 
        {
            List<BidEquipmentDBModel> temp = request;
            foreach (BidEquipmentDBModel item in temp)
            {
                item.UnitCost = Math.Round((double)item.TotalCost / (double)item.Quantity, 2);
            }
            return temp;
        }

        public static List<BidLabourDBModel> NormalizeLabours(List<BidLabourDBModel> request)
        {
            List<BidLabourDBModel> temp = request;
            foreach (BidLabourDBModel item in temp)
            {
                item.UnitCost = Math.Round((double)item.TotalCost / (double)item.Quantity, 2);
            }
            return temp;
        }

        public static List<BidMaterialDBModel> NormalizeMaterials(List<BidMaterialDBModel> request)
        {
            List<BidMaterialDBModel> temp = request;
            foreach (BidMaterialDBModel item in temp)
            {
                item.UnitCost = Math.Round((double)item.TotalCost / (double)item.Quantity, 2);
            }
            return temp;
        }



    }
}
