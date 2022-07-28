using Dapper;
using DataAccess.DBAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class TenderData : ITenderData
    {
        private readonly ISqlDataAccess _db;

        public TenderData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<int> InsertNewTender(TenderDBModel tender)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("TenderId");
            dt.Columns.Add("ParentTenderTaskId");
            dt.Columns.Add("Name");
            dt.Columns.Add("Description");
            dt.Columns.Add("CreatedByUsername");
            dt.Columns.Add("CreatedDateTime");
            dt.Columns.Add("LastModifiedDateTime");

            foreach (var ttender in tender.TenderTasks)
            {
                dt.Rows.Add(ttender.Id,
                    ttender.TenderId,
                    ttender.ParentTenderTaskId,
                    ttender.Name,
                    ttender.Description,
                    ttender.CreatedByUsername,
                    ttender.CreatedDateTime.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    ttender.LastModifiedDateTime?.Date.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            var param = new DynamicParameters();
            param.Add("@TenderTasks", dt, DbType.Object);
            param.Add("@Id", tender.Id);
            param.Add("@Name", tender.Name);
            param.Add("@Description", tender.Description);
            param.Add("@TenderType", tender.TenderType);
            param.Add("@StartDateTime", tender.StartDateTime);
            param.Add("@EndDateTime", tender.EndDateTime);
            param.Add("@CustomerId", tender.CustomerId);
            param.Add("@Status", tender.Status);
            param.Add("@ProjectType", tender.ProjectType);
            param.Add("@Comment", tender.Comment);
            param.Add("@CreatedByUsername", tender.CreatedByUsername);
            param.Add("@CreatedDateTime", tender.CreatedDateTime);
            param.Add("@LastModifiedDateTime", tender.LastModifiedDateTime);

            var result = _db.SaveData<int, DynamicParameters>("dbo.spTender_Insert", param);

            return result;
        }
        

    }
}
