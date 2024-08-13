using System;
using System.Security.Claims;
using System.Web.Mvc;
using AMHR_WEB.GlobalAttribute;
using AMHR_WEB.Models;
using Contract;
using Contract.ENUM;
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
        /// CONTROLLER_NAME : 전역변수 Controller Name
        /// </summary>
        public const string CONTROLLER_NAME = "Class";

        /// <summary>
        /// ClassView : Class 메인 뷰 담당 
        /// </summary>
        /// <returns></returns>
        [Authorize(ClaimType = ClaimTypes.Role, ClaimValue = Constants.UserRoles.Admin + "," + Constants.UserRoles.General)]
        public ActionResult ClassView()
        {
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "Class View";
            return View();
        }

        /// <summary>
        /// ClassReservation : Class 예약 뷰 담당
        /// </summary>
        /// <returns></returns>
        [Authorize(ClaimType = ClaimTypes.Role, ClaimValue = Constants.UserRoles.Admin + "," + Constants.UserRoles.General)]
        public ActionResult ClassReservation(ClassContract contract)
        {
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "Class Reservation";
            ViewBag.SELECT_CLASS_TIME = GlobalHelper.GetTimeTextValueItem(0);

            ClassRepository repository = new ClassRepository();
            

            ClassContract result = new ClassContract();
            result.CLASS_YMD = (contract.CLASS_YMD == null || string.IsNullOrEmpty(contract.CLASS_YMD))
                                ? DateTime.Now.ToString("yyyy-MM-dd")  // 넘어온 날짜값이 없으면 오늘날짜를 기준으로 달력을 세팅한다.
                                : (
                                        DateTime.Parse(contract.CLASS_YMD).Year == DateTime.Now.Year && DateTime.Parse(contract.CLASS_YMD).Month <= DateTime.Now.Month 
                                        ? DateTime.Now.ToString("yyyy-MM-dd")   // 넘어온 날짜가 있으나 금년, 금월 이전과 작거나 같으면 (과거날짜이면) 오늘날짜를 기준으로 달력을 세팅한다.(예약은 미래만)
                                        : contract.CLASS_YMD                    // 아닌 경우 넘어온 날짜를 기준으로 세팅한다.
                                   ) ;
            ViewBag.RSV_DATATABLE = repository.SelectClassRsvList(result.CLASS_YMD.Replace("-", "").Substring(0, 6));

            result.PRD_CODE = contract.PRD_CODE;

            return View(result);
        }
    }
}