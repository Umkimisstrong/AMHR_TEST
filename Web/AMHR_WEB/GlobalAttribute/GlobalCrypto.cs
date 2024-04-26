using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AMHR_WEB.GlobalAttribute
{
    public class GlobalCrypto
    {
        public enum HashType { MD5, SHA256, SHA384, SHA512 }

        public static string EncryptSHA512(string text, Encoding encoding)
        { 
            var sha = new System.Security.Cryptography.SHA512Managed();
            byte[] data = sha.ComputeHash(encoding.GetBytes(text));

            var sb = new StringBuilder();
            foreach (byte b in data) { sb.Append(b.ToString("x2")); }

            return sb.ToString();
        }

        public static bool IsSameHash(string text, string oldHash, HashType type, Encoding encoding)
        {
            string newHash = null;

            switch (type)
            {
                case HashType.SHA512:
                    newHash = EncryptSHA512(text, encoding);
                    break;
                default:
                    return false;
            }

            var comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(newHash, oldHash) == 0;
        }
    }
}