using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class FormCategory
    {
        public CategoryVm Categorys { get; set; } = new CategoryVm();
        public IFormFile ThumbnailImage { get; set; }
    }
}
