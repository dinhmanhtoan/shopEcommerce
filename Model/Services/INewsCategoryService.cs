namespace Model.Services;
public interface INewsCategoryService
{
    public Task Create(NewsCategory category);
    public Task Update(NewsCategory category);
    public Task Delete(long Id);
    public Task Delete(NewsCategory category);
}
