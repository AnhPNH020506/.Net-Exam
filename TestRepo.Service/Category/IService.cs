namespace TetPee.Service.Category;

public interface IService
{
    public Task<List<Response.GetCategoryresponse>>GetCategories();
    public Task<List<Response.GetCategoryresponse>> GetChildrenCategory(Guid parentId);
}