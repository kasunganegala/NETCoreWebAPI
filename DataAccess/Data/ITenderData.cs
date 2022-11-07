using DataAccess.Models;
using DataAccess.Models.Common;

namespace DataAccess.Data
{
    public interface ITenderData
    {
        Task<int> InsertNewTender(TenderDBModel tender);
        public Task<TenderDBModel?> GetTender(int id);
        public Task<List<TenderTasksDBModel>?> GetTenderTasks(int id);
        public Task<Grid<TenderDBModel>> GetTenders(int limit, int offset);
        Task<int> SetTenderClose(int id);
        Task<int> SetTenderHold(int id);
    }
}