using Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Shop.Areas.Admin.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _SliderServices;
        private readonly IProductService _ProductServices;
        private readonly IMediaService _mediaService;
        private readonly shopContext _context;
        public SliderController(ISliderService SliderServices, IProductService ProductServices, IMediaService mediaService, shopContext context)
        {
            _SliderServices = SliderServices;
            _ProductServices = ProductServices;
            _mediaService = mediaService;
            _context = context;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var Slider = await _SliderServices.GetAll();
            return View(Slider);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderForm model)
        {
            if (model.Sliders.Index == -1)
            {
                ModelState.AddModelError("Sliders.Index", "Vui lòng Chọn Vị trí");
            }

            if (ModelState.IsValid)
            {
                var Slider = new Slider()
                {
                    Index = model.Sliders.Index,
                    IsPublisher = model.Sliders.IsPublisher,
                };
                await SaveSlider(model, Slider);
                _SliderServices.AddSlider(Slider);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet("Slider/Updated/{Id}")]
        public IActionResult Updated(long Id)
        {
            var Slider = _SliderServices.getById(Id);
            var SliderVm = new SliderVm()
            {
                Id = Slider.Id,
                Index = Slider.Index,
                IsPublisher = Slider.IsPublisher,
                ThumbnailImageUrl = Slider.Thumbnail != null ? Slider.Thumbnail.FileName : ""
            };
            var SliderForm = new SliderForm();
            SliderForm.Sliders = SliderVm;
            return View(SliderForm);
        }
        [HttpPost("Slider/Updated/{Id}")]
        public async Task<IActionResult> Updated(long Id, SliderForm model)
        {
            var Slider = _SliderServices.getById(Id);
            if (model.Sliders.Index == -1)
            {
                ModelState.AddModelError("Sliders.Index", "Vui lòng Chọn Vị trí");
            }
            if (ModelState.IsValid)
            {
          
                Slider.Id = Id;
                Slider.Index = model.Sliders.Index; // không bị tracking
                Slider.IsPublisher = model.Sliders.IsPublisher;
                await SaveSlider(model, Slider);
                await _SliderServices.UpdateSlider(Slider);
                return RedirectToAction("Index");
            }
            model.Sliders.ThumbnailImageUrl = Slider.Thumbnail.FileName;
            return View(model);
        }
        [HttpGet("Slider/Deleted/{Id}")]
        public IActionResult Deleted(long Id)
        {
            var Slider = _SliderServices.getById(Id);
            return View(Slider);
        }
        [HttpPost]
        public IActionResult DeletedById(long Id)
        {
            var thumnail = _context.Slider.Include(x => x.Thumbnail).FirstOrDefault(x => x.Id == Id);
            if (thumnail != null)
            {
                _context.Medias.Remove(thumnail.Thumbnail);
                _SliderServices.Delete(Id);
            }

            return RedirectToAction("Index");
        }
        public async Task SaveSlider(SliderForm model, Slider Slider)
        {
            if (model.ThumbnailImage != null)
            {
                var fileName = await SaveFile(model.ThumbnailImage);
                if (Slider.Thumbnail != null)
                {
                    RemoveFile(Slider.Thumbnail.FileName);
                    Slider.Thumbnail.FileName = fileName;
                }
                else
                {
                    Slider.Thumbnail = new Media { FileName = fileName };
                }

            }
        }
        private void RemoveFile(string file)
        {
            _mediaService.DeleteMediaAsync(file);
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _mediaService.SaveMediaAsync(file.OpenReadStream(), fileName, file.ContentType);
            return fileName;
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
