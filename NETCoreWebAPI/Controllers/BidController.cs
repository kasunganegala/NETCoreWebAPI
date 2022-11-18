
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
                        BidId = 0
                    });
                }

                BidDBModel bid = BidBusinessRules.GenerateBidModel(model);

                int newBidId = await _bidData.InsertNewBid(bid);

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    BidId = newBidId
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
        public async Task<IActionResult> Bid(int id)
        {
            try
            {
                BidDBModel bid = await _bidData.GetBid(id);

                if (bid == null)
                {
                    return Ok(new
                    {
                        Errors = Array.Empty<Array>(),
                        Status = "Bid Not Found",
                        Bid = new { }
                    });
                }

                List<BidTasksDBModel> bidTasks = await _bidData.GetBidTasks(id);
                bid.BidTasks = bidTasks;

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Bid = bid
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
        public async Task<IActionResult> Bids([FromBody] BidsSearchRequest searchRequest)
        {
            try
            {
                Grid<BidsSearchResponse> bids = await _bidData.GetBids(searchRequest);

                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Bids = bids.Data,
                    Total = bids.Total
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

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
