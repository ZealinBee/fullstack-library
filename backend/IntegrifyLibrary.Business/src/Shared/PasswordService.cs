using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IntegrifyLibrary.Business;

public class PasswordService
{
    public static void HashPassword(string originalPassword, out string hashedPassword, out byte[] salt)
    {
        var hmac = new HMACSHA256();
        salt = hmac.Key;
        hashedPassword = Encoding.UTF8.GetString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)));
    }

    public static bool VerifyPassword(string originalPassword, string hashedPassword, byte[] salt)
    {
        var hmac = new HMACSHA256(salt); // salt is basically the key here
        var hashedOriginal = Encoding.UTF8.GetString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)));
        return hashedOriginal == hashedPassword;
    }

}