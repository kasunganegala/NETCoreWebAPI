using Dapper;
using DataAccess.DBAccess;
using DataAccess.Models;
using DataAccess.Models.Bid;
using DataAccess.Models.Common;
using DataAccess.Models.Cost;
using DataAccess.Models.Tender;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class BidData : IBidData
    {
        private readonly ISqlDataAccess _db;

        public BidData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<int> InsertNewBid(BidDBModel bid)
        {
            DataTable dt = GenerateBidTasksDataTable(bid.BidTasks);
            DataTable dt2 = GenerateMeterialsDataTable(bid.Materials);
            DataTable dt3 = GenerateEquipmentsDataTable(bid.Equipments);
            DataTable dt4 = GenerateLaboursDataTable(bid.Labours);

            var param = new DynamicParameters();
            param.Add("@BidTasks", dt, DbType.Object);
            param.Add("@Materials", dt2, DbType.Object);
            param.Add("@Equipments", dt3, DbType.Object);
            param.Add("@Labours", dt4, DbType.Object);

            param.Add("@Id", bid.Id);
            param.Add("@TenderId", bid.TenderId);
            param.Add("@ContractorId", bid.ContractorId);
            param.Add("@Name", bid.Name);
            param.Add("@StartDateTime", bid.StartDateTime);
            param.Add("@EndDateTime", bid.EndDateTime);
            param.Add("@IsSubmitted", bid.IsSubmitted);
            param.Add("@Status", bid.Status);
            param.Add("@Comment", bid.Comment);
            param.Add("@CreatedByUsername", bid.CreatedByUsername);
            param.Add("@CreatedDateTime", bid.CreatedDateTime);
            param.Add("@LastModifiedDateTime", bid.LastModifiedDateTime);

            param.Add("@MaterialCostTotal", bid.MaterialCostTotal);
            param.Add("@EquipmentCostTotal", bid.EquipmentCostTotal);
            param.Add("@LabourCostTotal", bid.LabourCostTotal);
            param.Add("@Tax", bid.Tax);
            param.Add("@CostTotal", bid.CostTotal);
            param.Add("@MaterialsProfit", bid.MaterialsProfit);
            param.Add("@EquipmentsProfit", bid.EquipmentsProfit);
            param.Add("@LaboursProfit", bid.LaboursProfit);
            param.Add("@ProfitTotal", bid.ProfitTotal);

            var result = _db.SaveData<int, DynamicParameters>("dbo.spBid_Insert", param);

            return result;
        }

        private DataTable GenerateBidTasksDataTable(List<BidTasksDBModel>? BidTasks)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("BidId");
            dt.Columns.Add("TaskId");
            dt.Columns.Add("ParentTaskId");
            dt.Columns.Add("Task");
            dt.Columns.Add("CreatedByUsername");
            dt.Columns.Add("StartDateTime");
            dt.Columns.Add("EndDateTime");
            dt.Columns.Add("CreatedDateTime");
            dt.Columns.Add("LastModifiedDateTime");

            foreach (BidTasksDBModel tbid in BidTasks)
            {
                dt.Rows.Add(
                    tbid.Id,
                    tbid.BidId,
                    tbid.TaskId,
                    tbid.ParentTaskId,
                    tbid.Task,
                    tbid.CreatedByUsername,
                    tbid.StartDate?.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    tbid.EndDate?.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    tbid.CreatedDateTime.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    tbid.LastModifiedDateTime?.Date.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            return dt;
        }

        private DataTable GenerateLaboursDataTable(List<BidLabourDBModel>? labours)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("BidId");
            dt.Columns.Add("LabourId");
            dt.Columns.Add("Name");
            dt.Columns.Add("UnitCost");
            dt.Columns.Add("UOMId");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Profit");
            dt.Columns.Add("TotalCost");
            dt.Columns.Add("CreatedByUsername");
            dt.Columns.Add("CreatedDateTime");
            dt.Columns.Add("LastModifiedDateTime");

            foreach (BidLabourDBModel item in labours)
            {
                dt.Rows.Add(
                    item.Id,
                    null,
                    item.LabourId,
                    item.Name,
                    item.UnitCost,
                    item.UOMId,
                    item.Quantity,
                    item.Profit,
                    item.TotalCost,
                    item.CreatedByUsername,
                    item.CreatedDateTime.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    item.LastModifiedDateTime?.Date.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            return dt;
        }

        private DataTable GenerateEquipmentsDataTable(List<BidEquipmentDBModel>? equipments)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("BidId");
            dt.Columns.Add("EquipmentId");
            dt.Columns.Add("Name");
            dt.Columns.Add("UnitCost");
            dt.Columns.Add("UOMId");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Profit");
            dt.Columns.Add("TotalCost");
            dt.Columns.Add("CreatedByUsername");
            dt.Columns.Add("CreatedDateTime");
            dt.Columns.Add("LastModifiedDateTime");

            foreach (BidEquipmentDBModel item in equipments)
            {
                dt.Rows.Add(
                    item.Id,
                    null,
                    item.EquipmentId,
                    item.Name,
                    item.UnitCost,
                    item.UOMId,
                    item.Quantity,
                    item.Profit,
                    item.TotalCost,
                    item.CreatedByUsername,
                    item.CreatedDateTime.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    item.LastModifiedDateTime?.Date.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            return dt;
        }

        private DataTable GenerateMeterialsDataTable(List<BidMaterialDBModel>? materials)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("BidId");
            dt.Columns.Add("MaterialId");
            dt.Columns.Add("Name");
            dt.Columns.Add("UnitCost");
            dt.Columns.Add("UOMId");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Profit");
            dt.Columns.Add("TotalCost");
            dt.Columns.Add("CreatedByUsername");
            dt.Columns.Add("CreatedDateTime");
            dt.Columns.Add("LastModifiedDateTime");

            foreach (BidMaterialDBModel item in materials)
            {
                dt.Rows.Add(
                    item.Id,
                    null,
                    item.MaterialId,
                    item.Name,
                    item.UnitCost,
                    item.UOMId,
                    item.Quantity,
                    item.Profit,
                    item.TotalCost,
                    item.CreatedByUsername,
                    item.CreatedDateTime.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    item.LastModifiedDateTime?.Date.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            return dt;
        }

        public async Task<BidDBModel?> GetBid(int id)
        {
            var results = await _db.LoadData<BidDBModel, dynamic>(
                "dbo.spBid_Get",
                new { Id = id });

            return results.FirstOrDefault();
        }

        public async Task<List<BidTasksDBModel>?> GetBidTasks(int id)
        {
            var results = await _db.LoadData<BidTasksDBModel, dynamic>(
                "dbo.spBidTasks_Get",
                new { Id = id });

            var bidTaskData = results.ToList();

            return bidTaskData;
        }

        public async Task<List<BidMaterialDBModel>?> GetBidMaterials(int id)
        {
            var results = await _db.LoadData<BidMaterialDBModel, dynamic>(
                "dbo.spBidMaterials_Get",
                new { Id = id });

            return results.ToList();
        }

        public async Task<List<BidLabourDBModel>?> GetBidLabours(int id)
        {
            var results = await _db.LoadData<BidLabourDBModel, dynamic>(
                "dbo.spBidLabours_Get",
                new { Id = id });

            return results.ToList();
        }

        public async Task<List<BidEquipmentDBModel>?> GetBidEquipments(int id)
        {
            var results = await _db.LoadData<BidEquipmentDBModel, dynamic>(
                "dbo.spBidEquipments_Get",
                new { Id = id });

            return results.ToList();            
        }

        public async Task<Grid<BidsSearchResponse>> GetBids(BidsSearchRequest searchRequest)
        {
            Grid<BidsSearchResponse> bidGrid = new Grid<BidsSearchResponse>();

            var param = new DynamicParameters();
            param.Add("@Limit", searchRequest.Limit);
            param.Add("@Offset", searchRequest.Offset);
            param.Add("@Customer", searchRequest.Customer);
            param.Add("@TenderType", searchRequest.TenderType);
            param.Add("@ProjectType", searchRequest.ProjectType);
            param.Add("@StartDate", searchRequest.StartDate);
            param.Add("@EndDate", searchRequest.EndDate);
            param.Add("@UserRole", searchRequest.UserRole);
            param.Add("@Contractor", searchRequest.Contractor);
            param.Add("@Status", searchRequest.Status);
            param.Add("@SubmittedDate", searchRequest.SubmittedDate);
            param.Add("@NoOfRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var results = await _db.LoadData<BidsSearchResponse, dynamic>("dbo.spBids_Get", param);

            bidGrid.Total = param.Get<int>("@NoOfRecords");
            bidGrid.Data = results;

            return bidGrid;
        }

        public async Task<Grid<BidsSearchResponse>> GetBidsExport(BidsSearchRequest searchRequest)
        {
            Grid<BidsSearchResponse> bidGrid = new Grid<BidsSearchResponse>();

            var param = new DynamicParameters();
            param.Add("@Customer", searchRequest.Customer);
			param.Add("@Limit", 999);
			param.Add("@Offset", 0);
			param.Add("@TenderType", searchRequest.TenderType);
            param.Add("@ProjectType", searchRequest.ProjectType);
            param.Add("@StartDate", searchRequest.StartDate);
            param.Add("@EndDate", searchRequest.EndDate);
            param.Add("@UserRole", searchRequest.UserRole);
            param.Add("@Contractor", searchRequest.Contractor);
            param.Add("@Status", searchRequest.Status);
            param.Add("@SubmittedDate", searchRequest.SubmittedDate);
            param.Add("@NoOfRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var results = await _db.LoadData<BidsSearchResponse, dynamic>("dbo.spBids_Get", param);

            bidGrid.Total = param.Get<int>("@NoOfRecords");
            bidGrid.Data = results;

            return bidGrid;
        }

        public Task<int> ApproveBid(int id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id);

            return _db.SaveData<int, DynamicParameters>("dbo.spBidApproveProject_Get", param);
        }
        public Task<int> RejectBid(int id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id);

            return _db.SaveData<int, DynamicParameters>("dbo.spBidRejectBid_Get", param);
        }
    }
}
