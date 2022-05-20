namespace Model.ViewModel.Menu;
public class MenuItemForm
{
    public long Id { get; set; }
    public long? EntityId { get; set; }
    public long? ParentId { get; set; }
    public string Name { get; set; }
    public string CustomLink { get; set; }
    public int DisplayOrder { get; set; }
}

