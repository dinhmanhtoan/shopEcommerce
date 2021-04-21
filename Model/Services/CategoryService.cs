using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAll();
        public void AddCategory(Category entity);
        public Task UpdateCategory(Category entity);
        public void Delete(long Id);
        public Category getById(long Id);

    }
    public class CategoryService : ICategoryService
    {
        private shopContext _context;

        public CategoryService(shopContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAll()
        {
            var res =  await _context.Category.Include(x => x.Thumbnail).ToListAsync();
            return res;
        }
     
        public  void AddCategory(Category entity)
        {
            _context.Category.Add(entity);
             _context.SaveChanges();
        }

        public async Task UpdateCategory(Category entity)
        {
            try
            {
                var Category = _context.Category.FirstOrDefault(x => x.Id == entity.Id);
                if (Category != null)
                {
                    Category = entity;
                    _context.Category.Update(Category);
                    _context.SaveChanges();
                }
            }catch(Exception e)
            {
                throw e;
            }
        }

        public void Delete(long Id)
        {
            var Category = _context.Category.FirstOrDefault(x => x.Id == Id);
            if (Category != null)
            {
                _context.Category.Remove(Category);
                _context.SaveChanges();
            }
        }
        public  Category getById(long Id)
        {
            var res =  _context.Category.Include(x=> x.Thumbnail).FirstOrDefault(x => x.Id == Id);
            return res;
        }
    }
}
