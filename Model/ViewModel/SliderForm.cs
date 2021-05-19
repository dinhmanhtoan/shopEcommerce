using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModel
{
    public class SliderForm
    {
        public SliderVm Sliders { get; set; } = new SliderVm();
        public IFormFile ThumbnailImage { get; set; }
    }
}
