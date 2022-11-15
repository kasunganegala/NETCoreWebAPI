using AutoFixture;
using DataAccess.Data;
using DataAccess.Models;
using Moq;
using NETCoreWebAPI.Controllers;
using Microsoft.Extensions.Configuration;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DataAccess.Models.Common;
using DataAccess.Models.Tender;
using NETCoreWebAPI.BusinessRules.Tender;
using System;

namespace Controller.Test.xUnit
{
    public class TenderControllerTest
    {
        private readonly IFixture _ifixture;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<ITenderData> _tenderData;
        private readonly TenderController _tenderController;

        public TenderControllerTest()
        {
            _ifixture = new Fixture();
            _configuration = _ifixture.Freeze<Mock<IConfiguration>>();
            _tenderData = _ifixture.Freeze<Mock<ITenderData>>();

            _tenderController = new TenderController(_configuration.Object, _tenderData.Object);
        }

        [Fact]
        [Trait("Category","Tender")]
        public async void Returns_Tender()
        {
            var TenderModelMock = _ifixture.Create<TenderDBModel>();
            _tenderData.Setup(x => x.GetTender(2)).ReturnsAsync(TenderModelMock);

            var TenderTasksModelMock = _ifixture.Create<List<TenderTasksDBModel>>();
            _tenderData.Setup(x => x.GetTenderTasks(2)).ReturnsAsync(TenderTasksModelMock);


            var result = await _tenderController.Tender(2);
            var processedResult = result as OkObjectResult;

            var processedResultValue = processedResult?.Value;

            Assert.NotNull(processedResultValue);
        }

        [Fact]
        [Trait("Category", "Tender")]
        public async void Returns_Newly_Created_Tender_Id()
        {
            var tenderValidationsMock = _ifixture.Create<List<Error>>();
            //var tenderDBMock = _ifixture.Create<TenderDBModel>();
            var tenderMock = _ifixture.Create<TenderModel>();
            var newTenderId = 4;

            tenderMock.StartDateTime = DateTime.Now;
            tenderMock.EndDateTime = DateTime.Now.AddDays(1);

            TenderDBModel tenderDBMock = TenderBusinessRules.GenerateTenderModel(tenderMock);
            _tenderData.Setup(x => x.InsertNewTender(tenderDBMock)).ReturnsAsync(newTenderId);

            var result = _tenderController.Create(tenderMock).Result;
            var processedResult = result as OkObjectResult;

            var processedResultValue = (dynamic)processedResult?.Value;

            Assert.NotNull(processedResultValue);
        }
    }
}