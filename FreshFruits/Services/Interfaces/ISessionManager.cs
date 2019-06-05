using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFruits.Services.Interfaces
{
    public interface ISessionManager
    {
        ShoppingCart GetShoppingCart();
        void SaveShoppingCart(ShoppingCart cart);
    }
}
