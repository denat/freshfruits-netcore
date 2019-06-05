using AngleSharp.Dom;
using FreshFruits.Controllers;
using FreshFruits.Data;
using FreshFruits.Tests.Helpers;
using FreshFruits.Tests.Integration.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FreshFruits.Tests.Integration
{
    public class FruitControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public FruitControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetIndex_NotLoggedIn_RedirectsToLoginPage()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Admin/Dashboard");

            // Assert
            Assert.StartsWith("/Identity/Account/Login", response.RequestMessage.RequestUri.AbsolutePath);
        }

        [Fact]
        public async Task GetIndex_LoggedIn_AllowsAccessToIndex()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(
                services => services.AddMvc(
                    options =>
                    {
                        options.Filters.Add(new AllowAnonymousFilter());
                        options.Filters.Add(new FakeUserFilter());
                    }))).CreateClient();

            // Act
            var response = await client.GetAsync("/Admin/Dashboard");

            // Assert
            Assert.StartsWith("/Admin/Fruits", response.RequestMessage.RequestUri.AbsolutePath);
        }
    }
}
