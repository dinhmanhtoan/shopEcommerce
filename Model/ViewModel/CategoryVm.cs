namespace Model.ViewModel;
public class CategoryVm
{
    
    public long Id { get; set; }
    [Required(ErrorMessage = "the {0} field is required.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "the {0} field is required.")]
    public string Slug { get; set; }
    public string MetaTitle { get; set; }
    public string MetaKeywords { get; set; }
    public string MetaDescription { get; set; }
    public bool IncludeInMenu { get; set; }
    [Required(ErrorMessage = "the {0} field is required.")]
    public int DisplayOrder { get; set; }
    public bool IsPublished { get; set; }
    public bool IsDeleted { get; set; }
    public long? ParentId { get; set; }
    public Category Parent { get; set; }

    public IList<CategoryListItem> categoriesList = new List<CategoryListItem>();
    public long CountProduct { get; set; }
    public IFormFile ThumbnailImage { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public ProductMediaVm ProductImages { get; set; }

}

