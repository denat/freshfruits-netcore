using FreshFruits.Data;
using FreshFruits.Models;
using FreshFruits.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FreshFruits.Tests.Unit
{
    public class FruitRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> GetDbContextOptions(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return options;
        }

        [Fact]
        public void GetById_ItemDoesntExist_ReturnsNull()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions("GetById_ItemDoesntExist_ReturnsNull")))
            {
                // Arrange
                var repo = new FruitRepository(context);

                // Act
                var item = repo.GetById(123).Result;

                // Assert
                Assert.Null(item);
            }
        }

        [Fact]
        public void GetById_ItemExists_ReturnsItem()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions("GetById_ItemExists_ReturnsTheItem")))
            {
                // Arrange
                context.Fruits.Add(new Fruit { Id = 1, Color = Color.Red, Name = "Apple", Price = 2.99m, Rating = 5, Description = "An apple" });
                context.SaveChanges();
                var repo = new FruitRepository(context);

                // Act
                var item = repo.GetById(1).Result;

                // Assert
                Assert.Equal(1, item.Id);
                Assert.Equal("Apple", item.Name);
            }
        }

        [Fact]
        public void GetAll_NoItems_ReturnsEmptyList()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions("GetAll_NoItems_ReturnsEmptyList")))
            {
                // Arrange
                var repo = new FruitRepository(context);

                // Act
                var list = repo.GetAll().Result;

                // Assert
                Assert.Empty(list);
            }
        }

        [Fact]
        public void GetAll_SingleItem_ReturnsListWithSingleItem()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions("GetAll_SingleItem_ReturnsListWithSingleItem")))
            {
                // Arrange
                context.Fruits.Add(new Fruit { Id = 1, Color = Color.Red, Name = "Apple", Price = 2.99m, Rating = 5, Description = "An apple" });
                context.SaveChanges();
                var repo = new FruitRepository(context);

                // Act
                var list = repo.GetAll().Result;

                // Assert
                Assert.Single(list);
            }
        }

        [Fact]
        public void Add_SingleItem_ItemAddedSuccessfully()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions("Add_SingleItem_ItemAddedSuccessfully")))
            {
                // Arrange
                var repo = new FruitRepository(context);
                var itemToAdd = new Fruit { Id = 5, Color = Color.Red, Name = "Apple", Price = 2.99m, Rating = 5, Description = "An apple" };

                // Act
                repo.Add(itemToAdd).Wait();

                // Assert
                var item = repo.GetById(5).Result;
                Assert.NotNull(item);
                Assert.Equal(5, item.Id);
                Assert.Equal("Apple", item.Name);
            }
        }

        [Fact]
        public void Update_ItemDoesntExist_ThrowsException()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions("Update_ItemDoesntExist_ThrowsException")))
            {
                // Arrange
                var repo = new FruitRepository(context);
                var itemToUpdate = new Fruit { Id = 5, Color = Color.Red, Name = "Apple", Price = 2.99m, Rating = 5, Description = "An apple" };

                // Assert
                Assert.ThrowsAny<Exception>(() =>
                {
                    // Act
                    repo.Update(itemToUpdate).Wait();
                });
            }
        }

        [Fact]
        public void Update_ItemExists_UpdatesItemSuccessfully()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions("Update_ItemExists_UpdatesItemSuccessfully")))
            {
                // Arrange
                var repo = new FruitRepository(context);
                var item = new Fruit { Id = 5, Color = Color.Red, Name = "Apple", Price = 2.99m, Rating = 5, Description = "An apple" };
                repo.Add(item).Wait();

                // Act
                item.Name = "Banana";
                repo.Update(item).Wait();

                // Assert
                var updatedItem = repo.GetById(5).Result;
                Assert.NotNull(updatedItem);
                Assert.Equal("Banana", updatedItem.Name);
            }
        }

        [Fact]
        public void DeleteById_ItemDoesntExist_ThrowsException()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions("Delete_ItemDoesntExist_ThrowsException")))
            {
                // Arrange
                var repo = new FruitRepository(context);

                // Assert
                Assert.ThrowsAny<Exception>(() =>
                {
                    // Act
                    repo.DeleteById(123).Wait();
                });
            }
        }

        [Fact]
        public void DeleteById_ItemExists_DeletesItemSuccessfully()
        {
            using (var context = new ApplicationDbContext(GetDbContextOptions("DeleteById_ItemExists_DeletesItemSuccessfully")))
            {
                // Arrange
                var repo = new FruitRepository(context);
                var item = new Fruit { Id = 5, Color = Color.Red, Name = "Apple", Price = 2.99m, Rating = 5, Description = "An apple" };
                repo.Add(item).Wait();

                // Act
                repo.DeleteById(5).Wait();

                // Assert
                var deletedItem = repo.GetById(5).Result;
                Assert.Null(deletedItem);
            }
        }
    }
}
