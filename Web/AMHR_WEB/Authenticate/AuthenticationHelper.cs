using AMHR_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AMHR_WEB.GlobalAttribute;

namespace AMHR_WEB.Authenticate
{
    /// <summary>
    /// 권한 검증 관련 Hepler 클래스
    /// </summary>
    internal class AuthenticationHelper
    {
        /// <summary>
        /// Claim 생성 List<Cliam> (ID / Name / Session / Role)
        /// </summary>
        /// <param name="userSessionModel"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        internal static List<Claim> CreateClaim(UserSessionModel userSessionModel, params string[] roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userSessionModel.UserId.ToString()),
                new Claim(ClaimTypes.Name, userSessionModel.DisplayName),
                new Claim(Constants.UserSession, userSessionModel.ToJson())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String, Constants.Issuer));
            }

            return claims;
        }
    }
}