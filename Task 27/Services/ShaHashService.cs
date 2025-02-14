using System.Security.Cryptography;
using System.Text;
using Task_27.Interfaces;

namespace Task_27.Services;

internal class ShaHashService : IHashService
{
    public string Hash(string infoToHash)
    {
        if (infoToHash == null)
            throw new ArgumentException(nameof(infoToHash));
        
        byte[] bytes = SHA1.HashData(Encoding.UTF8.GetBytes(infoToHash));
        string hashedString = Encoding.UTF8.GetString(bytes);
        
        return hashedString;
    }
}