using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMHR_WEB.Models
{
    /// <summary>
    /// Constants : 상수들 집합체
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// UserSession : UserSession 상수 문자열
        /// </summary>
        public const string UserSession = "UserSession";
        /// <summary>
        /// Issure : Issuer 상수 문자열
        /// </summary>
        public const string Issuer = "https://local.amhr.kr";
        /// <summary>
        /// UserRoles : 사용자 권한 문자열 객체
        /// </summary>
        public static class UserRoles
        {
            public const string Admin = "ADM";
            public const string General = "GNL";
        }
    }
}