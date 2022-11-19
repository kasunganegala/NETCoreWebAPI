using DataAccess.Models;
using DataAccess.Models.Common;
using DataAccess.Models.Cost;

namespace DataAccess.Data
{
    public interface ICostData
    {
        public Task<List<UOMResponse>?> GetUOMList();
        public Task<List<MaterialsResponse>?> GetMaterialsList();
        public Task<List<EquipmentResponse>?> GetEquipmentList();
        public Task<List<LabourResponse>?> GetLabourList();
    }
}