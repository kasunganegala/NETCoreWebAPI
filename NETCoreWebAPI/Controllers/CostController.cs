
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
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

        [HttpGet]
        [Route("uom")]
        [Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> GetUOMList()
        {
            try
            {
                List<UOMResponse> uom = await _costData.GetUOMList();

                if (uom == null)
                {
                    return Ok(new
                    {
                        Errors = Array.Empty<Array>(),
                        Status = "UOMs Not Found",
                        UOM = new { }
                    });
                }
                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    UOM = uom
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("materials")]
        [Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> GetMaterialsList()
        {
            try
            {
                List<MaterialsResponse> materils = await _costData.GetMaterialsList();

                if (materils == null)
                {
                    return Ok(new
                    {
                        Errors = Array.Empty<Array>(),
                        Status = "Materials Not Found",
                        Materials = new { }
                    });
                }
                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Materials = materils
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("equipments")]
        [Authorize(Roles = "ProjectManager,Client,Contractor")]
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

        [HttpGet]
        [Route("labours")]
        [Authorize(Roles = "ProjectManager,Client,Contractor")]
        public async Task<IActionResult> GetLabourList()
        {
            try
            {
                List<LabourResponse> labours = await _costData.GetLabourList();

                if (labours == null)
                {
                    return Ok(new
                    {
                        Errors = Array.Empty<Array>(),
                        Status = "Labours Not Found",
                        Labours = new { }
                    });
                }
                return Ok(new
                {
                    Errors = Array.Empty<Array>(),
                    Status = "Success",
                    Labours = labours
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
