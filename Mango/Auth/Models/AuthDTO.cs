namespace Auth.Models
{
    public class LoginDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class RegisterDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Phone { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
    }

    public class UserDTO
    {
        public required string Id { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Name { get; set; }
    }

    public class LoginResponseDTO
    {
        public UserDTO? User { get; set; }
        public required string Token { get; set; }
    }

}
