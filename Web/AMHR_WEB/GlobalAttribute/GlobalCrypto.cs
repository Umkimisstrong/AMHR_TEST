using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AMHR_WEB.GlobalAttribute
{
    /// <summary>
    /// GlobalCrypto : 글로벌 암호 클래스
    /// </summary>
    public class GlobalCrypto
    {
        /// <summary>
        /// HashType : enum 형의 HashType
        /// </summary>
        public enum HashType { MD5, SHA256, SHA384, SHA512 }

        /// <summary>
        /// EncryptSHA512 : 암호화(SHA512) 메소드
        /// </summary>
        /// <param name="text">암호화 할 문자열</param>
        /// <param name="encoding">인코딩 ex) UTF-8</param>
        /// <returns></returns>
        public static string EncryptSHA512(string text, Encoding encoding)
        { 
            var sha = new System.Security.Cryptography.SHA512Managed();
            byte[] data = sha.ComputeHash(encoding.GetBytes(text));

            var sb = new StringBuilder();
            foreach (byte b in data) { sb.Append(b.ToString("x2")); }

            return sb.ToString();
        }

        /// <summary>
        /// IsSameHash : 같은 Hash 임을 비교하는 메소드
        /// </summary>
        /// <param name="text">비교할 문자열</param>
        /// <param name="oldHash">이전 Hash 값</param>
        /// <param name="type">enum 형의 HashType</param>
        /// <param name="encoding">인코딩 ex) UTF-8</param>
        /// <returns></returns>
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