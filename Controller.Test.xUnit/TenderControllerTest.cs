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
            OkObjectResult processedResult = result as OkObjectResult;

            Assert.NotNull(processedResult?.Value);
        }

        [Fact]
        [Trait("Category", "Tender")]
        public async void Returns_Tenders()
        {
            var TenderSearchRequestMock = _ifixture.Create<TenderSearchRequest>();
            var GridTenderDBModelMock = _ifixture.Create<Grid<TenderDBModel>>();
            _tenderData.Setup(x => x.GetTenders(TenderSearchRequestMock)).ReturnsAsync(GridTenderDBModelMock);

            var result = await _tenderController.Tenders(TenderSearchRequestMock);
            var processedResult = result as OkObjectResult;

            Assert.NotNull(processedResult?.Value);

            var Errors = (dynamic)processedResult?.Value?.GetType().GetProperty("Errors").GetValue(processedResult?.Value);
            var Status = (string)processedResult?.Value?.GetType().GetProperty("Status").GetValue(processedResult?.Value);
            var Tenders = (dynamic)processedResult?.Value?.GetType().GetProperty("Tenders").GetValue(processedResult?.Value);
            var Total = (int)processedResult?.Value?.GetType().GetProperty("Total").GetValue(processedResult?.Value);

            Assert.Equal(Status, "Success");
        }

        [Fact]
        [Trait("Category", "Tender")]
        public async void Returns_Put_Tender_Hold()
        {
            int TenderidMock = _ifixture.Create<int>();
            TenderidMock = 2;
            _tenderData.Setup(x => x.SetTenderHold(TenderidMock)).ReturnsAsync(TenderidMock);

            var result = await _tenderController.Hold(TenderidMock);
            var processedResult = result as OkObjectResult;

            Assert.NotNull(processedResult?.Value);

            var Errors = (dynamic)processedResult?.Value?.GetType().GetProperty("Errors").GetValue(processedResult?.Value);
            var Status = (string)processedResult?.Value?.GetType().GetProperty("Status").GetValue(processedResult?.Value);
            var Tender = (int)processedResult?.Value?.GetType().GetProperty("Tender").GetValue(processedResult?.Value);

            Assert.Equal(Status, "Success");
            Assert.NotNull(Tender);
            Assert.Equal(Tender, TenderidMock);

        }

        [Fact]
        [Trait("Category", "Tender")]
        public async void Returns_Put_Tender_Close()
        {
            int TenderidMock = _ifixture.Create<int>();
            TenderidMock = 2;
            _tenderData.Setup(x => x.SetTenderClose(TenderidMock)).ReturnsAsync(TenderidMock);

            var result = await _tenderController.Close(TenderidMock);
            var processedResult = result as OkObjectResult;

            Assert.NotNull(processedResult?.Value);

            var Errors = (dynamic)processedResult?.Value?.GetType().GetProperty("Errors").GetValue(processedResult?.Value);
            var Status = (string)processedResult?.Value?.GetType().GetProperty("Status").GetValue(processedResult?.Value);
            var Tender = (int)processedResult?.Value?.GetType().GetProperty("Tender").GetValue(processedResult?.Value);

            Assert.Equal(Status, "Success");
            Assert.NotNull(Tender);
            Assert.Equal(Tender, TenderidMock);

        }
    }

    //public class TestTenderCreateResut
    //{
    //    public Array[] Errors { get; set; }
    //    public string Status { get; set; }
    //    public int Tenders { get; set; }
    //}
}