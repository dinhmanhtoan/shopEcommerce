using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModel
{
    public class SliderVm
    {

        public long Id { get; set; }
        public int Index { get; set; }
        public bool IsPublisher { get; set; }

        public string ThumbnailImageUrl { get; set; }

        public ProductMediaVm ProductImages { get; set; }
    }
}
