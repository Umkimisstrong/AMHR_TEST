using AMHR_WEB.Models;
using Contract;
using Repository;
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
        public const int START_NUMBER = 0;
        public const int ROW_COUNT = 2;

        public ActionResult SetSystem(CodeContract contract)
        {
            int REQUEST_PAGE_NUMBER = contract.PAGE_NUMBER == 0 ? START_NUMBER : contract.PAGE_NUMBER * 2;
            int PAGE_COUNT = 0;
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "SetSystem";
            ViewBag.ADMIN_VIEW = CONTROLLER_NAME;

            CodeContract response = new CodeContract();
            CodeRepository repository = new CodeRepository();
            
            response = repository.SelectCodeEntityList(contract.SYS_CODE_ID, contract.DIV_CODE_ID, contract.CODE_ID, contract.CODE_NM, REQUEST_PAGE_NUMBER, ROW_COUNT);

            // 5       = 55 / 10
            PAGE_COUNT = response.TOTAL_COUNT / ROW_COUNT;
            //         = 10 * 5 < 55 ? PAGE_COUNT+1
            PAGE_COUNT = (ROW_COUNT * PAGE_COUNT < response.TOTAL_COUNT) ? PAGE_COUNT + 1 : PAGE_COUNT;
            ViewBag.PAGE_COUNT = PAGE_COUNT;

            // START = 0, ELSE = 1, 2, 3, ...
            ViewBag.NOW_PAGE_NUMBER = contract.PAGE_NUMBER+1;
            ViewBag.TOTAL_COUNT = response.TOTAL_COUNT;

            return View(response); 
        }


    }
}