using Application.DataTransfer;

namespace Application.Helpers
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public UserDTO User { get; set; }
    }
}
