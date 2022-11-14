
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
using DataAccess.Models.Bid;
using NETCoreWebAPI.BusinessRules.Bid;

namespace NETCoreWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class BidController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IBidData _bidData;
        
        private static MailMessage _global = new MailMessage();

        public BidController(
            IConfiguration configuration,
            IBidData bidData)
        {
            _configuration = configuration;
            _bidData = bidData;

        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Contractor")]
        public async Task<IActionResult> Create([FromBody] BidModel model)
        {
            try
            {
                List<Error> Errors = BidValidation.NewBidValidation(model);

                if (Errors.Count > 0)
                {
                    return Ok(new
                    {
                        Errors = Errors,
                        Status = "Validation Errors",
                        TenderId = 0
                    });
                }

                BidDBModel bid = BidBusinessRules.GenerateBidModel(model);

                int newBidId = await _bidData.InsertNewBid(bid);

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    TenderId = newBidId
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("{id}")]
        //[Authorize(Roles = "ProjectManager,Client,Contractor")]
        //public async Task<IActionResult> Tender(int id)
        //{
        //    try
        //    {
        //        TenderDBModel tender = await _tenderData.GetTender(id);
                
        //        if (tender == null)
        //        {
        //            return Ok(new
        //            {
        //                Errors = Array.Empty<Array>(),
        //                Status = "Tender Not Found",
        //                Tender = new { }
        //            });
        //        }

        //        List<TenderTasksDBModel> tenderTasks = await _tenderData.GetTenderTasks(id);
        //        tender.TenderTasks = tenderTasks;

        //        return Ok(new
        //        {
        //            Errors = Array.Empty<Array>(),
        //            Status = "Success",
        //            Tender = tender
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        //[HttpPost]
        //[Route("search")]
        //[Authorize(Roles = "ProjectManager,Client,Contractor")]
        //public async Task<IActionResult> Tenders([FromBody] TenderSearchRequest searchRequest)
        //{
        //    try
        //    {
        //        Grid<TenderDBModel> tenders = await _tenderData.GetTenders(searchRequest);

        //        return Ok(new
        //        {
        //            Errors = Array.Empty<Array>(),
        //            Status = "Success",
        //            Tenders = tenders.Data,
        //            Total = tenders.Total
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("hold/{id}")]
        //[Authorize(Roles = "ProjectManager")]
        //public async Task<IActionResult> Hold(int id)
        //{
        //    try
        //    {
        //        int tenderId = await _tenderData.SetTenderHold(id);

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
