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
       
    }
}