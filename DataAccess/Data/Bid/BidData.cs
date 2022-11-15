using Dapper;
using DataAccess.DBAccess;
using DataAccess.Models;
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

        //public async Task<TenderDBModel?> GetTender(int id)
        //{
        //    var results = await _db.LoadData<TenderDBModel, dynamic>(
        //        "dbo.spTender_Get",
        //        new { Id = id });

        //    return results.FirstOrDefault();
        //}

        //public async Task<List<TenderTasksDBModel>?> GetTenderTasks(int id)
        //{
        //    var results = await _db.LoadData<TenderTasksDBModel, dynamic>(
        //        "dbo.spTenderTasks_Get",
        //        new { Id = id });

        //    var tenderTaskData = results.ToList();

        //    return tenderTaskData;
        //}

        //public async Task<Grid<TenderDBModel>> GetTenders(TenderSearchRequest searchRequest)
        //{
        //    Grid<TenderDBModel> tenderGrid = new Grid<TenderDBModel>();

        //    var param = new DynamicParameters();
        //    param.Add("@Limit", searchRequest.Limit);
        //    param.Add("@Offset", searchRequest.Offset);
        //    param.Add("@Customer", searchRequest.Customer);
        //    param.Add("@TenderType", searchRequest.TenderType);
        //    param.Add("@ProjectType", searchRequest.ProjectType);
        //    param.Add("@StartDate", searchRequest.StartDate);
        //    param.Add("@EndDate", searchRequest.EndDate);
        //    param.Add("@UserRole", searchRequest.UserRole);
        //    param.Add("@NoOfRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);

        //    var results = await _db.LoadData<TenderDBModel, dynamic>("dbo.spTenders_Get", param);

        //    tenderGrid.Total = param.Get<int>("@NoOfRecords");
        //    tenderGrid.Data = results;

        //    return tenderGrid;
        //}

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
