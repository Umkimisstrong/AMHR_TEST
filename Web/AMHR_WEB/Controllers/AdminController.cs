using AMHR_WEB.Models;
using Contract;
using Contract.ENUM;
using Entity;
using Repository;
using System.Web.Mvc;

namespace AMHR_WEB.Controllers
{
    /// <summary>
    /// AdminController : Admin 관련 Buisness Logic 담당
    /// </summary>
    public class AdminController : BaseController
    {
        /// <summary>
        /// CONTROLLER_NAME : 전역변수 Controller Name
        /// </summary>
        public const string CONTROLLER_NAME = "Admin";
        /// <summary>
        /// START_NUMBER : 페이징 관련 시작 번호
        /// </summary>
        public const int START_NUMBER = 1;
        /// <summary>
        /// ROW_COUNT : 페이징 관련 조회 행 수
        /// </summary>
        public const int ROW_COUNT = 10;

        /// <summary>
        /// CodeManagement : 코드 관리 목록 뷰 담당
        /// </summary>
        /// <param name="contract">코드 Contract</param>
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
        /// ProductManagement : 상품 관리 목록 뷰 담당
        /// </summary>
        /// <param name="contract">상품 Contract</param>
        /// <returns></returns>
        public ActionResult ProductManagement(ProductContract contract)
        {
            int REQUEST_PAGE_NUMBER = (contract.PAGE_NUMBER == 0 ? START_NUMBER - 1 : (contract.PAGE_NUMBER - 1) * ROW_COUNT);
            int NOW_PAGE_NUMBER = contract.PAGE_NUMBER == 0 ? START_NUMBER : contract.PAGE_NUMBER;
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "ProductManagement";
            ViewBag.ADMIN_VIEW = CONTROLLER_NAME;

            ProductContract response = new ProductContract();
            ProductRepository repository = new ProductRepository();

            response = repository.SelectProductEntityList(contract.PRD_CODE, contract.PRD_CODE_NM, contract.PRD_TYPE_CODE, contract.PRD_TYPE_NM, REQUEST_PAGE_NUMBER, ROW_COUNT);

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
        /// CodeSave_P : 코드 저장 팝업 담당
        /// </summary>
        /// <param name="contract">코드 Contract</param>
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
        /// ProductSave_P : 상품 저장 담당
        /// </summary>
        /// <param name="contract">상품 Contract</param>
        /// <returns></returns>
        public ActionResult ProductSave_P(ProductContract contract)
        {
            ProductContract productContract = new ProductContract();
            productContract.ProductEntity = new ProductEntity();

            if(
                !string.IsNullOrEmpty(contract.PRD_CODE)
                
               )
            {
                ProductRepository repository = new ProductRepository();
                productContract.ProductEntity = repository.SelectProductEntity(contract.PRD_CODE);
                ViewBag.GENERAL_FLAG = EnumProperties.GeneralFlag.UPDATE;
            }
            else
            {
                productContract.ProductEntity.USE_YN = "Y";
                ViewBag.GENERAL_FLAG = EnumProperties.GeneralFlag.CREATE;
            }
            return View(productContract);
        }

        /// <summary>
        /// RequestSaveProduct : 상품 저장 요청
        /// </summary>
        /// <param name="contract">상품 Contract</param>
        /// <param name="generalFlag">일반 플래그</param>
        /// <returns></returns>
        public JsonResult RequestSaveProduct(ProductContract contract, EnumProperties.GeneralFlag generalFlag)
        {
            ProductRepository repository = new ProductRepository();
            contract.ProductEntity.CREATE_ID = UserSessionModel.USER_ID;
            contract.ProductEntity.UPDATE_ID = UserSessionModel.USER_ID;

            string result = repository.SaveProductEntity(contract.ProductEntity, generalFlag);
            return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// RequestSaveCode : 코드 저장 요청
        /// </summary>
        /// <param name="contract">코드 Contract</param>
        /// <param name="generalFlag">일반 플래그</param>
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
        /// <param name="contract">코드 Contract</param>
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

        /// <summary>
        /// CheckProductCode : 상품 코드 중복체크
        /// </summary>
        /// <param name="contract">상품 Contract</param>
        /// <returns></returns>
        public JsonResult CheckProductCode(ProductContract contract)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(contract.PRD_CODE))
            {
                result = "EMPTY_PRD";
                return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
            }
            
            

            ProductRepository repository = new ProductRepository();
            result = repository.CheckPrdCode(contract.PRD_CODE) ? "OK" : "NO";
            return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// UserManagement : 사용자 관리 뷰 담당
        /// </summary>
        /// <param name="contract">사용자 Contract</param>
        /// <returns></returns>
        public ActionResult UserManagement(UserContract contract)
        {
            int REQUEST_PAGE_NUMBER = (contract.PAGE_NUMBER == 0 ? START_NUMBER - 1 : (contract.PAGE_NUMBER - 1) * ROW_COUNT);
            int NOW_PAGE_NUMBER = contract.PAGE_NUMBER == 0 ? START_NUMBER : contract.PAGE_NUMBER;
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "UserManagement";
            ViewBag.ADMIN_VIEW = CONTROLLER_NAME;

            UserContract response = new UserContract();

            UserRepository repository = new UserRepository();

            response = repository.SelectUserEntityList(contract.USER_ID, contract.USER_NM, contract.USER_TYPE, contract.USER_CREATE_TYPE, REQUEST_PAGE_NUMBER, ROW_COUNT);

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
    }
}