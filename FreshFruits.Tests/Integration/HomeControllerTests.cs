using AngleSharp.Dom;
using FreshFruits.Controllers;
using FreshFruits.Tests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
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
    public class HomeControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public HomeControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Home")]
        [InlineData("/Home/About")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetIndex_ShoppingCartStartsEmpty()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Home");

            var html = await HtmlHelpers.GetDocumentAsync(response);
            var element = html.QuerySelector(".shopping-cart-items li");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("Shopping cart is empty", element.Text());
        }

        [Fact]
        public async Task AddItemToCart_ShoppingCartHasOneItem()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Home/AddToCart/1");

            var html = await HtmlHelpers.GetDocumentAsync(response);
            var elements = html.QuerySelectorAll(".shopping-cart-items li.product-item");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(1, elements.Length);
        }

        [Fact]
        public async Task AddItemsToCart_ShoppingCartHasCorrectAmountOfItems()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            await client.GetAsync("/Home/AddToCart/1");
            await client.GetAsync("/Home/AddToCart/2");
            var response = await client.GetAsync("/Home/AddToCart/3");

            var html = await HtmlHelpers.GetDocumentAsync(response);
            var elements = html.QuerySelectorAll(".shopping-cart-items li.product-item");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(3, elements.Length);
        }
    }
}
