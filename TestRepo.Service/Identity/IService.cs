namespace TetPee.Service.Identity;

public interface IService
{
    public Task<Response.GetIdentityResponse> Login(string email, string password);
}