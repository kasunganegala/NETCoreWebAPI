using DataAccess.Data;
using DataAccess.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NETCoreWebAPI.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controller.Test
{
    [TestClass()]
    [TestCategory("Authentication")]
    public class AuthenticationControllerTest
    {
        public AuthenticationControllerTest()
        {

        }


        [TestMethod("Sample Test")]
        //[DataTestMethod]
        //[DataRow(1, "Jignesh")]
        //[DataRow(2, "Rakesh")]
        //[DataRow(3, "Not Found")]
        public void TestMethod1()
        {
            //var myConfiguration = new Dictionary<string, string>
            //{
            //    {"Key1", "Value1"},
            //    {"Nested:Key1", "NestedValue1"},
            //    {"Nested:Key2", "NestedValue2"}
            //};

            //var configuration = new ConfigurationBuilder()
            //    .AddInMemoryCollection(myConfiguration)
            //    .Build();

            var fakeUsers = A.CollectionOfDummy<UserModel>(1);
            var Configuration = A.Fake<IConfiguration>();
            var UserData = A.Fake<IUserData>();
            
            //A.CallTo(()=> UserData.GetUser(1)).Return()
            var authenticationController = new AuthenticationController(Configuration, UserData);
            //var response = TestInitializer.TestHttpClient.GetAsync("https://localhost:44331/api/Authentication/Users/1").Result;
            //var user = await authenticationController.GetUser(1);

            Assert.AreEqual(1, 1);
        }
    }
}