namespace Shop.Components;

public class CommentViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(long entityId, string entityTypeId)
    {

        var model = new CommentVm
        {
            EntityId = entityId,
            EntityTypeId = entityTypeId
        };

        return View(model);
    }
}


