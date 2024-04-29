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
        /// <summary>
        /// CONTROLLER_NAME : 전역변수 Controller Name
        /// </summary>
        public const string CONTROLLER_NAME = "Home";

        /// <summary>
        /// Index : 메인 뷰 담당
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            CodeRepository rep = new CodeRepository();
            
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "Index";
            return View();



        }

        /// <summary>
        /// About : 기본정보 뷰 담당
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "About";
            return View("Index");
        }

        /// <summary>
        /// Contract : 연락 / 장소 뷰 담당
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "Contact";
            return View();
        }
    }
}