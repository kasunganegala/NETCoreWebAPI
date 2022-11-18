using Dapper;
using DataAccess.DBAccess;
using DataAccess.Models;
using DataAccess.Models.Bid;
using DataAccess.Models.Common;
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

            foreach (var tbid in bid.BidTasks)
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

            var param = new DynamicParameters();
            param.Add("@BidTasks", dt, DbType.Object);
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

            var result = _db.SaveData<int, DynamicParameters>("dbo.spBid_Insert", param);

            return result;
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

        //public Task<int> SetTenderClose(int id)
        //{
        //    var param = new DynamicParameters();
        //    param.Add("@Id", id);

        //    var result = _db.SaveData<int, DynamicParameters>("dbo.spTenderClose_Get", param);

        //    return result;
        //}
        //public Task<int> SetTenderHold(int id)
        //{
        //    var param = new DynamicParameters();
        //    param.Add("@Id", id);

        //    var result = _db.SaveData<int, DynamicParameters>("dbo.spTenderHold_Get", param);

        //    return result;
        //}
    }
}
