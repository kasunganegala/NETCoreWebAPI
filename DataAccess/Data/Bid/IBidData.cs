using DataAccess.Models;
using DataAccess.Models.Common;
using DataAccess.Models.Bid;
using DataAccess.Models.Tender;
using DataAccess.Models.Cost;

namespace DataAccess.Data
{
    public interface IBidData
    {
        Task<int> InsertNewBid(BidDBModel tender);
        public Task<BidDBModel?> GetBid(int id);
        public Task<List<BidTasksDBModel>?> GetBidTasks(int id);
        public Task<List<BidMaterialDBModel>?> GetBidMaterials(int id);
        public Task<List<BidLabourDBModel>?> GetBidLabours(int id);
        public Task<List<BidEquipmentDBModel>?> GetBidEquipments(int id);
        public Task<Grid<BidsSearchResponse>> GetBids(BidsSearchRequest searchRequest);
        public Task<Grid<BidsSearchResponse>> GetBidsExport(BidsSearchRequest searchRequest);
        //Task<int> SetTenderClose(int id);
        //Task<int> SetTenderHold(int id);
    }
}