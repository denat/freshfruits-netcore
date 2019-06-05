using FreshFruits.Controllers;
using FreshFruits.Models;
using FreshFruits.Repositories.Interfaces;
using FreshFruits.Services;
using FreshFruits.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Xunit;

namespace FreshFruits.Tests.Integration
{
    public class ShoppingCartIntegrationTests
    {
        private Fruit _apple = new Fruit { Id = 1, Name = "Apple", Price = 3.99m, Color = Color.Red, Description = "An apple", Image = "apple.jpg", Rating = 4 };
        private Fruit _banana = new Fruit { Id = 2, Name = "Banana", Price = 1.99m, Color = Color.Yellow, Description = "A banana", Image = "banana.jpg", Rating = 2 };
        private Fruit _orange = new Fruit { Id = 3, Name = "Orange", Price = 5.99m, Color = Color.Orange, Description = "An orange", Image = "orange.jpg", Rating = 5 };

        [Fact]
        public void AddToCart_SingleItem_CartHasOneItem()
        {
            // Arrange
            var shoppingCart = new ShoppingCart();
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var sessionManagerMock = new Mock<ISessionManager>();
            var tempDataDictionary = new Mock<ITempDataDictionary>();

            fruitRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(_apple);
            sessionManagerMock.Setup(x => x.GetShoppingCart()).Returns(shoppingCart);

            var controller = new HomeController(fruitRepositoryMock.Object, sessionManagerMock.Object);
            controller.TempData = tempDataDictionary.Object;

            // Act
            var result = controller.AddToCart(1).Result;

            // Assert
            Assert.Equal(1, shoppingCart.Count());
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void RemoveFromCart_ItemDoesntExist_ThrowsException()
        {
            // Arrange
            var shoppingCart = new ShoppingCart();
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var sessionManagerMock = new Mock<ISessionManager>();
            var tempDataDictionary = new Mock<ITempDataDictionary>();

            sessionManagerMock.Setup(x => x.GetShoppingCart()).Returns(shoppingCart);

            var controller = new HomeController(fruitRepositoryMock.Object, sessionManagerMock.Object);
            controller.TempData = tempDataDictionary.Object;

            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                // Act
                controller.RemoveFromCart(1).Wait();
            });
        }

        [Fact]
        public void RemoveFromCart_ItemExists_CartIsEmpty()
        {
            // Arrange
            var shoppingCart = new ShoppingCart();
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var sessionManagerMock = new Mock<ISessionManager>();
            var tempDataDictionary = new Mock<ITempDataDictionary>();

            shoppingCart.Add(_apple);
            fruitRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(_apple);
            sessionManagerMock.Setup(x => x.GetShoppingCart()).Returns(shoppingCart);

            var controller = new HomeController(fruitRepositoryMock.Object, sessionManagerMock.Object);
            controller.TempData = tempDataDictionary.Object;

            // Act
            var result = controller.RemoveFromCart(1).Result;

            // Assert
            Assert.Equal(0, shoppingCart.Count());
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
