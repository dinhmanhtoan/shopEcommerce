namespace Model.ViewModel.Menu;
public class MenuItemVm
{
    public MenuItemVm()
    {
        ChildItems = new List<MenuItemVm>();
    }

    public long Id { get; set; }

    public string Name { get; set; }

    public string Link { get; set; }

    public MenuItemVm Parent { get; set; }

    public IList<MenuItemVm> ChildItems { get; set; }

    public void AddChildItem(MenuItemVm childItem)
    {
        childItem.Parent = this;
        ChildItems.Add(childItem);
    }
}

