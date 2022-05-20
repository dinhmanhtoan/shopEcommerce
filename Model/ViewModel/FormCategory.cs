using Microsoft.AspNetCore.Http;

namespace Model.ViewModel;
public class FormCategory
{
    public CategoryVm Categorys { get; set; } = new CategoryVm();
    [Required(ErrorMessage = "the {0} field is required.")]
    public IFormFile ThumbnailImage { get; set; }
}

