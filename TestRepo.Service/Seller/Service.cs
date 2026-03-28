using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.Service.Seller;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreateSeller(Request.CreateSellerRequest request)
    {
        var existUserQuery = _dbContext.Users.Where(x => x.Email == request.Email);
        bool isExistUser = await existUserQuery.AnyAsync();
        if (isExistUser)
        {
            throw new Exception("User already exists");
        }

        var user = new Repository.Entity.User()
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = "Seller"
        };
        _dbContext.Add(user);
        var result = await _dbContext.SaveChangesAsync();
        if (result > 0)
        {
            var seller = new Repository.Entity.Seller
            {
                TaxCode = request.TaxCode,
                CompanyName = request.CompanyName,
                CompanyAddress = request.CompanyAddress,
                UserId =  user.Id,
            };
            _dbContext.Add(seller);
            var sellerResult = await _dbContext.SaveChangesAsync();
            if (sellerResult > 0)
            {
                return "Add Seller successful";
            }
            return "Add Seller failed";

        }
        return "Add Seller Failed";
    }
}