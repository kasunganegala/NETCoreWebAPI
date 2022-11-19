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
    public class CostData : ICostData
    {
        private readonly ISqlDataAccess _db;

        public CostData(ISqlDataAccess db)
        {
            _db = db;
        }

        //public async Task<BidDBModel?> GetBid(int id)
        //{
        //    var results = await _db.LoadData<BidDBModel, dynamic>(
        //        "dbo.spBid_Get",
        //        new { Id = id });

        //    return results.FirstOrDefault();
        //}

        public async Task<List<EquipmentResponse>?> GetEquipmentList()
        {
            var results = await _db.LoadData<EquipmentResponse>("dbo.spCostEquipments_Get");

            return results.ToList();
        }

        public async Task<List<LabourResponse>?> GetLabourList()
        {
            var results = await _db.LoadData<LabourResponse>("dbo.spCostLabour_Get");

            return results.ToList();
        }

        public async Task<List<MaterialsResponse>?> GetMaterialsList()
        {
            var results = await _db.LoadData<MaterialsResponse>("dbo.spCostMaterial_Get");

            return results.ToList();

        }

        public async Task<List<UOMResponse>?> GetUOMList()
        {
            var results = await _db.LoadData<UOMResponse>("dbo.spCostUOM_Get");

            return results.ToList();
        }
    }
}
