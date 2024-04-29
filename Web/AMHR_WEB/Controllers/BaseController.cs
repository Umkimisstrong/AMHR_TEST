using AMHR_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AMHR_WEB.GlobalAttribute;

namespace AMHR_WEB.Controllers
{
    /// <summary>
    /// BaseController : 모든 컨트롤러에서 상속받는 최상위 컨트롤러
    /// </summary>
    public class BaseController : Controller
    {
        
        /// <summary>
        /// Global 인스턴스 UserSessionModel
        /// </summary>
        protected internal UserSessionModel UserSessionModel { get; private set; }

        /// <summary>
        /// 상속받는 컨트롤러에서 사용하는 Initailize 메소드 
        /// </summary>
        /// <param name="requestContext">요청 Context</param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var user = User as ClaimsPrincipal;
            if (user != null) 
            {
                var claims = user.Claims.ToList();
                var sessionClaim = claims.FirstOrDefault(o => o.Type == Constants.UserSession);
                if(sessionClaim != null)
                {
                    UserSessionModel =  sessionClaim.Value.ToObject<UserSessionModel>();
                }
            }
        }


    }
}