
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
using DataAccess.Models.Cost;

namespace NETCoreWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class CostController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ICostData _costData;
        
        private static MailMessage _global = new MailMessage();

        public CostController(
            IConfiguration configuration,
            ICostData costData)
        {
            _configuration = configuration;
            _costData = costData;

        }

        //[HttpGet]
        //[Route("uom")]
        //[Authorize(Roles = "ProjectManager,Client,Contractor")]
        //public async Task<IActionResult> GetUOMList()
        //{
        //    try
        //    {
        //        BidDBModel bid = await _costData.GetBid(0);

        //        if (bid == null)
        //        {
        //            return Ok(new
        //            {
        //                Errors = Array.Empty<Array>(),
        //                Status = "Bid Not Found",
        //                Bid = new { }
        //            });
        //        }

        //        List<BidTasksDBModel> bidTasks = await _costData.GetBidTasks(id);
        //        bid.BidTasks = bidTasks;

        //        return Ok(new
        //        {
        //            Errors = Array.Empty<Array>(),
        //            Status = "Success",
        //            Bid = bid
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("materials")]
        //[Authorize(Roles = "ProjectManager,Client,Contractor")]
        //public async Task<IActionResult> GetMaterialsList()
        //{
        //    try
        //    {
        //        BidDBModel bid = await _costData.GetBid(0);

        //        if (bid == null)
        //        {
        //            return Ok(new
        //            {
        //                Errors = Array.Empty<Array>(),
        //                Status = "Bid Not Found",
        //                Bid = new { }
        //            });
        //        }

        //        List<BidTasksDBModel> bidTasks = await _costData.GetBidTasks(0);
        //        bid.BidTasks = bidTasks;

        //        return Ok(new
        //        {
        //            Errors = Array.Empty<Array>(),
        //            Status = "Success",
        //            Bid = bid
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        [HttpGet]
        [Route("equipments")]
        //[Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> GetEquipmentList()
        {
            try
            {
                List<EquipmentResponse> equipments = await _costData.GetEquipmentList();

                if (equipments == null)
                {
                    return Ok(new
                    {
                        Errors = Array.Empty<Array>(),
                        Status = "Equipments Not Found",
                        Equipments = new { }
                    });
                }
                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Equipments = equipments
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("labours")]
        //[Authorize(Roles = "ProjectManager,Client,Contractor")]
        //public async Task<IActionResult> GetLabourList()
        //{
        //    try
        //    {
        //        BidDBModel bid = await _costData.GetBid(0);

        //        if (bid == null)
        //        {
        //            return Ok(new
        //            {
        //                Errors = Array.Empty<Array>(),
        //                Status = "Bid Not Found",
        //                Bid = new { }
        //            });
        //        }

        //        List<BidTasksDBModel> bidTasks = await _costData.GetBidTasks(0);
        //        bid.BidTasks = bidTasks;

        //        return Ok(new
        //        {
        //            Errors = Array.Empty<Array>(),
        //            Status = "Success",
        //            Bid = bid
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

    }
}
