using DataAccess.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NETCoreWebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Test
{
    [TestClass]
    public class TestInitializer
    {
        public static HttpClient TestHttpClient;
        public static Mock<IUserData> MockUserData;

        [AssemblyInitialize]
        public static void InitializeTestServer(TestContext testContext)
        {
            var testServer = new TestServer(new WebHostBuilder()
               .UseStartup<TestStartup>()
               // this would cause it to use StartupIntegrationTest class
               // or ConfigureServicesIntegrationTest / ConfigureIntegrationTest
               // methods (if existing)
               // rather than Startup, ConfigureServices and Configure
               .UseEnvironment("Development"));

            TestHttpClient = testServer.CreateClient();
        }

        public static void RegisterMockRepositories(IServiceCollection services)
        {
            MockUserData = (new Mock<IUserData>());
            services.AddSingleton(MockUserData.Object);

            //add more mock repositories below
        }
    }
}
