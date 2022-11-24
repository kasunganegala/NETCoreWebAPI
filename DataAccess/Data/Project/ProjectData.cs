﻿using Dapper;
using DataAccess.DBAccess;
using DataAccess.Models;
using DataAccess.Models.Project;
using DataAccess.Models.Common;
using DataAccess.Models.Cost;
using DataAccess.Models.Tender;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ProjectData : IProjectData
    {
        private readonly ISqlDataAccess _db;

        public ProjectData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<ProjectDBModel?> GetProject(int id)
        {
            var results = await _db.LoadData<ProjectDBModel, dynamic>(
                "dbo.spProject_Get",
                new { Id = id });

            var project =  results.FirstOrDefault();

            return project;
        }

        public async Task<List<ProjectTasksDBModel>?> GetProjectTasks(int id)
        {
            var results = await _db.LoadData<ProjectTasksDBModel, dynamic>(
                "dbo.spProjectTasks_Get",
                new { Id = id });

            var projectTaskData = results.ToList();

            return projectTaskData;
        }

        public async Task<List<ProjectMaterialDBModel>?> GetProjectMaterials(int id)
        {
            var results = await _db.LoadData<ProjectMaterialDBModel, dynamic>(
                "dbo.spProjectMaterials_Get",
                new { Id = id });

            return results.ToList();
        }

        public async Task<List<ProjectLabourDBModel>?> GetProjectLabours(int id)
        {
            var results = await _db.LoadData<ProjectLabourDBModel, dynamic>(
                "dbo.spProjectLabours_Get",
                new { Id = id });

            return results.ToList();
        }

        public async Task<List<ProjectEquipmentDBModel>?> GetProjectEquipments(int id)
        {
            var results = await _db.LoadData<ProjectEquipmentDBModel, dynamic>(
                "dbo.spProjectEquipments_Get",
                new { Id = id });

            return results.ToList();            
        }

        public async Task<Grid<ProjectsSearchResponse>> GetProjects(ProjectsSearchRequest searchRequest)
        {
            Grid<ProjectsSearchResponse> projectGrid = new Grid<ProjectsSearchResponse>();

            var param = new DynamicParameters();
            param.Add("@Limit", searchRequest.Limit);
            param.Add("@Offset", searchRequest.Offset);
            param.Add("@Customer", searchRequest.Customer);
            param.Add("@TenderType", searchRequest.TenderType);
            param.Add("@ProjectType", searchRequest.ProjectType);
            param.Add("@StartDate", searchRequest.StartDate);
            param.Add("@EndDate", searchRequest.EndDate);
            param.Add("@UserRole", searchRequest.UserRole);
            param.Add("@Contractor", searchRequest.Contractor);
            param.Add("@Status", searchRequest.Status);
            param.Add("@SubmittedDate", searchRequest.SubmittedDate);
            param.Add("@NoOfRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var results = await _db.LoadData<ProjectsSearchResponse, dynamic>("dbo.spProjects_Get", param);

            projectGrid.Total = param.Get<int>("@NoOfRecords");
            projectGrid.Data = results;

            return projectGrid;
        }

        public async Task<Grid<ProjectsSearchResponse>> GetProjectsExport(ProjectsSearchRequest searchRequest)
        {
            Grid<ProjectsSearchResponse> projectGrid = new Grid<ProjectsSearchResponse>();

            var param = new DynamicParameters();
            param.Add("@Customer", searchRequest.Customer);
            param.Add("@TenderType", searchRequest.TenderType);
            param.Add("@ProjectType", searchRequest.ProjectType);
            param.Add("@StartDate", searchRequest.StartDate);
            param.Add("@EndDate", searchRequest.EndDate);
            param.Add("@UserRole", searchRequest.UserRole);
            param.Add("@Contractor", searchRequest.Contractor);
            param.Add("@Status", searchRequest.Status);
            param.Add("@SubmittedDate", searchRequest.SubmittedDate);
            param.Add("@NoOfRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var results = await _db.LoadData<ProjectsSearchResponse, dynamic>("dbo.spProjects_Get", param);

            projectGrid.Total = param.Get<int>("@NoOfRecords");
            projectGrid.Data = results;

            return projectGrid;
        }

        public Task<int> ApproveProject(int id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id);

            return _db.SaveData<int, DynamicParameters>("dbo.spProjectApproveProject_Get", param);
        }
        public Task<int> RejectProject(int id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id);

            return _db.SaveData<int, DynamicParameters>("dbo.spProjectRejectProject_Get", param);
        }
    }
}