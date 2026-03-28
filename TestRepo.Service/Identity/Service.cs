using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TetPee.Repository;
using TetPee.Service.JwtService;

namespace TetPee.Service.Identity;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    private readonly JwtOptions _jwtOption = new();
    private readonly JwtService.IService _jwtService;

    public Service(AppDbContext dbContext, IConfiguration configuration, JwtService.IService jwtService)
    {
        _dbContext = dbContext;
        configuration.GetSection(nameof(JwtOptions)).Bind(_jwtOption);
        _jwtService = jwtService;
    }

    public async Task<Response.GetIdentityResponse> Login(string email, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email ==  email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (user.Password != password)
        {
            throw new Exception("Password do not match");
        }

        var claim = new List<Claim>
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("Email", user.Email),
            new Claim("Role", user.Role),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Expired, DateTimeOffset.UtcNow.AddMinutes(_jwtOption.ExpireMinutes).ToString())
        };
        var token = _jwtService.GenerateAccessToken(claim);
        var result = new Response.GetIdentityResponse()
        {
            AccessToken = token,
        };
        return result;
    }
}