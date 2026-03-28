using Microsoft.AspNetCore.Mvc;
using TetPee.Repository;
using TetPee.Repository.Entity;
using TetPee.Service.User;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class UserController:ControllerBase
{
    private readonly AppDbContext _dbContext;
    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
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
}
