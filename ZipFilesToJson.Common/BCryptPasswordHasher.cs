using System;
using System.Collections.Generic;
using System.Text;
using BCrypt;

namespace ZipFilesToJson.Common
{
    public static class BCryptPasswordHasher
    {
        public static string HashPassword(string password)
        {
            string salt = BCryptHelper.GenerateSalt(6);
            return BCryptHelper.HashPassword(password, salt);
            
        }

        public static bool VerifyPassword(string password, string passwordHash)
        {
            return BCryptHelper.CheckPassword(password, passwordHash);
        }
    }
}
