using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Components
{
    public class ProductByCategoryComponent : ViewComponent
    {
        private readonly shopContext _shopContext;
        public ProductByCategoryComponent(shopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int CategoryId,string Orderby)
        {
            var products = _shopContext.Product.Where(x=>x.CategoryId == CategoryId).Include(x=>x.Thumbnail).ToList();
            return View(products);
        }

    }
}
