using DataAccess.Models;

namespace DataAccess.Data
{
    public interface ITenderData
    {
        Task<int> InsertNewTender(TenderDBModel tender);
    }
}