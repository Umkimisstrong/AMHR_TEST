using AMHR_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult ErrorView(CustomErrorModel contract)
        {
            ViewBag.ERROR_STATUS = string.IsNullOrEmpty(contract.status) ? "500" : contract.status;
            ViewBag.ERROR_STATUS_TEXT = string.IsNullOrEmpty(contract.statusText) ? "Server Error" : contract.statusText;
            ViewBag.ERROR_RESPONSE_TEXT = string.IsNullOrEmpty(contract.responseText) ? "Sorry, There are problems in our System. Please Try later" : contract.responseText;
            return View();
        }
    }
}