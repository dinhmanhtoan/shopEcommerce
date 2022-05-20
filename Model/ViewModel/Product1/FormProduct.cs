namespace Model.ViewModel.Product1;
public class FormProduct
{
    public ProductVm Products { get; set; } = new ProductVm();
    public IFormFile ThumbnailImage { get; set; }
    public IList<IFormFile> ProductImages { get; set; } = new List<IFormFile>();
    public ProductMedia productMedia { get; set; }
    public IList<ProductOptionVm> ProductOptionVm { get; set; } = new List<ProductOptionVm>();
    //public IList<ProductOption> ProductOption { get; set; }

}

