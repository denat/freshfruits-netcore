using FreshFruits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFruits.Repositories.Interfaces
{
    public interface IFruitRepository
    {
        Task<Fruit> GetById(int id);
        Task<IEnumerable<Fruit>> GetAll();
        Task Add(Fruit post);
        Task Update(Fruit post);
        Task DeleteById(int id);
    }
}
