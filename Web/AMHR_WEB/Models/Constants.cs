using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    internal class Constants
    {
        internal const string UserSession = "UserSession";
        internal const string Issuer = "https://local.amhr.kr";
        internal static class UserRoles
        {
            internal const string Admin = "ADM";
            internal const string General = "GNL";
        }
    }
}