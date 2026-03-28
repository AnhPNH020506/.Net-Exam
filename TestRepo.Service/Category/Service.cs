using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.Service.Category;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Response.GetCategoryresponse>> GetCategories()
    {
        var query = _dbContext.Categories.OrderBy(x => x.Name);
        var selectQuery = query.Select(x => new Response.GetCategoryresponse()
        {
            Id = x.Id,
            Name = x.Name,
            ParentId = x.ParentID,
        });
       
        var listResult = await selectQuery.ToListAsync();
        return listResult;
    }

    public async Task<List<Response.GetCategoryresponse>> GetChildrenCategory(Guid parentId)
    {
        var query = _dbContext.Categories.Where(x => x.ParentID == parentId);
        query = query.OrderBy(x => x.Name);
        var selectQuery = query.Select(x => new Response.GetCategoryresponse()
        {
            Id = x.Id,
            Name = x.Name,
            ParentId = x.ParentID,
        });
        var listResult = await selectQuery.ToListAsync();
        return listResult;
    }
}