using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public interface ISliderService
    {
        public Task<List<Slider>> GetAll();
        public void AddSlider(Slider entity);
        public Task UpdateSlider(Slider entity);
        public void Delete(long Id);
        public Slider getById(long Id);

    }
    public class SliderService : ISliderService
    {
        private shopContext _context;

        public SliderService(shopContext context)
        {
            _context = context;
        }
        public async Task<List<Slider>> GetAll()
        {
            var res = await _context.Slider.Include(x => x.Thumbnail).ToListAsync();
            return res;
        }

        public void AddSlider(Slider entity)
        {
            _context.Slider.Add(entity);
            _context.SaveChanges();
        }

        public async Task UpdateSlider(Slider entity)
        {
            try
            {
                var Slider = _context.Slider.FirstOrDefault(x => x.Id == entity.Id);
                if (Slider != null)
                {
                    Slider = entity;
                    _context.Slider.Update(Slider);
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
            var Slider = _context.Slider.FirstOrDefault(x => x.Id == Id);
            if (Slider != null)
            {
                _context.Slider.Remove(Slider);
                _context.SaveChanges();
            }
        }
        public Slider getById(long Id)
        {
            var res = _context.Slider.Include(x => x.Thumbnail).FirstOrDefault(x => x.Id == Id);
            return res;
        }
    }
}
