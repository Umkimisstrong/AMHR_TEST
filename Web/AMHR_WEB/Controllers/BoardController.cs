using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB.Controllers
{
    public class BoardController : BaseController
    {
        /// <summary>
        /// CONTROLLER_NAME : 전역변수 Controller Name
        /// </summary>
        public const string CONTROLLER_NAME = "Board";
        public ActionResult BoardList(BoardContract contract)
        {
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "BoardList";

            return View();
        }
    }
}