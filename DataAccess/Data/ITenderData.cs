using DataAccess.Models;

namespace DataAccess.Data
{
    public interface ITenderData
    {
        Task InsertNewTender(TenderDBModel tender);
    }
}