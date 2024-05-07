using AMHR_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AuthorizeAttribute = AMHR_WEB.App_Filters.AuthorizeAttribute;

namespace AMHR_WEB.Controllers
{
    /// <summary>
    /// AdminController : Admin 관련 Buisness Logic 담당
    /// </summary>
    public class AdminController : BaseController
    {
        public const string CONTROLLER_NAME = "Admin";
        

        public ActionResult SetSystem()
        {
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "SetSystem";

            ViewBag.ADMIN_VIEW = CONTROLLER_NAME;

            return View(); 
        }


    }
}