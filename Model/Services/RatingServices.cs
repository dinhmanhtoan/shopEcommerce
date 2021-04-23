using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Services
{
    public interface IRatingServices
    {
        public void Add(Rating rating);
        public List<Rating> Get(long ProductId, int page, int size);
    }
    public class RatingServices  : IRatingServices
    {
        private shopContext _context;

        public RatingServices(shopContext context)
        {
            _context = context;
        }

        public void Add(Rating rating)
        {
            _context.Rating.Add(rating);
            _context.SaveChanges();
        }
        public List<Rating> Get(long ProductId,int page,int size)
        {
           return _context.Rating.Where(x => x.ProductId == ProductId).Skip(page*(page - 1)).Take(size).ToList();
        }
    }
}
