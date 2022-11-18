using DataAccess.Models;
using DataAccess.Models.Common;
using DataAccess.Models.Bid;

namespace DataAccess.Data
{
    public interface IBidData
    {
        Task<int> InsertNewBid(BidDBModel tender);
        public Task<BidDBModel?> GetBid(int id);
        public Task<List<BidTasksDBModel>?> GetBidTasks(int id);
        //public Task<Grid<TenderDBModel>> GetTenders(TenderSearchRequest searchRequest);
        //Task<int> SetTenderClose(int id);
        //Task<int> SetTenderHold(int id);
    }
}