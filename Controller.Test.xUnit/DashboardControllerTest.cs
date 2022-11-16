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

namespace Controller.Test.xUnit
{
    public class DashboardControllerTest
    {
        private readonly IFixture _ifixture;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IUserData> _userData;
        private readonly AuthenticationController _authenticationController;

        public DashboardControllerTest()
        {
            _ifixture = new Fixture();
            _configuration = _ifixture.Freeze<Mock<IConfiguration>>();
            _userData = _ifixture.Freeze<Mock<IUserData>>();

            _authenticationController = new AuthenticationController(_configuration.Object, _userData.Object);
        }

        [Fact]
        [Trait("Category","Dashboard")]
        public async void Returns_Dashboard_data()
        {
            var UserModelMock = _ifixture.Create<UserModel>();
            
            _userData.Setup(x => x.GetUser(2)).ReturnsAsync(UserModelMock);
            var result = await _authenticationController.GetUser(2);
            var processedResult = result.Result as OkObjectResult;

            var processedResultValue = processedResult?.Value;

            Assert.NotNull(processedResultValue);
            Assert.Equal(UserModelMock, processedResultValue);
        }

        [Fact]
        [Trait("Category", "Dashboard")]
        public async void Returns_Tile_Numbers()
        {
            var UserModelMock = _ifixture.Create<IEnumerable<UserModel>>();

            _userData.Setup(x => x.GetUsers()).ReturnsAsync(UserModelMock);
            var result = await _authenticationController.GetUsers();
            var processedResult = result.Result as OkObjectResult;

            var processedResultValue = processedResult?.Value;

            Assert.NotNull(processedResultValue);
            Assert.Equal(UserModelMock, processedResultValue);
        }
    }
}