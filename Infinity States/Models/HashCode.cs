using System;
using System.Text;
using System.Security.Cryptography;

namespace Infinity_States.Models;

public struct HashCode
{
    /// <summary>
    ///  Generate Hash Code, using default byte encoding.
    /// </summary>
    public string GenerateHashedPassword(string password)
    {
        var md5 = MD5.Create();
        var md5data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

        return Convert.ToBase64String(md5data);
    }
}
