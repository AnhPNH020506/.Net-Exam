namespace TetPee.Service.Seller;

public class Request
{
    public class CreateSellerRequest()
    {
        public required string Email { get; set; } 
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string TaxCode { get; set; }
        public required string CompanyName { get; set; }
        public  required string CompanyAddress { get; set; }
    }
}