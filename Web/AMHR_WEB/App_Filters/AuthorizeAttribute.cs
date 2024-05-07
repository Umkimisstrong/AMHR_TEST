using AMHR_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace AMHR_WEB.App_Filters
{

    /// <summary>
    /// AuthorizeAttribute : 권한 필터
    /// </summary>
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        /// <summary>
        /// ClaimType : 권한 검증 타입 
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// ClaimValue : 검증할 값 (값 , 값...)
        /// </summary>
        public string ClaimValue { get; set;}

        /// <summary>
        /// OnAuthorization : 특정 Action에 Annotation으로 선언 가능
        /// [Authorize(ClaimType=Role, ClaimType=Value +","+Value..)]
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
                TempDataDictionary keyValuePairs = new TempDataDictionary();
                keyValuePairs.Add("MESSAGE", "로그인 후 이용하여 주십시오.");
                
                filterContext.Result = new RedirectResult("/User/UserLogin");
                filterContext.Controller.TempData = keyValuePairs;
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
                TempDataDictionary keyValuePairs = new TempDataDictionary();
                keyValuePairs.Add("MESSAGE", "이용 권한이 없습니다. 관리자에게 문의하십시오.");
                filterContext.Result = new RedirectResult("/Home/Index");
                filterContext.Controller.TempData = keyValuePairs;
                return;
            }

        }
    }
}