using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contract;
using Entity;
using Repository;

namespace AMHR_WEB.Controllers
{
    public class ClassController : BaseController
    {
        public const string CONTROLLER_NAME = "Class";

        public ActionResult ClassView()
        {
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            
            return View();
        }
    }
}