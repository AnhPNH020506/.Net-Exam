using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestRepo.Api.Extensions;
using TetPee.Repository;
using TetPee.Repository.Entity;
using TetPee.Service.User;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class UserController:ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IService _userService;
    public UserController(AppDbContext dbContext,  IService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    [HttpPost("")]
    public IActionResult CreateUser(Request.CreateUserRequest request)
    {
        var user = new User
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = "User"

        };
        _dbContext.Add(user);
        _dbContext.SaveChanges();
        return Ok("Add User successful");
        
    }

    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpGet("")]
    public async Task<IActionResult> GetUsers(string? searchTerm, int pageSize = 10, int pageIndex = 1)
    {
        var result = await _userService.GetAllUser(searchTerm, pageSize, pageIndex);
        return Ok(result);
    }
}
