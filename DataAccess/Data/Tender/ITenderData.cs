using DataAccess.Models;
using DataAccess.Models.Common;
using DataAccess.Models.Tender;

namespace DataAccess.Data
{
    public interface ITenderData
    {
        Task<int> InsertNewTender(TenderDBModel tender);
        public Task<TenderDBModel?> GetTender(int id);
        public Task<List<BidDBModel>?> GetTenderBids(int id);
        public Task<List<TenderTasksDBModel>?> GetTenderTasks(int id);
        public Task<Grid<TenderSearchResponse>> GetTenders(TenderSearchRequest searchRequest);
        public Task<Grid<TenderSearchResponse>> GetTendersExport(TenderSearchRequest searchRequest);
        Task<int> SetTenderClose(int id);
        Task<int> SetTenderHold(int id);
    }
}