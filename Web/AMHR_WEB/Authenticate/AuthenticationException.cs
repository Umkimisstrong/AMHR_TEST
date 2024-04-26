using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Authenticate
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message)
        {
        
        }
    }
}