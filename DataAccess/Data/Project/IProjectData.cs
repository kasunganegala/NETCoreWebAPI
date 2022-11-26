using DataAccess.Models;
using DataAccess.Models.Common;
using DataAccess.Models.Project;
using DataAccess.Models.Tender;
using DataAccess.Models.Cost;

namespace DataAccess.Data
{
    public interface IProjectData
    {
        public Task<ProjectDBModel?> GetProject(int id);
        public Task<List<ProjectTasksDBModel>?> GetProjectTasks(int id);
        public Task<List<ProjectMaterialDBModel>?> GetProjectMaterials(int id);
        public Task<List<ProjectLabourDBModel>?> GetProjectLabours(int id);
        public Task<List<ProjectEquipmentDBModel>?> GetProjectEquipments(int id);
        public Task<Grid<ProjectsSearchResponse>> GetProjects(ProjectsSearchRequest searchRequest);
        public Task<Grid<ProjectsSearchResponse>> GetProjectsExport(ProjectsSearchRequest searchRequest);
        public Task<List<ProjectTasksDBModel>?> UpdateTasks(ProjectsUpdateTasksRequest updateRequest);
        public Task<List<ProjectLabourDBModel>?> UpdateLabours(ProjectsUpdateLaboursRequest updateRequest);
        public Task<List<ProjectEquipmentDBModel>?> UpdateEquipments(ProjectsUpdateEquipmentsRequest updateRequest);
        public Task<List<ProjectMaterialDBModel>?> UpdateMaterials(ProjectsUpdateMaterialsRequest updateRequest);
        public Task<int> ProjectStart(int id);
        public Task<ProjectTaskResponse?> ProjectTask(int projectId, int taskId);
        public Task<int> TaskSetStatus(int pid, int tid, string status);
        public Task<int> SubmitMeterials(ProjectTaskMaterialUsageRequest searchRequest);

    }
}