using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAll();
        public Task<List<Product>> GetByCategory(long categoryId);
        public Task AddProduct(Product entity);
        public Task UpdateProduct(Product entity);
        public Task Delete(long Id);
        public Task<Product> getById(long Id);

    }
    public class ProductService : IProductService
    {
        private shopContext _context;

        public ProductService(shopContext context)
        {
            _context = context;
        }
        public  async Task<List<Product>> GetAll()
        {
            var res = await _context.Product.ToListAsync();
            return res;
        }
        public  Task<List<Product>> GetByCategory(long categoryId)
        {
            var res =  _context.Product.Where(x => x.CategoryId == categoryId).ToListAsync();
            return res;
        }

        public async Task AddProduct(Product entity)
        {
            _context.Product.Add(entity);
            _context.SaveChanges();
        }

        public async Task UpdateProduct(Product entity)
        {
            try
            {
                var product = _context.Product.FirstOrDefault(x => x.Id == entity.Id);
                if (product != null)
                {
                    product = entity;
                    _context.Product.Update(product);
                    _context.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public async Task Delete(long Id)
        {
            var product = _context.Product.FirstOrDefault(x => x.Id == Id);
            if (product != null)
            {
                _context.Product.Remove(product);
                _context.SaveChangesAsync();
            }
        }
        public Task<Product> getById(long Id)
        {
            var res = _context.Product.Include(x=>x.Thumbnail).FirstOrDefaultAsync(x => x.Id == Id);
            return res;
        }
    }
}
