using FreshFruits.Data;
using FreshFruits.Models;
using FreshFruits.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFruits.Repositories
{
    public class FruitRepository : IFruitRepository
    {
        private ApplicationDbContext _dbContext;

        public FruitRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Fruit> GetById(int id)
        {
            return await _dbContext.Fruits.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Fruit>> GetAll()
        {
            return await _dbContext.Fruits.ToListAsync();
        }

        public async Task Add(Fruit post)
        {
            await _dbContext.Fruits.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Fruit post)
        {
            if (await _dbContext.Fruits.FindAsync(post.Id) == null)
                throw new Exception("Item doesn't exist");

            _dbContext.Fruits.Update(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var fruit = await _dbContext.Fruits.FindAsync(id);
            _dbContext.Fruits.Remove(fruit);
            await _dbContext.SaveChangesAsync();
        }
    }
}
