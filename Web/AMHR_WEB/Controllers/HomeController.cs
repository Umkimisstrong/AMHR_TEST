using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contract;
using Entity;
using Repository;

namespace AMHR_WEB.Controllers
{
    /// <summary>
    /// HomeController : Home 관련 Buisness Logic 담당
    /// </summary>
    public class HomeController : BaseController
    {
        public const string CONTROLLER_NAME = "Home";
        public ActionResult Index()
        {
            CodeRepository rep = new CodeRepository();
            //테스트
            //CodeContract contract =  rep.SelectCode("MASTER", "MASTER");
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "Index";
            return View();



        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "About";
            return View("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "Contact";
            return View();
        }
    }
}