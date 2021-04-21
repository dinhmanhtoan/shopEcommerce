using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Category
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public long? ThumbnailId { get; set; }
        public Media Thumbnail { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditOn { get; set; }
        public long? EditBy { get; set; }

   
    }
}
