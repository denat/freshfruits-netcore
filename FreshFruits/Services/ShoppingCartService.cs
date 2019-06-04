using FreshFruits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFruits.Services
{
    public class ShoppingCartService
    {
        private List<Fruit> _items = new List<Fruit>();
        private int _totalLimit = 5;

        public void Add(Fruit item)
        {
            if (_items.Count == _totalLimit)
                throw new Exception("Cart is full! Try removing something first...");

            _items.Add(item);
        }

        public void Remove(Fruit apple)
        {
            if (!_items.Contains(apple))
                throw new Exception("Item doesn't exist!");

            _items.Remove(apple);
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
