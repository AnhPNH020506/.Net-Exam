using Microsoft.EntityFrameworkCore;
using TetPee.Repository.Entity;

namespace TetPee.Repository;

public class AppDbContext : DbContext
{
    public static Guid categoryParentId1 = Guid.NewGuid();
    public static Guid categoryParentId2 = Guid.NewGuid();
    public static Guid userId1 = Guid.NewGuid();
    public static Guid userId2 = Guid.NewGuid();
    public static Guid sellerId1 = Guid.NewGuid();
    public static Guid productId1 = Guid.NewGuid();
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

     public DbSet<User> Users { get; set; }
     public DbSet<Seller> Sellers { get; set; }
     public DbSet<Product> Products { get; set; }
     public DbSet<Category> Categories { get; set; }


     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         modelBuilder.Entity<Category>(builder =>
         {
             builder.Property(x => x.Name).IsRequired();
             var category = new List<Category>()
             {
                 new()
                 {
                     Id = categoryParentId1,
                     Name = "Áo"
                 },
                 new()
                 {
                     Id = categoryParentId2,
                     Name = "Quần"
                 },
                 new()
                 {
                     Id = Guid.NewGuid(),
                     Name = "Áo 3 lo",
                     ParentID =  categoryParentId1
                 },
                 new()
                 {
                     Id = Guid.NewGuid(),
                     Name = "Quần xi dui",
                     ParentID =  categoryParentId2
                 },
             };
             builder.HasData(category);
         });
         modelBuilder.Entity<User>(builder =>
         {
             builder.HasOne(x => x.Seller)
                 .WithOne(u => u.User)
                 .HasForeignKey<Seller>(s => s.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
             var user = new List<User>()
             {
                 new()
                 {
                     Id = userId1,
                     Email = "Anhne@gmail.com",
                     FirstName = "Anhne",
                     LastName = "Anhne",
                     Password = "123456",
                     Role = "User"

                 },
                 new()
                 {
                     Id = userId2,
                     Email = "Anhne123@gmail.com",
                     FirstName = "Anhne",
                     LastName = "Anhne",
                     Password = "123456",
                     Role = "User"

                 },

             };
             builder.HasData(user);
         });
         modelBuilder.Entity<Seller>(builder =>
             {
                 var seller = new List<Seller>()
                 {
                     new()
                     {
                         Id = sellerId1,
                         TaxCode = "abc-123",
                         CompanyAddress = "Khu cnc",
                         CompanyName = "cnc",
                         UserId = userId1,

                     },
                 };
                 builder.HasData(seller);
             }
         );
         modelBuilder.Entity<Product>(builder =>
             {
                 builder.HasOne(x => x.Seller)
                     .WithMany(p => p.Products)
                     .HasForeignKey(x => x.SellerId)
                     .OnDelete(DeleteBehavior.Restrict);
                 var product = new List<Product>()
                 {
                     new()
                     {
                         Id = productId1,
                         Name = "Áo len nam",
                         Price = 200m,
                         SellerId = sellerId1,

                     },

                 };
                 builder.HasData(product);

             }
         );
     }
}