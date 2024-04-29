using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Authenticate
{
    /// <summary>
    /// AuthenticationException : 커스텀 예외 처리 클래스
    /// </summary>
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message)
        {
        
        }
    }
}