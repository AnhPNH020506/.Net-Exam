namespace TetPee.Service.Category;

public class Request
{
    public class CreateCategoryrequest
    {
        public required string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}