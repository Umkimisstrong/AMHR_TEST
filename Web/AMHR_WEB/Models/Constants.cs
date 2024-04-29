using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    public class Constants
    {
        public const string UserSession = "UserSession";
        public const string Issuer = "https://local.amhr.kr";
        public static class UserRoles
        {
            public const string Admin = "ADM";
            public const string General = "GNL";
        }
    }
}