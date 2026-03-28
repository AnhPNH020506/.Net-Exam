using TetPee.Repository.Abstraction;

namespace TetPee.Repository.Entity;

public class User: BaseEntity<Guid>, IAuditableEntity
{
    public required string Email { get; set; }
    public required  string Password { get; set; }
    public required  string FirstName { get; set; }
    public required  string LastName { get; set; }
    public required string Role { get; set; }
    public Seller Seller { get; set; }
    public DateTimeOffset  CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
}