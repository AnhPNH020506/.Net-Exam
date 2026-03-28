using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TetPee.Repository;
using TetPee.Repository.Entity;
using TetPee.Service.Category;
// using TetPee.Repository
namespace TestRepo.Api.Controller;
[ApiController]
[Route("[controller]")]
public class CategoryController:ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IService _CategoryService;
    public CategoryController(IService categoryService, AppDbContext dbContext)
    {
        _CategoryService = categoryService;
       _dbContext = dbContext;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _CategoryService.GetCategories();
        return Ok(result);
    }

    [HttpGet("{parentId}/children")]
    public async Task<IActionResult> GetChildrenCategory(Guid parentId)
    {
        var result = await _CategoryService.GetChildrenCategory(parentId);
        return Ok(result);
    }

    [HttpPost("")]
    public IActionResult CreateCategory([FromBody] Request.CreateCategoryrequest request)
    {
        var cate = new Category
        {
            ParentID = request.ParentId,
            Name = request.Name
        };
        _dbContext.Add(cate);
        _dbContext.SaveChanges();
        Console.WriteLine(request);
        return Ok("Add Category Successfully");
    }
    
}