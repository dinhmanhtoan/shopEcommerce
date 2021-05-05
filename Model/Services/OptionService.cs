using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public interface IOptionService
    {
        public Task<List<ProductOption>> GetAll();
        public void AddOption(ProductOption entity);
        public Task UpdateOption(ProductOption entity);
        public void Delete(long Id);
        public ProductOption getById(long Id);

    }
    public class OptionService : IOptionService
    {
        private shopContext _context;

        public OptionService(shopContext context)
        {
            _context = context;
        }
        public async Task<List<ProductOption>> GetAll()
        {
            var res = await _context.ProductOption.ToListAsync();
            return res;
        }

        public void AddOption(ProductOption entity)
        {
            _context.ProductOption.Add(entity);
            _context.SaveChanges();
        }
        
        public async Task UpdateOption(ProductOption entity)
        {
            try
            {
                var Option = _context.ProductOption.AsNoTracking().FirstOrDefault(x => x.Id == entity.Id);
                if (Option != null)
                {
                    Option = entity;
                   
                   
                    _context.ProductOption.Update(Option);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var entries = _context.ChangeTracker.Entries();
                foreach (var item in entries)
                {
                    Console.WriteLine("entries name :{0}", item.Entity.GetType().FullName);
                    Console.WriteLine("Status :{0}", item.State);
                }
                throw e;
            }
        }

        public void Delete(long Id)
        {
            var Option = _context.ProductOption.FirstOrDefault(x => x.Id == Id);
            if (Option != null)
            {
                _context.ProductOption.Remove(Option);
                _context.SaveChanges();
            }
        }
        public ProductOption getById(long Id)
        {
            var res = _context.ProductOption.FirstOrDefault(x => x.Id == Id);
            return res;
        }
    }
}


