namespace Model.Services;
public class WidgetInstanceService : IWidgetInstanceService
{
    private IRepository<WidgetInstance> _widgetInstanceRepository;

    public WidgetInstanceService(IRepository<WidgetInstance> widgetInstanceRepository)
    {
        _widgetInstanceRepository = widgetInstanceRepository;
    }

    public IQueryable<WidgetInstance> GetPublished()
    {
        var now = DateTimeOffset.Now;
        var data = _widgetInstanceRepository.Query().Where(x =>
            x.PublishStart.HasValue && x.PublishStart < now
            && (!x.PublishEnd.HasValue || x.PublishEnd > now));
        return data;
    }
}
