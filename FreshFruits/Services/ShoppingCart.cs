using FreshFruits.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFruits.Services
{
    public class ShoppingCart
    {
        [JsonProperty]
        private List<Fruit> _items = new List<Fruit>();

        [JsonProperty]
        private int _totalLimit = 5;

        public void Add(Fruit item)
        {
            if (item == null)
                throw new Exception("Invalid item");

            if (_items.Count == _totalLimit)
                throw new Exception("Cart is full! Try removing something first...");

            _items.Add(item);
        }

        public void Remove(Fruit item)
        {
            if (!_items.Any(x => x.Name == item.Name))
                throw new Exception("Item doesn't exist!");

            _items.Remove(_items.First(x => x.Name == item.Name));
        }

        public List<Fruit> GetItems()
        {
            return _items.ToList();
        }

        public decimal CalculateTotalPrice()
        {
            decimal price = 0;
            for (int i = 0; i < _items.Count; i++)
            {
                price += _items[i].Price;
            }
            return price;
        }

        public int Count()
        {
            return _items.Count;
        }
    }
}
