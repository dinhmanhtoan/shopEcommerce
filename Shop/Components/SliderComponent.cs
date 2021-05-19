using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Services;
using Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//future
namespace Shop.Components
{
    public class SliderViewComponent : ViewComponent
    {

        private readonly shopContext _shopContext;
        private readonly IMediaService _mediaService;
        public SliderViewComponent(shopContext shopContext, IMediaService mediaService)
        {
            _shopContext = shopContext;
            _mediaService = mediaService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = _shopContext.Slider.OrderBy(x => x.Index).Where(x => x.IsPublisher == true).Include(x => x.Thumbnail).ToList();
            var SliderVms = new List<SliderVm>();

            foreach (var item in query)
            {
                var SliderVm = new SliderVm()
                {
                    Id = item.Id,
                    Index = item.Index,
                    ThumbnailImageUrl = item.Thumbnail.FileName,
                };
                SliderVms.Add(SliderVm);
            }
            return View(SliderVms);
        }
    }

}
