
//using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using System.Security.Cryptography;
using DataAccess.Models.Tender;
using DataAccess.Models.Authentication;
using DataAccess.Models;
using NETCoreWebAPI.Validations;
using DataAccess.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DataAccess.Models.Project;
using NETCoreWebAPI.BusinessRules.Project;

namespace NETCoreWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class ProjectController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IProjectData _projectData;
        
        private static MailMessage _global = new MailMessage();

        public ProjectController(
            IConfiguration configuration,
            IProjectData projectData)
        {
            _configuration = configuration;
            _projectData = projectData;

        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> Project(int id)
        {
            try
            {
                ProjectDBModel project = await _projectData.GetProject(id);
                //dynamic projectProgress = await _projectData.GetProjectProgress(id);

                if (project == null)
                {
                    return Ok(new
                    {
                        Errors = Array.Empty<Array>(),
                        Status = "Project Not Found",
                        Project = new { }
                    });
                }

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Project = project
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("search")]
        //[Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> Projects([FromBody] ProjectsSearchRequest searchRequest)
        {
            try
            {
                Grid<ProjectsSearchResponse> projects = await _projectData.GetProjects(searchRequest);

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Projects = projects.Data,
                    Total = projects.Total
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("update/tasks")]
        //[Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> UpdateTasks([FromBody] ProjectsUpdateTasksRequest updateRequest)
        {
            try
            {
                List<ProjectTasksDBModel> tasks = await _projectData.UpdateTasks(updateRequest);

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    ProjectTasks = tasks,
                    
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

		[HttpPost]
		[Route("update/labours")]
		//[Authorize(Roles = "ProjectManager,Client,Contractor")]
		public async Task<IActionResult> UpdateLabours([FromBody] ProjectsUpdateLaboursRequest updateRequest)
		{
			try
			{
				List<ProjectLabourDBModel> tasks = await _projectData.UpdateLabours(updateRequest);

				return Ok(new
				{
					Errors = Array.Empty<Array>(),
					Status = "Success",
					ProjectLabours = tasks,

				});
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPost]
		[Route("update/equipments")]
		//[Authorize(Roles = "ProjectManager,Client,Contractor")]
		public async Task<IActionResult> UpdateEquipments([FromBody] ProjectsUpdateEquipmentsRequest updateRequest)
		{
			try
			{
				List<ProjectEquipmentDBModel> tasks = await _projectData.UpdateEquipments(updateRequest);

				return Ok(new
				{
					Errors = Array.Empty<Array>(),
					Status = "Success",
					ProjectEquipments = tasks,

				});
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		[HttpPost]
		[Route("update/materials")]
		//[Authorize(Roles = "ProjectManager,Client,Contractor")]
		public async Task<IActionResult> UpdateMaterials([FromBody] ProjectsUpdateMaterialsRequest updateRequest)
		{
			try
			{
				List<ProjectMaterialDBModel> tasks = await _projectData.UpdateMaterials(updateRequest);

				return Ok(new
				{
					Errors = Array.Empty<Array>(),
					Status = "Success",
					ProjectMaterials = tasks,

				});
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPost]
        [Route("Export")]
        [Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> ProjectsExport([FromBody] ProjectsSearchRequest searchRequest)
        {
            try
            {
                Grid<ProjectsSearchResponse> projects = await _projectData.GetProjectsExport(searchRequest);

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Projects = projects.Data,
                    Total = projects.Total
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}/start")]
        [Authorize(Roles = "ProjectManager,Contractor")]
        public async Task<IActionResult> ProjectStart(int id)
        {
            try
            {
                int? projectId = await _projectData.ProjectStart(id);

                if (projectId == null)
                {
                    return Ok(new
                    {
                        Errors = Array.Empty<Array>(),
                        Status = "Cannot start project",
                    });
                }

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success"
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

		[HttpGet]
		[Route("{id}/complete")]
		[Authorize(Roles = "ProjectManager,Contractor")]
		public async Task<IActionResult> ProjectComplete(int id)
		{
			try
			{
				int? projectId = await _projectData.ProjectComplete(id);

				if (projectId == null)
				{
					return Ok(new
					{
						Errors = Array.Empty<Array>(),
						Status = "Cannot start project",
					});
				}

				return Ok(new
				{
					Errors = Array.Empty<Array>(),
					Status = "Success"
				});
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}



		[HttpGet]
        [Route("{pid}/task/{tid}")]
       // [Authorize(Roles = "ProjectManager,Contractor")]
        public async Task<IActionResult> ProjectTasks(int pid, int tid)
        {
            try
            {
                ProjectTaskResponse projectTask = await _projectData.ProjectTask(pid, tid);

                if (projectTask == null)
                {
                    return Ok(new
                    {
                        Errors = Array.Empty<Array>(),
                        Status = "Cannot find project task",
                        ProjectTasks = new { }
                    });
                }

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    ProjectTasks = projectTask
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{pid}/task/{tid}/status/{status}")]
        [Authorize(Roles = "ProjectManager,Contractor")]
        public async Task<IActionResult> TaskSetStatus(int pid, int tid, string status)
        {
            try
            {
                int? projectId = await _projectData.TaskSetStatus(pid, tid, status);

                if (projectId == null)
                {
                    return Ok(new
                    {
                        Errors = Array.Empty<Array>(),
                        Status = "Cannot set status",
                    });
                }

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success"
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("task/submit/materials")]
        [Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> SubmitMeterials([FromBody] ProjectTaskMaterialUsageRequest searchRequest)
        {
            try
            {
                var status = await _projectData.SubmitMeterials(searchRequest);

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Success = status
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

		[HttpPost]
		[Route("task/submit/equipments")]
		[Authorize(Roles = "ProjectManager,Client,Contractor")]
		public async Task<IActionResult> SubmitEquipments([FromBody] ProjectTaskEquipmentUsageRequest searchRequest)
		{
			try
			{
				var status = await _projectData.SubmitEquipments(searchRequest);

				return Ok(new
				{
					Errors = Array.Empty<Array>(),
					Status = "Success",
					Success = status
				});
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPost]
		[Route("task/submit/labours")]
		[Authorize(Roles = "ProjectManager,Client,Contractor")]
		public async Task<IActionResult> SubmitLabours([FromBody] ProjectTaskLabourUsageRequest searchRequest)
		{
			try
			{
				var status = await _projectData.SubmitLabours(searchRequest);

				return Ok(new
				{
					Errors = Array.Empty<Array>(),
					Status = "Success",
					Success = status
				});
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


		[HttpPost]
		[Route("task/submit/worklog")]
		[Authorize(Roles = "ProjectManager,Client,Contractor")]
		public async Task<IActionResult> SubmitWorklog([FromBody] ProjectTaskWorklogRequest request)
		{
			try
			{
				var status = await _projectData.SubmitWorklog(request);

				return Ok(new
				{
					Errors = Array.Empty<Array>(),
					Status = "Success",
					Success = status
				});
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


	}
}
