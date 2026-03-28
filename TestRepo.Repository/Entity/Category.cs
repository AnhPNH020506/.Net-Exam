using TetPee.Repository.Abstraction;

namespace TetPee.Repository.Entity;

public class Category : BaseEntity<Guid>, IAuditableEntity
{
    public Guid? ParentID {get; set;}
    public required string Name { get; set; }
    public Category? Parent { get; set; }
    public Guid Id { get; set; }
    public ICollection<Category> Children { get; set; } = new List<Category>();
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}