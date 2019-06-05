using FreshFruits.Data;
using FreshFruits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFruits.Tests.Integration.Setup
{
    public static class SeedData
    {
        #region snippet1
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.Fruits.Add(new Fruit
            {
                Id = 1,
                Name = "Apple",
                Description = "An apple is a sweet, edible fruit produced by an apple tree (Malus pumila). Apple trees are cultivated worldwide and are the most widely grown species in the genus Malus.",
                Color = Color.Red,
                Image = "apple.jpg",
                Price = 2.99m,
                Rating = 4
            });

            db.Fruits.Add(new Fruit
            {
                Id = 2,
                Name = "Banana",
                Description = "A banana is an edible fruit – botanically a berry – produced by several kinds of large herbaceous flowering plants in the genus Musa. In some countries, bananas used for cooking may be called \"plantains\", distinguishing them from dessert bananas.",
                Color = Color.Yellow,
                Image = "banana.jpg",
                Price = 1.99m,
                Rating = 3
            });

            db.Fruits.Add(new Fruit
            {
                Id = 3,
                Name = "Blueberry",
                Description = "Blueberries are perennial flowering plants with blue– or purple–colored berries. They are classified in the section Cyanococcus within the genus Vaccinium.",
                Color = Color.Blue,
                Image = "blueberry.jpg",
                Price = 5.99m,
                Rating = 5
            });

            db.Fruits.Add(new Fruit
            {
                Id = 4,
                Name = "Orange",
                Description = "The orange is the fruit of the citrus species Citrus × sinensis in the family Rutaceae. It is also called sweet orange, to distinguish it from the related Citrus × aurantium, referred to as bitter orange.",
                Color = Color.Orange,
                Image = "orange.jpg",
                Price = 4.99m,
                Rating = 4
            });

            db.Fruits.Add(new Fruit
            {
                Id = 5,
                Name = "Strawberry",
                Description = "The garden strawberry (or simply strawberry; Fragaria × ananassa) is a widely grown hybrid species of the genus Fragaria, collectively known as the strawberries. It is cultivated worldwide for its fruit. The fruit is widely appreciated for its characteristic aroma, bright red color, juicy texture, and sweetness.",
                Color = Color.Red,
                Image = "strawberry.jpg",
                Price = 5.99m,
                Rating = 5
            });

            db.Fruits.Add(new Fruit
            {
                Id = 6,
                Name = "Watermelon",
                Description = "Citrullus lanatus is a plant species in the family Cucurbitaceae, a vine-like (scrambler and trailer) flowering plant originating in West Africa. It is cultivated for its fruit.",
                Color = Color.Green,
                Image = "watermelon.jpg",
                Price = 9.99m,
                Rating = 2
            });

            db.SaveChanges();
        }

        #endregion
    }
}
