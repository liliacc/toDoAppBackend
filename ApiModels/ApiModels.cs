namespace toDoAppBackend.ApiModels
{
    public class ValidationResponse
    {
        public string Error { get; set; }
    }
    
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    public class RegisterResponse : ValidationResponse
    {
        public string Token { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    public class LoginResponse : ValidationResponse
    {
        public string Token { get; set; }
    }
}