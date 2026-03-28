using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestRepo.Api.Extensions;
using TetPee.Repository;
using TetPee.Repository.Entity;
using TetPee.Service.Seller;

namespace TestRepo.Api.Controller;
[Authorize(Policy = JwtExtensions.AdminPolicy)]
[ApiController]
[Route("[controller]")]
public class SellerController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IService _sellerService;

    public SellerController(AppDbContext dbContext, IService sellerService)
    {
        _dbContext = dbContext;
        _sellerService = sellerService;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreatSeller([FromBody] Request.CreateSellerRequest request)
    {
        var seller = await _sellerService.CreateSeller(request);
        return Ok(seller);
    }
}
