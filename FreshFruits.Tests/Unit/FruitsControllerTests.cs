using FreshFruits.Areas.Admin.Controllers;
using FreshFruits.Models;
using FreshFruits.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FreshFruits.Tests.Unit
{
    public class FruitsControllerTests
    {
        [Fact]
        public void GetList_NoItems_ReturnsView()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var list = new List<Fruit>();
            fruitRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(list);
            var controller = new FruitsController(fruitRepositoryMock.Object);

            // Act
            var result = controller.Index().Result;

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ViewList_SingleItemInList_ReturnsViewWithSingleItem()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var list = new List<Fruit>();
            list.Add(new Fruit { Id = 1, Color = Color.Red, Name = "Apple", Price = 2.99m, Rating = 5, Description = "An apple" });
            fruitRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(list);
            var controller = new FruitsController(fruitRepositoryMock.Object);

            // Act
            var result = controller.Index().Result;
            var viewModelList = (List<Fruit>)((ViewResult)result).Model;

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.Single(viewModelList);
        }

        [Fact]
        public void ViewDetails_ItemExists_ReturnsView()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            fruitRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(new Fruit { Id = 1, Color = Color.Red, Name = "Apple", Price = 2.99m, Rating = 5, Description = "An apple", Image = "apple.jpeg" });
            var controller = new FruitsController(fruitRepositoryMock.Object);

            // Act
            var result = controller.Details(1).Result;

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ViewDetails_ItemDoesntExist_Returns404NotFound()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var controller = new FruitsController(fruitRepositoryMock.Object);

            // Act
            var result = controller.Details(2).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ViewCreateForm_AlwaysReturnsView()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var controller = new FruitsController(fruitRepositoryMock.Object);

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SubmitCreate_CorrectInput_RedirectsOnSuccess()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var controller = new FruitsController(fruitRepositoryMock.Object);
            var itemToInsert = new Fruit
            {
                Id = 1,
                Color = Color.Red,
                Name = "Apple",
                Price = 2.99m,
                Rating = 5,
                Description = "An apple",
                Image = "apple.jpeg"
            };

            // Act

            var result = controller.Create(itemToInsert).Result;

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void SubmitCreate_IncorrectInput_ReturnsViewAgain()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var controller = new FruitsController(fruitRepositoryMock.Object);
            var itemToInsert = new Fruit { Id = 1 }; // Missing name!

            // Act
            var result = controller.Create(itemToInsert).Result;

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ViewEditForm_ExistingItem_ReturnsView()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var controller = new FruitsController(fruitRepositoryMock.Object);
            fruitRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(new Fruit { Id = 1, Color = Color.Red, Name = "Apple", Price = 2.99m, Rating = 5, Description = "An apple", Image = "apple.jpeg" });
            
            // Act
            var result = controller.Edit(1).Result;

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ViewEditForm_NonExistingItem_Returns404NotFound()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var controller = new FruitsController(fruitRepositoryMock.Object);

            // Act
            var result = controller.Edit(1).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void SubmitEdit_IncorrectInput_ReturnsViewAgain()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var controller = new FruitsController(fruitRepositoryMock.Object);

            var originalItem = new Fruit
            {
                Id = 1,
                Color = Color.Red,
                Name = "Apple",
                Price = 2.99m,
                Rating = 5,
                Description = "An apple",
                Image = "apple.jpeg"
            };

            fruitRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(originalItem);

            var updatedItem = new Fruit
            {
                Id = 1,
                Color = Color.Red,
                Name = null, // Missing name!
                Price = 2.99m,
                Rating = 5,
                Description = "An apple",
                Image = "apple.jpeg"
            };

            // Act
            var result = controller.Edit(1, updatedItem).Result;

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ViewDeleteForm_NonExistingItem_Returns404NotFound()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var controller = new FruitsController(fruitRepositoryMock.Object);

            // Act
            var result = controller.Delete(1).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void SubmitDelete_NonExistingItem_Returns404NotFound()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var controller = new FruitsController(fruitRepositoryMock.Object);

            // Act
            var result = controller.DeleteConfirmed(1).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void SubmitDelete_ExistingItem_RedirectsToIndex()
        {
            // Arrange
            var fruitRepositoryMock = new Mock<IFruitRepository>();
            var controller = new FruitsController(fruitRepositoryMock.Object);
            fruitRepositoryMock.Setup(x => x.GetById(1)).ReturnsAsync(new Fruit
            {
                Id = 1,
                Color = Color.Red,
                Name = "Apple",
                Price = 2.99m,
                Rating = 5,
                Description = "An apple",
                Image = "apple.jpeg"
            });

            // Act
            var result = controller.DeleteConfirmed(1).Result;

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
