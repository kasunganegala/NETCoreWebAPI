
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

        //[HttpPost]
        //[Route("create")]
        //[Authorize(Roles = "Contractor")]
        //public async Task<IActionResult> Create([FromBody] ProjectModel model)
        //{
        //    try
        //    {
        //        List<Error> Errors = ProjectValidation.NewProjectValidation(model);

        //        if (Errors.Count > 0)
        //        {
        //            return Ok(new
        //            {
        //                Errors = Errors,
        //                Status = "Validation Errors",
        //                ProjectId = 0
        //            });
        //        }

        //        ProjectDBModel project = ProjectBusinessRules.GenerateProjectModel(model);

        //        int newProjectId = await _projectData.InsertNewProject(project);

        //        return Ok(new
        //        {
        //            Errors = Array.Empty<Array>(),
        //            Status = "Success",
        //            ProjectId = newProjectId
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        [HttpGet]
        [Route("{id}")]
        //[Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> Project(int id)
        {
            try
            {
                ProjectDBModel project = await _projectData.GetProject(id);

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

        //[HttpGet]
        //[Route("{id}/approve")]
        //[Authorize(Roles = "ProjectManager")]
        //public async Task<IActionResult> Approve(int id)
        //{
        //    try
        //    {
        //        int? projectId = await _projectData.ApproveProject(id);

        //        if (projectId == null)
        //        {
        //            return Ok(new
        //            {
        //                Errors = Array.Empty<Array>(),
        //                Status = "Project Creation failed",
        //                Project = projectId
        //            });
        //        }

        //        return Ok(new
        //        {
        //            Errors = Array.Empty<Array>(),
        //            Status = "Success",
        //            Project = projectId
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("close/{id}")]
        //[Authorize(Roles = "ProjectManager")]
        //public async Task<IActionResult> Close(int id)
        //{
        //    try
        //    {
        //        int tenderId = await _tenderData.SetTenderClose(id);

        //        return Ok(new
        //        {
        //            Errors = Array.Empty<Array>(),
        //            Status = "Success",
        //            Tender = tenderId
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

    }
}
