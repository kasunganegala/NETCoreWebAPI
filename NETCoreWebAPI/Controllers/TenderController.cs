
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
        [Authorize(Roles = "ProjectManager")]
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
        [Authorize(Roles = "ProjectManager,Client,Contractor")]
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

        [HttpPost]
        [Route("search")]
        [Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> Tenders([FromBody] TenderSearchRequest searchRequest)
        {
            try
            {
                Grid<TenderSearchResponse> tenders = await _tenderData.GetTenders(searchRequest);

                foreach (TenderSearchResponse tender in tenders.Data)
                {
                    List<BidDBModel> bids = await _tenderData.GetTenderBids((int)tender.Id);

                    tender.BidsCount = bids.Count;
                }

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Tenders = tenders.Data,
                    Total = tenders.Total
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("export")]
        [Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> TendersExport([FromBody] TenderSearchRequest searchRequest)
        {
            try
            {
                Grid<TenderSearchResponse> tenders = await _tenderData.GetTendersExport(searchRequest);

                foreach (TenderSearchResponse tender in tenders.Data)
                {
                    List<BidDBModel> bids = await _tenderData.GetTenderBids((int)tender.Id);

                    tender.BidsCount = bids.Count;
                }

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Tenders = tenders.Data,
                    Total = tenders.Total
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("hold/{id}")]
        [Authorize(Roles = "ProjectManager")]
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
        [Authorize(Roles = "ProjectManager")]
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Tender id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/bids")]
        [Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> GetTenderBids(int id)
        {
            try
            {
                
                List<BidDBModel> bids = await _tenderData.GetTenderBids(id);

                if (bids == null || bids.Count == 0)
                {
                    return Ok(new
                    {
                        Errors = Array.Empty<Array>(),
                        Status = "Bids Not Found",
                        Bids = new { }
                    });
                }

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Bids = bids
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


    }
}
