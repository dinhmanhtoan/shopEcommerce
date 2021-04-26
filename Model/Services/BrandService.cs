using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public interface IBrandService
    {
        public Task<List<Brand>> GetAll();
        public void AddBrand(Brand entity);
        public Task UpdateBrand(Brand entity);
        public void Delete(long Id);
        public Brand getById(long Id);

    }
    public class BrandService : IBrandService
    {
        private shopContext _context;

        public BrandService(shopContext context)
        {
            _context = context;
        }
        public async Task<List<Brand>> GetAll()
        {
            var res = await _context.Brand.ToListAsync();
            return res;
        }

        public void AddBrand(Brand entity)
        {
            _context.Brand.Add(entity);
            _context.SaveChanges();
        }

        public async Task UpdateBrand(Brand entity)
        {
            try
            {
                var Brand = _context.Brand.AsNoTracking().FirstOrDefault(x => x.Id == entity.Id);
                if (Brand != null)
                {
                    Brand = entity;
                    _context.Brand.Update(Brand);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(long Id)
        {
            var Brand = _context.Brand.FirstOrDefault(x => x.Id == Id);
            if (Brand != null)
            {
                _context.Brand.Remove(Brand);
                _context.SaveChanges();
            }
        }
        public Brand getById(long Id)
        {
            var res = _context.Brand.FirstOrDefault(x => x.Id == Id);
            return res;
        }
    }
}

