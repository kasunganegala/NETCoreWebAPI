
namespace DataAccess.DBAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task<IEnumerable<T>> LoadData<T>(string storedProcedure, string connectionId = "Default");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default");
        Task<T> SaveData<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
    }
}