
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
using NETCoreWebAPI.BusinessRules.Tender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace NETCoreWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class TenderController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ITenderData _tenderData;
        
        private static MailMessage _global = new MailMessage();

        public TenderController(
            IConfiguration configuration,
            ITenderData tenderData)
        {
            _configuration = configuration;
            _tenderData = tenderData;

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] TenderModel model)
        {
            try
            {
                List<Error> Errors = TenderValidation.NewTenderValidation(model);

                if (Errors.Count > 0)
                {
                    return Ok(new
                    {
                        Errors = Errors,
                        Status = "Validation Errors",
                        TenderId = 0
                    });
                }

                TenderDBModel tender = TenderBusinessRules.GenerateTenderModel(model);

                int newTenderId = await _tenderData.InsertNewTender(tender);

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    TenderId = newTenderId
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "ProjectManager,Client")]
        public async Task<IActionResult> Tender(int id)
        {
            try
            {
                TenderDBModel tender = await _tenderData.GetTender(id);
                
                if (tender == null)
                {
                    return Ok(new
                    {
                        Errors = Array.Empty<Array>(),
                        Status = "Tender Not Found",
                        Tender = new { }
                    });
                }

                List<TenderTasksDBModel> tenderTasks = await _tenderData.GetTenderTasks(id);
                tender.TenderTasks = tenderTasks;

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Tender = tender
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "ProjectManager,Client")]
        public async Task<IActionResult> Tenders()
        {
            try
            {
                List<TenderDBModel> tenders = await _tenderData.GetTenders();

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Tenders = tenders
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("hold/{id}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Hold(int id)
        {
            try
            {
                int tenderId = await _tenderData.SetTenderHold(id);

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Tender = tenderId
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("close/{id}")]
        public async Task<IActionResult> Close(int id)
        {
            try
            {
                int tenderId = await _tenderData.SetTenderClose(id);

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Tender = tenderId
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
