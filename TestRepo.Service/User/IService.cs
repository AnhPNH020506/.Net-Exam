namespace TetPee.Service.User;

public interface IService
{
    public Task<Base.Response.PageResult<Response.GetAllUserResponse>> 
                                GetAllUser(string? SearchTerm, int PageSize, int PageIndex);
}