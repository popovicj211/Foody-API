namespace Application.Interfaces
{
    public interface IPasswordHashing : IDisposable
    {
        string HashPassword(string password, byte[] salt = null);
        string HashPassword(byte[] password, byte[] salt = null);
        bool ValidatePassword(string password, string hash);
        byte[] GenerateSalt();
    }
}
