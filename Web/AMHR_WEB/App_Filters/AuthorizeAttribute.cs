using AMHR_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB.App_Filters
{

    /// <summary>
    /// 권한 필터
    /// </summary>
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set;}

        /// <summary>
        /// 특정 Action에 Annotation으로 선언 가능
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            // 현재 User 관련 정보를 가져온다.
            var principal = filterContext.RequestContext.HttpContext.User as ClaimsPrincipal;   

            // IsAuthenticated 되지 않았다면
            // 로그인 시킨다.
            if(!principal.Identity.IsAuthenticated) 
            {
                filterContext.Result = new RedirectResult("/User/UserLogin");
                return;
            }

            // IsAuthenticated 되었으나
            // Claim Type 과 Value와 Issuer 가 다르다면
            // 권한없음으로 보낸다.
            var claimValue = ClaimValue.Split(',');
            if (
                !(
                    principal.HasClaim(x => x.Type == ClaimType && claimValue.Any(v => v == x.Value) && x.Issuer == Constants.Issuer)
                  )
               )
            {
                filterContext.Result = new RedirectResult("/User/UserSignup");
            }

        }
    }
}