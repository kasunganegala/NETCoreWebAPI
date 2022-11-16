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
using Microsoft.Extensions.DependencyInjection;
using DataAccess.DBAccess;
using System.Threading.Tasks;
using System;
using DataAccess.Models.Authentication;
using Newtonsoft.Json;

namespace Controller.Test.xUnit
{
    //public class DependencySetupFixture
    //{
    //    public DependencySetupFixture()
    //    {
    //        var serviceCollection = new ServiceCollection();
    //        serviceCollection.AddControllers();

    //        //serviceCollection.AddDbContext<SharedServicesContext>(options => options.UseInMemoryDatabase(databaseName: "TestDatabase"));
    //        serviceCollection.AddSingleton<ISqlDataAccess, SqlDataAccess>();
    //        serviceCollection.AddSingleton<IUserData, UserData>();
    //        serviceCollection.AddSingleton<ITenderData, TenderData>();
    //        serviceCollection.AddSingleton<IBidData, BidData>();

    //        serviceCollection.AddCors(options => options
    //           .AddDefaultPolicy(builder => builder
    //               .AllowAnyOrigin()
    //               .AllowAnyHeader()
    //               .AllowAnyMethod()
    //               )
    //           );

    //        ServiceProvider = serviceCollection.BuildServiceProvider();
    //    }

    //    public ServiceProvider ServiceProvider { get; private set; }
    //}

    public class AuthenticationTest//: IClassFixture<DependencySetupFixture>
    {
        private ServiceProvider _serviceProvider;

        private readonly IFixture _ifixture;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IUserData> _userData;
        private readonly AuthenticationController _authenticationController;

        //public AuthenticationTest(DependencySetupFixture fixture)
        public AuthenticationTest()
        {
            var serviceCollection = new ServiceCollection();
            _ifixture = new Fixture();
            _configuration = _ifixture.Freeze<Mock<IConfiguration>>();
            _userData = _ifixture.Freeze<Mock<IUserData>>();

            _authenticationController = new AuthenticationController(_configuration.Object, _userData.Object);
            //_serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        [Trait("Category","Authentication")]
        public async void Returns_Correct_User()
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
        [Trait("Category", "Authentication")]
        public async void Returns_Correct_Number_Of_Users()
        {
            var UserModelMock = _ifixture.Create<IEnumerable<UserModel>>();

            _userData.Setup(x => x.GetUsers()).ReturnsAsync(UserModelMock);
            var result = await _authenticationController.GetUsers();
            var processedResult = result.Result as OkObjectResult;

            var processedResultValue = processedResult?.Value;

            Assert.NotNull(processedResultValue);
            Assert.Equal(UserModelMock, processedResultValue);
        }

        [Fact]
        [Trait("Category", "Authentication")]
        public async void Returns_Login()
        {
            UserModel UserModelMock = _ifixture.Create<UserModel>();
            UserModelMock.IsDeactivated = false;
            IEnumerable<RolesModel> RolesModelMock = _ifixture.Create<IEnumerable<RolesModel>>();


            _configuration.Setup(x => x["JWT:Secret"]).Returns("ConstructionProjectManagementSystem");
            _configuration.Setup(x => x["JWT:ValidIssuer"]).Returns("http://localhost:44331");
            _configuration.Setup(x => x["JWT:ValidAudience"]).Returns("FortranUsers");

            _userData.Setup(x => x.GetUser("c@c.com", "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3")).ReturnsAsync(UserModelMock);
            _userData.Setup(x => x.GetUserRoles(UserModelMock.Id)).ReturnsAsync(RolesModelMock);

            var result = await _authenticationController.Login(new LoginModel() { Email= "c@c.com", Password= "123" });
            var processedResult = result as OkObjectResult;

            dynamic processedResultValue = processedResult?.Value;

            Assert.NotNull(processedResultValue);
        }

        [Fact]
        [Trait("Category", "Authentication")]
        public async void Returns_Logout()
        {
            UserModel UserModelMock = _ifixture.Create<UserModel>();
            UserModelMock.IsDeactivated = false;
            IEnumerable<RolesModel> RolesModelMock = _ifixture.Create<IEnumerable<RolesModel>>();


            _configuration.Setup(x => x["JWT:Secret"]).Returns("ConstructionProjectManagementSystem");
            _configuration.Setup(x => x["JWT:ValidIssuer"]).Returns("http://localhost:44331");
            _configuration.Setup(x => x["JWT:ValidAudience"]).Returns("FortranUsers");

            _userData.Setup(x => x.GetUser("c@c.com", "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3")).ReturnsAsync(UserModelMock);
            _userData.Setup(x => x.GetUserRoles(UserModelMock.Id)).ReturnsAsync(RolesModelMock);

            var result = await _authenticationController.Login(new LoginModel() { Email = "c@c.com", Password = "123" });
            var processedResult = result as OkObjectResult;

            dynamic processedResultValue = processedResult?.Value;

            Assert.NotNull(processedResultValue);
        }

    }
}