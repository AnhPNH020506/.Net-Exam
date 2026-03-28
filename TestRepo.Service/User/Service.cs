using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.Service.User;

public class Service :IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Base.Response.PageResult<Response.GetAllUserResponse>> GetAllUser(string? SearchTerm, int PageSize, int PageIndex)
    {
        var query = _dbContext.Users.Where(x => true);
        if (SearchTerm != null)
        {
            query = query.Where(x => x.FirstName.Contains(SearchTerm)||
                                     x.LastName.Contains(SearchTerm)||
                                     x.Email.Contains(SearchTerm));
        }
        query = query.OrderBy(x => x.Email);
        query = query.Skip((PageIndex - 1) * PageSize).Take(PageSize);
        var selectQuery = query.Select(x => new Response.GetAllUserResponse()
        {
            UserId = x.Id,
            Email = x.Email,
            Password = x.Password,
            Role = x.Role,

        });
        var listResult = await selectQuery.ToListAsync();
        var totalItems = listResult.Count();

        var result = new Base.Response.PageResult<Response.GetAllUserResponse>()
        {
            Items = listResult,
            PageIndex = PageIndex,
            PageSize = PageSize,
            TotalItems = totalItems,
        };
        return result;
    }
}