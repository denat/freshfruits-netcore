﻿using FreshFruits.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFruits.Services
{
    public class SessionManager : ISessionManager
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public ShoppingCart GetShoppingCart()
        {
            ShoppingCart cart = _httpContextAccessor.HttpContext.Session.Get<ShoppingCart>("ShoppingCart");

            if (cart == null)
            {
                cart = new ShoppingCart();
                SaveShoppingCart(cart);
            }

            return cart;
        }

        public void SaveShoppingCart(ShoppingCart cart)
        { 
            _httpContextAccessor.HttpContext.Session.Set("ShoppingCart", cart);
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
