namespace Model.ViewModel.Menu;
public class MenuForm
{
    public long Id { get; set; }
    [Required(ErrorMessage = "The {0} field is required.")]
    public string Name { get; set; }
    public bool IsPublished { get; set; }
    public IList<MenuItemForm> Items { get; set; } = new List<MenuItemForm>();
}

