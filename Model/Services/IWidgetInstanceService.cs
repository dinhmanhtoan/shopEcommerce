namespace Model.Services;

public interface IWidgetInstanceService
{
    IQueryable<WidgetInstance> GetPublished();
}

