using FreshFruits.Models;
using FreshFruits.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FreshFruits.Tests.Unit
{
    public class ShoppingCartServiceTests
    {
        private Fruit _apple = new Fruit { Id = 1, Name = "Apple", Price = 3.99m, Color = Color.Red, Description = "An apple", Image = "apple.jpg", Rating = 4 };
        private Fruit _banana = new Fruit { Id = 2, Name = "Banana", Price = 1.99m, Color = Color.Yellow, Description = "A banana", Image = "banana.jpg", Rating = 2 };
        private Fruit _orange = new Fruit { Id = 3, Name = "Orange", Price = 5.99m, Color = Color.Orange, Description = "An orange", Image = "orange.jpg", Rating = 5 };

        [Fact]
        public void GetCount_NoItemsInCart_ReturnsZero()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();
            
            // Assert
            Assert.Equal(0, shoppingCart.Count());
        }

        [Fact]
        public void Add_SingleItem_CountIsOne()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();

            // Act
            shoppingCart.Add(_apple);

            // Assert
            Assert.Equal(1, shoppingCart.Count());
        }

        [Fact]
        public void Add_MultipleItems_CountIsCorrect()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();

            // Act
            shoppingCart.Add(_apple);
            shoppingCart.Add(_apple);
            shoppingCart.Add(_banana);
            shoppingCart.Add(_orange);

            // Assert
            Assert.Equal(4, shoppingCart.Count());
        }

        [Fact]
        public void Remove_EmptyCart_ThrowsException()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();

            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                // Act
                shoppingCart.Remove(_apple);
            });
        }

        [Fact]
        public void Remove_PreviouslyAddedItem_CountIsZero()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();
            shoppingCart.Add(_apple);

            // Act
            shoppingCart.Remove(_apple);

            // Assert
            Assert.Equal(0, shoppingCart.Count());
        }

        [Fact]
        public void Remove_OneOfTwoSameItems_DoesntRemoveAllSameItems()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();
            shoppingCart.Add(_apple);
            shoppingCart.Add(_apple);

            // Act
            shoppingCart.Remove(_apple);

            // Assert
            Assert.Equal(1, shoppingCart.Count());
        }

        [Fact]
        public void Remove_TwoSameItems_CountIsZero()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();
            shoppingCart.Add(_apple);
            shoppingCart.Add(_apple);

            // Act
            shoppingCart.Remove(_apple);
            shoppingCart.Remove(_apple);

            // Assert
            Assert.Equal(0, shoppingCart.Count());
        }

        [Fact]
        public void Remove_ThreeTimesWhileCartHasTwoItems_ThrowsException()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();
            shoppingCart.Add(_apple);
            shoppingCart.Add(_apple);

            // Act
            shoppingCart.Remove(_apple);
            shoppingCart.Remove(_apple);

            // Assert
            Assert.ThrowsAny<Exception>(() =>
            {
                // Act
                shoppingCart.Remove(_apple);
            });
        }

        [Fact]
        public void CalculateTotalPrice_EmptyCart_ReturnsZero()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();

            // Act
            var price = shoppingCart.CalculateTotalPrice();

            // Assert
            Assert.Equal(0, price);
        }

        [Fact]
        public void CalculateTotalPrice_SingleItem_ReturnsCorrectPrice()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();
            shoppingCart.Add(_apple);

            // Act
            var price = shoppingCart.CalculateTotalPrice();

            // Assert
            Assert.Equal(_apple.Price, price);
        }

        [Fact]
        public void CalculateTotalPrice_MultipleSameItems_ReturnsCorrectPrice()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();
            shoppingCart.Add(_apple);
            shoppingCart.Add(_apple);
            shoppingCart.Add(_apple);

            // Act
            var price = shoppingCart.CalculateTotalPrice();

            // Assert
            Assert.Equal(_apple.Price * 3, price);
        }

        [Fact]
        public void CalculateTotalPrice_MultipleDifferentItems_ReturnsCorrectPrice()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();
            shoppingCart.Add(_apple);
            shoppingCart.Add(_banana);
            shoppingCart.Add(_orange);

            // Act
            var price = shoppingCart.CalculateTotalPrice();

            // Assert
            Assert.Equal(_apple.Price + _banana.Price + _orange.Price, price);
        }

        [Fact]
        public void Add_FullCart_ThrowsException()
        {
            // Arrange
            var shoppingCart = new ShoppingCartService();

            // Fill up the cart first
            shoppingCart.Add(_apple);
            shoppingCart.Add(_apple);
            shoppingCart.Add(_apple);
            shoppingCart.Add(_apple);
            shoppingCart.Add(_apple);

            // Act
            Assert.ThrowsAny<Exception>(() =>
            {
                // Act
                shoppingCart.Add(_apple);
            });
        }
    }
}
