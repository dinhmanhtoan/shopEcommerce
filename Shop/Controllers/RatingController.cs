using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Services;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingServices _ratingServices;
        public RatingController(IRatingServices ratingServices)
        {
            _ratingServices = ratingServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Add(RatingForm model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var rating = new Rating()
            {
                ProductId = model.ProductId,
                Email = model.Email,
                Name = model.Name,
                Content = model.Content,
                Scores = model.Scores,
                CreatedOn = DateTime.Now
            };
            _ratingServices.Add(rating);

            return RedirectToAction("Success");
        }
        [HttpGet("gui-danh-gia-thanh-cong")]
        public async Task<IActionResult> Success()
        {
            return View();
        }

    }
}
