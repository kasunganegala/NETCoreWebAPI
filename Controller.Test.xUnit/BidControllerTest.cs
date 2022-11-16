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
using DataAccess.Models.Bid;
using DataAccess.Models.Common;
using System;
using NETCoreWebAPI.BusinessRules.Bid;

namespace Controller.Test.xUnit
{
    public class BidControllerTest
    {
        private readonly IFixture _ifixture;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IBidData> _bidData;
        private readonly BidController _bidController;

        public BidControllerTest()
        {
            _ifixture = new Fixture();
            _configuration = _ifixture.Freeze<Mock<IConfiguration>>();
            _bidData = _ifixture.Freeze<Mock<IBidData>>();

            _bidController = new BidController(_configuration.Object, _bidData.Object);
        }

        [Fact]
        [Trait("Category", "Bid")]
        public async void Returns_Newly_Created_Bid_Id()
        {
            var bidValidationsMock = _ifixture.Create<List<Error>>();
            //var tenderDBMock = _ifixture.Create<TenderDBModel>();
            var bidMock = _ifixture.Create<BidModel>();
            var newTenderId = 4;

            bidMock.StartDateTime = DateTime.Now;
            bidMock.EndDateTime = DateTime.Now.AddDays(1);

            BidDBModel bidDBMock = BidBusinessRules.GenerateBidModel(bidMock);
            _bidData.Setup(x => x.InsertNewBid(bidDBMock)).ReturnsAsync(newTenderId);

            var result = _bidController.Create(bidMock).Result;
            OkObjectResult processedResult = result as OkObjectResult;

            Assert.NotNull(processedResult?.Value);

            var Errors = (dynamic)processedResult?.Value?.GetType().GetProperty("Errors").GetValue(processedResult?.Value);
            var Status = (string)processedResult?.Value?.GetType().GetProperty("Status").GetValue(processedResult?.Value);
            var BidId = (int)processedResult?.Value?.GetType().GetProperty("BidId").GetValue(processedResult?.Value);

            Assert.Equal(Status, "Success");
        }

        [Fact]
        [Trait("Category", "Bid")]
        public async void Returns_Bid()
        {
            var bidValidationsMock = _ifixture.Create<List<Error>>();
            //var tenderDBMock = _ifixture.Create<TenderDBModel>();
            var bidMock = _ifixture.Create<BidModel>();
            var newTenderId = 4;

            bidMock.StartDateTime = DateTime.Now;
            bidMock.EndDateTime = DateTime.Now.AddDays(1);

            BidDBModel bidDBMock = BidBusinessRules.GenerateBidModel(bidMock);
            _bidData.Setup(x => x.InsertNewBid(bidDBMock)).ReturnsAsync(newTenderId);

            var result = _bidController.Create(bidMock).Result;
            OkObjectResult processedResult = result as OkObjectResult;

            Assert.NotNull(processedResult?.Value);

            var Errors = (dynamic)processedResult?.Value?.GetType().GetProperty("Errors").GetValue(processedResult?.Value);
            var Status = (string)processedResult?.Value?.GetType().GetProperty("Status").GetValue(processedResult?.Value);
            var BidId = (int)processedResult?.Value?.GetType().GetProperty("BidId").GetValue(processedResult?.Value);

            Assert.Equal(Status, "Success");
        }


        [Fact]
        [Trait("Category", "Bid")]
        public async void Returns_Accept_Bid()
        {
            var bidValidationsMock = _ifixture.Create<List<Error>>();
            //var tenderDBMock = _ifixture.Create<TenderDBModel>();
            var bidMock = _ifixture.Create<BidModel>();
            var newTenderId = 4;

            bidMock.StartDateTime = DateTime.Now;
            bidMock.EndDateTime = DateTime.Now.AddDays(1);

            BidDBModel bidDBMock = BidBusinessRules.GenerateBidModel(bidMock);
            _bidData.Setup(x => x.InsertNewBid(bidDBMock)).ReturnsAsync(newTenderId);

            var result = _bidController.Create(bidMock).Result;
            OkObjectResult processedResult = result as OkObjectResult;

            Assert.NotNull(processedResult?.Value);

            var Errors = (dynamic)processedResult?.Value?.GetType().GetProperty("Errors").GetValue(processedResult?.Value);
            var Status = (string)processedResult?.Value?.GetType().GetProperty("Status").GetValue(processedResult?.Value);
            var BidId = (int)processedResult?.Value?.GetType().GetProperty("BidId").GetValue(processedResult?.Value);

            Assert.Equal(Status, "Success");
        }


        [Fact]
        [Trait("Category", "Bid")]
        public async void Returns_Reject_Bid()
        {
            var bidValidationsMock = _ifixture.Create<List<Error>>();
            //var tenderDBMock = _ifixture.Create<TenderDBModel>();
            var bidMock = _ifixture.Create<BidModel>();
            var newTenderId = 4;

            bidMock.StartDateTime = DateTime.Now;
            bidMock.EndDateTime = DateTime.Now.AddDays(1);

            BidDBModel bidDBMock = BidBusinessRules.GenerateBidModel(bidMock);
            _bidData.Setup(x => x.InsertNewBid(bidDBMock)).ReturnsAsync(newTenderId);

            var result = _bidController.Create(bidMock).Result;
            OkObjectResult processedResult = result as OkObjectResult;

            Assert.NotNull(processedResult?.Value);

            var Errors = (dynamic)processedResult?.Value?.GetType().GetProperty("Errors").GetValue(processedResult?.Value);
            var Status = (string)processedResult?.Value?.GetType().GetProperty("Status").GetValue(processedResult?.Value);
            var BidId = (int)processedResult?.Value?.GetType().GetProperty("BidId").GetValue(processedResult?.Value);

            Assert.Equal(Status, "Success");
        }

    }
}


