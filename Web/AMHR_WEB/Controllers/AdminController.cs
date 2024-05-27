using AMHR_WEB.Models;
using Contract;
using Contract.ENUM;
using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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
        public const int START_NUMBER = 1;
        public const int ROW_COUNT = 10;
        
        /// <summary>
        /// CodeManagement : 코드 관리 목록 뷰 담당
        /// </summary>
        /// <param name="contract">Code Contract</param>
        /// <returns></returns>
        public ActionResult CodeManagement(CodeContract contract)
        {
            int REQUEST_PAGE_NUMBER = (contract.PAGE_NUMBER == 0 ? START_NUMBER - 1 : (contract.PAGE_NUMBER-1) * ROW_COUNT);
            int NOW_PAGE_NUMBER = contract.PAGE_NUMBER == 0 ? START_NUMBER : contract.PAGE_NUMBER;
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "CodeManagement";
            ViewBag.ADMIN_VIEW = CONTROLLER_NAME;

            CodeContract response = new CodeContract();
            CodeRepository repository = new CodeRepository();
            
            response = repository.SelectCodeEntityList(contract.SYS_CODE_ID, contract.DIV_CODE_ID, contract.CODE_ID, contract.CODE_NM, REQUEST_PAGE_NUMBER, ROW_COUNT);

            // 5       = 55 / 10
            int PAGE_COUNT = response.TOTAL_COUNT / ROW_COUNT;
            //         = 10 * 5 < 55 ? PAGE_COUNT+1
            PAGE_COUNT = (ROW_COUNT * PAGE_COUNT < response.TOTAL_COUNT) ? PAGE_COUNT + 1 : PAGE_COUNT;
            ViewBag.PAGE_COUNT = PAGE_COUNT;

            // START = 0, ELSE = 1, 2, 3, ...
            ViewBag.NOW_PAGE_NUMBER = NOW_PAGE_NUMBER;
            ViewBag.TOTAL_COUNT = response.TOTAL_COUNT;

            // 현재 액션명을 TempData 로 넘겨준다. Admin 사이드 Bar 에서 메뉴 Display 에 사용
            TempData["ACTION_NAME"] = RouteData.Values["Action"].ToString();

            return View(response); 
        }

        /// <summary>
        /// CodeSave_P : 코드 저장 팝업 뷰 담당
        /// </summary>
        /// <returns></returns>
        public ActionResult CodeSave_P(CodeContract contract)
        {
            CodeContract codeContract = new CodeContract();
            codeContract.CodeEntity = new CodeEntity();

            if (
                (!string.IsNullOrEmpty(contract.SYS_CODE_ID)
                  && !string.IsNullOrEmpty(contract.DIV_CODE_ID)
                  && !string.IsNullOrEmpty(contract.CODE_ID)
                )
               )
            {
                CodeRepository repository = new CodeRepository();
                codeContract.CodeEntity = repository.SelectCodeEntity(contract.SYS_CODE_ID, contract.DIV_CODE_ID, contract.CODE_ID);
                ViewBag.GENERAL_FLAG = EnumProperties.GeneralFlag.UPDATE;
            }
            else
            {
                codeContract.CodeEntity.USE_YN = "Y";
                ViewBag.GENERAL_FLAG = EnumProperties.GeneralFlag.CREATE;
            }
            return View(codeContract);
        }

        /// <summary>
        /// RequestSaveCode : 코드 저장 요청
        /// </summary>
        /// <param name="contract">Code Contract</param>
        /// <returns></returns>
        public JsonResult RequestSaveCode(CodeContract contract, EnumProperties.GeneralFlag generalFlag)
        {
            CodeRepository repository = new CodeRepository();
            contract.CodeEntity.CREATE_ID = UserSessionModel.USER_ID;
            string result = repository.SaveCodeEntity(contract.CodeEntity, generalFlag);
            return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// CheckCodeID : Code ID 중복체크
        /// </summary>
        /// <param name="contract">Code Contract</param>
        /// <returns></returns>
        public JsonResult CheckCodeID(CodeContract contract)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(contract.SYS_CODE_ID))
            {
                result = "EMPTY_SYS";
                return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(contract.DIV_CODE_ID))
            {
                result = "EMPTY_DIV";
                return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(contract.CODE_ID))
            {
                result = "EMPTY_CD";
                return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
            }

            CodeRepository repository = new CodeRepository();
            result = repository.CheckCodeID(contract.SYS_CODE_ID, contract.DIV_CODE_ID, contract.CODE_ID) ? "OK" : "NO";
            return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserManagement(UserContract contract)
        {
            return View(contract);
        }
    }
}