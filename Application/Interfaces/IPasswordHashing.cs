using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPasswordHashing : IDisposable
    {
        // string Hash { get; }
        string HashPassword(string password, byte[] salt = null);
        string HashPassword(byte[] password, byte[] salt = null);
        bool ValidatePassword(string password, string hash);
        byte[] GenerateSalt();
    }
}
