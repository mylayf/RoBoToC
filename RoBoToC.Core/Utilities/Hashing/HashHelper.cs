using System;
using System.Collections.Generic;
using System.Text;

namespace RoBoToC.Core.Utilities.Hashing
{
    public class HashHelper
    {
        public static void GenerateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public static bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                hmac.Key = passwordSalt;
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (computedHash.Length != passwordHash.Length)
                {
                    return false;
                }
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
