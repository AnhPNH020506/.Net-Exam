namespace TetPee.Service.User;

public class Response
{
    public class GetAllUserResponse()
    {
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}