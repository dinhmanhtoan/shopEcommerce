using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
   public class Slider
    {
        public long Id { get; set; }
        public int Index { get; set; }
        public long? ThumbnailId { get; set; }
        public Media Thumbnail { get; set; }
        public bool IsPublisher { get; set; }
    }
}
