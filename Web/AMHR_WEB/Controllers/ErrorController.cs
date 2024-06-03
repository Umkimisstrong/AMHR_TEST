using AMHR_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB.Controllers
{
    /// <summary>
    /// ErrorController : 에러 컨트롤러, 에러 발생 시 호출되는 컨트롤러 담당
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// ErrorView : 에러 발생 시 호출되는 뷰 담당
        /// </summary>
        /// <param name="contract">CustormErrorModel</param>
        /// <returns></returns>
        public ActionResult ErrorView(CustomErrorModel contract)
        {
            ViewBag.ERROR_STATUS = string.IsNullOrEmpty(contract.status) ? "500" : contract.status;
            ViewBag.ERROR_STATUS_TEXT = string.IsNullOrEmpty(contract.statusText) ? "Server Error" : contract.statusText;
            ViewBag.ERROR_RESPONSE_TEXT = string.IsNullOrEmpty(contract.responseText) ? "Sorry, There are problems in our System. Please Try later" : contract.responseText;
            return View();
        }
    }
}