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
using System.Xml.Linq;

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

            ProjectDBModel project =  results.FirstOrDefault();

            if (project != null) {
                project.ProjectTasks = await GetProjectTasks(id);
                project.Materials = await GetProjectMaterials(id);
                project.Labours = await GetProjectLabours(id);
                project.Equipments = await GetProjectEquipments(id);
            }
            
            return project;
        }

		
		public async Task<ProjectDBModel?> GetProjectProgress(int id)
		{
			var results = await _db.LoadData<ProjectDBModel, dynamic>(
				"dbo.spProject_Get",
				new { Id = id });

			ProjectDBModel project = results.FirstOrDefault();

			if (project != null)
			{
				project.ProjectTasks = await GetProjectTasks(id);
				project.Materials = await GetProjectMaterials(id);
				project.Labours = await GetProjectLabours(id);
				project.Equipments = await GetProjectEquipments(id);
			}

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

        public async Task<List<ProjectTasksDBModel>?> UpdateTasks(ProjectsUpdateTasksRequest updateRequest)
        {
            DataTable dt = GenerateProjectTasksDataTable(updateRequest.ProjectTasks);
            
            var param = new DynamicParameters();
            param.Add("@ProjectTasks", dt, DbType.Object);
            param.Add("@ProjectId", updateRequest.ProjectId);

            var isProcessed =  _db.SaveData<bool, DynamicParameters>("dbo.spProjectTasks_Update", param);

            if ((bool)isProcessed.Result) {
                return await GetProjectTasks((int)updateRequest.ProjectId);
            }

            return null;
        }

		public async Task<List<ProjectLabourDBModel>?> UpdateLabours(ProjectsUpdateLaboursRequest updateRequest)
        {
            DataTable dt = GenerateProjectLaboursDataTable(updateRequest.ProjectLabours);
            
            var param = new DynamicParameters();
            param.Add("@ProjectLabours", dt, DbType.Object);
            param.Add("@ProjectId", updateRequest.ProjectId);

            var isProcessed =  _db.SaveData<bool, DynamicParameters>("dbo.spProjectLabours_Update", param);

            if ((bool)isProcessed.Result) {
                return await GetProjectLabours((int)updateRequest.ProjectId);
            }

            return null;
        }

		public async Task<List<ProjectEquipmentDBModel>?> UpdateEquipments(ProjectsUpdateEquipmentsRequest updateRequest)
		{
			DataTable dt = GenerateProjectEquipmentsDataTable(updateRequest.ProjectEquipments);

			var param = new DynamicParameters();
			param.Add("@ProjectEquipments", dt, DbType.Object);
			param.Add("@ProjectId", updateRequest.ProjectId);

			var isProcessed = _db.SaveData<bool, DynamicParameters>("dbo.spProjectEquipments_Update", param);

			if ((bool)isProcessed.Result)
			{
				return await GetProjectEquipments((int)updateRequest.ProjectId);
			}

			return null;
		}

		public async Task<List<ProjectMaterialDBModel>?> UpdateMaterials(ProjectsUpdateMaterialsRequest updateRequest)
		{
			DataTable dt = GenerateProjectMaterialsDataTable(updateRequest.ProjectMaterials);

			var param = new DynamicParameters();
			param.Add("@ProjectMaterials", dt, DbType.Object);
			param.Add("@ProjectId", updateRequest.ProjectId);

			var isProcessed = _db.SaveData<bool, DynamicParameters>("dbo.spProjectMaterials_Update", param);

			if ((bool)isProcessed.Result)
			{
				return await GetProjectMaterials((int)updateRequest.ProjectId);
			}

			return null;
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

        public Task<int> ProjectStart(int id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id);

            return _db.SaveData<int, DynamicParameters>("dbo.spProjectStart_Get", param);
        }

		public Task<int> ProjectComplete(int id)
		{
			var param = new DynamicParameters();
			param.Add("@Id", id);

			return _db.SaveData<int, DynamicParameters>("dbo.spProjectComplete_Get", param);
		}


		public Task<int> RejectProject(int id)
        {
            var param = new DynamicParameters();
            param.Add("@Id", id);

            return _db.SaveData<int, DynamicParameters>("dbo.spProjectRejectProject_Get", param);
        }

        public Task<int> TaskSetStatus(int pid, int tid, string status)
        {
            var param = new DynamicParameters();
            param.Add("@ProjectId", pid);
            param.Add("@TaskId", tid);
            param.Add("@Status", status);

            return _db.SaveData<int, DynamicParameters>("dbo.spProjectTaskSetStatus_Get", param);
        }

        public async Task<ProjectTaskResponse> ProjectTask(int projectId, int taskId)
        {
            var param = new DynamicParameters();
            param.Add("@ProjectId", projectId);
            param.Add("@TaskId", taskId);

            var results = await _db.LoadData<ProjectTaskResponse, DynamicParameters>("dbo.spProjectTask_Get", param);

            ProjectTaskResponse project = results.FirstOrDefault();

            if (project != null)
            {
                project.Materials = await GetProjectTaskMaterials(projectId, taskId);
                project.Labours = await GetProjectTaskLabours(projectId, taskId);
                project.Equipments = await GetProjectTaskEquipments(projectId, taskId);
                project.WorkLogs = await GetProjectTaskWorkLogs(projectId, taskId);
            }

            return project;
        }

        public async Task<List<ProjectTaskCostLineDBModel>?> GetProjectTaskMaterials(int projectId, int taskId)
        {
            var results = await _db.LoadData<ProjectTaskCostLineDBModel, dynamic>(
                "dbo.spProjectTaskMaterials_Get",
                new { ProjectId = projectId, TaskId = taskId });

            return results.ToList();
        }

        public async Task<List<ProjectTaskCostLineDBModel>?> GetProjectTaskLabours(int projectId, int taskId)
        {
            var results = await _db.LoadData<ProjectTaskCostLineDBModel, dynamic>(
               "dbo.spProjectTaskLabours_Get",
               new { ProjectId = projectId, TaskId = taskId });

            return results.ToList();
        }

        public async Task<List<ProjectTaskCostLineDBModel>?> GetProjectTaskEquipments(int projectId, int taskId)
        {
            var results = await _db.LoadData<ProjectTaskCostLineDBModel, dynamic>(
                "dbo.spProjectTaskEquipments_Get",
                new { ProjectId = projectId, TaskId = taskId });

            return results.ToList();
        }

		public async Task<List<dynamic>?> GetProjectTaskWorkLogs(int projectId, int taskId)
		{
			var results = await _db.LoadData<dynamic, dynamic>(
				"dbo.spProjectTaskWorkLogs_Get",
				new { ProjectId = projectId, TaskId = taskId });

			return results.ToList();
		}

		public Task<int> SubmitMeterials(ProjectTaskMaterialUsageRequest searchRequest)
        {
            DataTable dt = GenerateProjectTaskMaterialsDataTable(searchRequest.Materials, (int)searchRequest.ProjectId, (int)searchRequest.TaskId);

            var param = new DynamicParameters();
            param.Add("@Materials", dt, DbType.Object);
            param.Add("@ProjectId", searchRequest.ProjectId);
            param.Add("@TaskId", searchRequest.TaskId);

            return _db.SaveData<int, DynamicParameters>("dbo.spProjectTaskMaterialUsage_Insert", param);
        }

		public Task<int> SubmitEquipments(ProjectTaskEquipmentUsageRequest searchRequest)
		{
			DataTable dt = GenerateProjectTaskEquipmentsDataTable(searchRequest.Equipments, (int)searchRequest.ProjectId, (int)searchRequest.TaskId);;

			var param = new DynamicParameters();
			param.Add("@Equipments", dt, DbType.Object);
			param.Add("@ProjectId", searchRequest.ProjectId);
			param.Add("@TaskId", searchRequest.TaskId);

			return _db.SaveData<int, DynamicParameters>("dbo.spProjectTaskEquipmentUsage_Insert", param);
		}

		public Task<int> SubmitLabours(ProjectTaskLabourUsageRequest searchRequest)
		{
            DataTable dt = GenerateProjectTaskLaboursDataTable(searchRequest.Labours, (int)searchRequest.ProjectId, (int)searchRequest.TaskId);

			var param = new DynamicParameters();
			param.Add("@Labours", dt, DbType.Object);
			param.Add("@ProjectId", searchRequest.ProjectId);
			param.Add("@TaskId", searchRequest.TaskId);

			return _db.SaveData<int, DynamicParameters>("dbo.spProjectTaskLabourUsage_Insert", param);
		}

		public Task<int> SubmitWorklog(ProjectTaskWorklogRequest request)
		{
			DataTable laborlog = GenerateProjectTaskLaboursDataTable(request.Labours, (int)request.ProjectId, (int)request.TaskId);
			DataTable equipmentlog = GenerateProjectTaskEquipmentsDataTable(request.Equipments, (int)request.ProjectId, (int)request.TaskId); ;
			DataTable materiallog = GenerateProjectTaskMaterialsDataTable(request.Materials, (int)request.ProjectId, (int)request.TaskId);

			var param = new DynamicParameters();
            param.Add("@Materials", materiallog, DbType.Object);
			param.Add("@Equipments", equipmentlog, DbType.Object);
			param.Add("@Labours", laborlog, DbType.Object);
			param.Add("@ProjectId", request.ProjectId);
			param.Add("@TaskId", request.TaskId);
			param.Add("@LogDate", request.LogDate);
			param.Add("@Comment", request.Comment);
			param.Add("@Effort", request.Effort);

			return _db.SaveData<int, DynamicParameters>("dbo.spProjectTaskWorklog_Insert", param);
		}

		private DataTable GenerateProjectTasksDataTable(List<ProjectTasksDBModel>? List)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("ProjectId");
            dt.Columns.Add("TaskId");
            dt.Columns.Add("ParentTaskId");
            dt.Columns.Add("Task");
            dt.Columns.Add("CreatedByUsername");
            dt.Columns.Add("StartDateTime");
            dt.Columns.Add("EndDateTime");
            dt.Columns.Add("CreatedDateTime");
            dt.Columns.Add("LastModifiedDateTime");
            dt.Columns.Add("Status");

            foreach (ProjectTasksDBModel item in List)
            {
                dt.Rows.Add(
                    item.Id,
                    item.ProjectId,
                    item.TaskId,
                    item.ParentTaskId,
                    item.Task,
                    item.CreatedByUsername,
                    item.StartDate?.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    item.EndDate?.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    item.CreatedDateTime.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    item.Status);
            }

            return dt;
        }
		private DataTable GenerateProjectLaboursDataTable(List<ProjectLabourDBModel>? List)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Id");
			dt.Columns.Add("ProjectId");
			dt.Columns.Add("LabourId");
			dt.Columns.Add("Name");
			dt.Columns.Add("UOMId");
			dt.Columns.Add("EstimatedUnitCost");
			dt.Columns.Add("EstimatedQuantity");
            dt.Columns.Add("EstimatedTotalCost");
			dt.Columns.Add("UnitCost");
			dt.Columns.Add("Quantity");
            dt.Columns.Add("TotalCost");
            dt.Columns.Add("Profit");
			dt.Columns.Add("CreatedByUsername");
            dt.Columns.Add("CreatedDateTime");
			dt.Columns.Add("LastModifiedDateTime");
			dt.Columns.Add("IsDeleted");

			foreach (ProjectLabourDBModel item in List)
			{
				dt.Rows.Add(
					item.Id,
					item.ProjectId,
					item.LabourId,
					item.Name,
					item.UOMId,
					item.UnitCost,
					item.Quantity,
					item.TotalCost,
					item.UnitCost,
                    0,
                    0,
                    item.Profit,
                    item.CreatedByUsername,
					DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
				    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    0);
			}

			return dt;
		}
		private DataTable GenerateProjectMaterialsDataTable(List<ProjectMaterialDBModel>? List)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Id");
			dt.Columns.Add("ProjectId");
			dt.Columns.Add("MaterialId");
			dt.Columns.Add("Name");
			dt.Columns.Add("UOMId");
			dt.Columns.Add("EstimatedUnitCost");
			dt.Columns.Add("EstimatedQuantity");
			dt.Columns.Add("EstimatedTotalCost");
			dt.Columns.Add("UnitCost");
			dt.Columns.Add("Quantity");
			dt.Columns.Add("TotalCost");
			dt.Columns.Add("Profit");
			dt.Columns.Add("CreatedByUsername");
			dt.Columns.Add("CreatedDateTime");
			dt.Columns.Add("LastModifiedDateTime");
			dt.Columns.Add("IsDeleted");

			foreach (ProjectMaterialDBModel item in List)
			{
				dt.Rows.Add(
					item.Id,
					item.ProjectId,
					item.MaterialId,
					item.Name,
					item.UOMId,
					item.UnitCost,
					item.Quantity,
					item.TotalCost,
					item.UnitCost,
					0,
					0,
					item.Profit,
					item.CreatedByUsername,
					DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
					DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
					0);
			}

			return dt;
		}
		private DataTable GenerateProjectEquipmentsDataTable(List<ProjectEquipmentDBModel>? List)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Id");
			dt.Columns.Add("ProjectId");
			dt.Columns.Add("EquipmentId");
			dt.Columns.Add("Name");
			dt.Columns.Add("UOMId");
			dt.Columns.Add("EstimatedUnitCost");
			dt.Columns.Add("EstimatedQuantity");
			dt.Columns.Add("EstimatedTotalCost");
			dt.Columns.Add("UnitCost");
			dt.Columns.Add("Quantity");
			dt.Columns.Add("TotalCost");
			dt.Columns.Add("Profit");
			dt.Columns.Add("CreatedByUsername");
			dt.Columns.Add("CreatedDateTime");
			dt.Columns.Add("LastModifiedDateTime");
			dt.Columns.Add("IsDeleted");

			foreach (ProjectEquipmentDBModel item in List)
			{
				dt.Rows.Add(
					item.Id,
					item.ProjectId,
					item.EquipmentId,
					item.Name,
					item.UOMId,
					item.UnitCost,
					item.Quantity,
					item.TotalCost,
					item.UnitCost,
					0,
					0,
					item.Profit,
					item.CreatedByUsername,
					DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
					DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
					0);
			}

			return dt;
		}
        private DataTable GenerateProjectTaskMaterialsDataTable(List<ProjectTaskMaterialUsageDBModel>? List, int projectId, int taskId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("MaterialId");
            dt.Columns.Add("Name");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("UOM");
            dt.Columns.Add("UOMId");
            dt.Columns.Add("ProjectId");
            dt.Columns.Add("TaskId");

            foreach (ProjectTaskMaterialUsageDBModel item in List)
            {
                dt.Rows.Add(
                    item.Id,
                    item.MaterialId,
                    item.Name,
                    item.Quantity,
                    item.UOM,
                    item.UOMId,
                    projectId,
                    taskId);
            }

            return dt;
        }
        private DataTable GenerateProjectTaskEquipmentsDataTable(List<ProjectTaskEquipmentUsageDBModel>? List, int projectId, int taskId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("EquipmentId");
            dt.Columns.Add("Name");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("UOM");
            dt.Columns.Add("UOMId");
            dt.Columns.Add("ProjectId");
            dt.Columns.Add("TaskId");

            foreach (ProjectTaskEquipmentUsageDBModel item in List)
            {
                dt.Rows.Add(
                    item.Id,
                    item.EquipmentId,
                    item.Name,
                    item.Quantity,
                    item.UOM,
                    item.UOMId,
                    projectId,
                    taskId);
            }

            return dt;
        }
        private DataTable GenerateProjectTaskLaboursDataTable(List<ProjectTaskLabourUsageDBModel>? List, int projectId, int taskId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("LabourId");
            dt.Columns.Add("Name");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("UOM");
            dt.Columns.Add("UOMId");
            dt.Columns.Add("ProjectId");
            dt.Columns.Add("TaskId");

            foreach (ProjectTaskLabourUsageDBModel item in List)
            {
                dt.Rows.Add(
                    item.Id,
                    item.LabourId,
                    item.Name,
                    item.Quantity,
                    item.UOM,
                    item.UOMId,
                    projectId,
                    taskId);
            }

            return dt;
        }
    }
}
