using System;
using System.Security.Claims;
using System.Web.Mvc;
using AMHR_WEB.GlobalAttribute;
using AMHR_WEB.Models;
using Contract;
using Contract.ENUM;
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

            ProductRepository productRepository = new ProductRepository();
            ProductEntity productEntity = productRepository.SelectProductEntity(contract.PRD_CODE);

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
            ViewBag.CLASS_YM = result.CLASS_YMD.Replace("-", "").Substring(0, 6);

            result.PRD_CODE = contract.PRD_CODE;
            result.PRD_TYPE_NM = productEntity.PRD_TYPE_NM;
            result.PRD_PRICE = productEntity.PRD_PRICE;

            return View(result);
        }

        /// <summary>
        /// ClassReservationSave_P : 클래스 예약 저장 팝업 담당 뷰
        /// </summary>
        /// <param name="contract">클래스 Contract</param>
        /// <returns></returns>
        public ActionResult ClassReservationSave_P(ClassContract contract)
        {
            ClassContract classContract = new ClassContract();
            ClassRepository repository = new ClassRepository();

            classContract.ClassEntity = new ClassEntity();
            

            if (
                !string.IsNullOrEmpty(contract.CLASS_NO)

               )
            {
                
                //classContract.ClassEntity = repository.SelectProductEntity(contract.PRD_CODE);
                ViewBag.GENERAL_FLAG = EnumProperties.GeneralFlag.UPDATE;
            }
            else
            {
                classContract.ClassEntity.USE_YN = "Y";
                classContract.ClassEntity.PRD_CODE = contract.PRD_CODE;
                classContract.ClassEntity.PRD_TYPE_NM = contract.PRD_TYPE_NM;
                classContract.ClassEntity.PRD_PRICE = contract.PRD_PRICE;
                classContract.ClassEntity.CLASS_YMD = contract.CLASS_YMD;
                classContract.ClassEntity.CLASS_TIME = contract.CLASS_TIME;
                classContract.ClassEntity.CLASS_TIME_NM = repository.SelectClassTimeNm(contract.CLASS_TIME);
                ViewBag.GENERAL_FLAG = EnumProperties.GeneralFlag.CREATE;
            }
            return View(classContract);
        }



        /// <summary>
        /// RequestSaveClass : 클래스 예약 저장 요청
        /// </summary>
        /// <param name="contract">클래스 Contract</param>
        /// <param name="generalFlag">일반 플래그</param>
        /// <returns></returns>
        public JsonResult RequestSaveClass(ClassContract contract, EnumProperties.GeneralFlag generalFlag)
        {
            ClassRepository repository = new ClassRepository();
            contract.ClassEntity.CLASS_USER_ID  = UserSessionModel.USER_ID;
            contract.ClassEntity.CREATE_ID = UserSessionModel.USER_ID;
            contract.ClassEntity.UPDATE_ID = UserSessionModel.USER_ID;
            contract.ClassEntity.USE_YN = "Y";
            contract.ClassEntity.DEL_YN = "N";

            bool result = repository.CreateClassReservation(contract.ClassEntity);
            return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CheckClassRsvOk(ClassContract contract) 
        {
            ClassRepository repository = new ClassRepository();
            string result = repository.CheckClassRsvOK(contract.CLASS_YMD, contract.CLASS_TIME);
            return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
        }
    }
}