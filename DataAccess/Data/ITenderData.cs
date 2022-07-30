using DataAccess.Models;

namespace DataAccess.Data
{
    public interface ITenderData
    {
        Task<int> InsertNewTender(TenderDBModel tender);
        public Task<TenderDBModel?> GetTender(int id);
        public Task<List<TenderTasksDBModel>?> GetTenderTasks(int id);
        public Task<List<TenderDBModel>?> GetTenders();
    }
}