using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AMHR_WEB.App_Filters;
using AMHR_WEB.Models;
using Contract;
using Entity;
using Repository;
using AuthorizeAttribute = AMHR_WEB.App_Filters.AuthorizeAttribute;

namespace AMHR_WEB.Controllers
{
    /// <summary>
    /// ClassController : 클래스 관련 Buisness Logic 담당
    /// </summary>
    public class ClassController : BaseController
    {
        /// <summary>
        /// 전역변수 Controller Name
        /// </summary>
        public const string CONTROLLER_NAME = "Class";

        /// <summary>
        /// Class 메인화면 
        /// </summary>
        /// <returns></returns>
        [Authorize(ClaimType = ClaimTypes.Role, ClaimValue = Constants.UserRoles.Admin + "," + Constants.UserRoles.General)]
        public ActionResult ClassView()
        {
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            
            return View();
        }
    }
}