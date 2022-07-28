
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

namespace NETCoreWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
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

    }
}
