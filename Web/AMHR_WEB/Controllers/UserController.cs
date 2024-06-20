using AMHR_WEB.GlobalAttribute;
using AMHR_WEB.Models;
using Contract;
using Contract.ENUM;
using Repository;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB.Controllers
{


    /// <summary>
    /// UserController : User 관련 Buisness Logic 담당
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// CONTROLLER_NAME : 전역변수 Controller Name
        /// </summary>
        public const string CONTROLLER_NAME = "User";

        /// <summary>
        /// UserSignup : 회원가입 뷰 담당
        /// </summary>
        /// <returns></returns>
        public ActionResult UserSignup()
        {
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "SignUp";
            return View();
        }
       /// <summary>
       /// UserLogin : 사용자 로그인 뷰 담당
       /// </summary>
       /// <param name="message">메시지</param>
       /// <returns></returns>
        public ActionResult UserLogin(string message = "")
        {
            var x = TempData;
            ViewBag.MESSAGE = x["MESSAGE"] != null ? x["MESSAGE"].ToString() : "";
            ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
            ViewBag.SECOND_BREADCRUMB_NAME = "Login";
            return View();
        }

        /// <summary>
        /// CreateUser : User 생성
        /// </summary>
        /// <param name="contract">사용자 Contract</param>
        /// <returns></returns>
        public JsonResult CreateUser(UserContract contract)
        {
            UserRepository repository = new UserRepository();
            Encoding encoding = Encoding.UTF8;
            contract.UserEntity.USER_PWD = GlobalAttribute.GlobalCrypto.EncryptSHA512(contract.UserEntity.USER_PWD, encoding);
            contract.UserEntity.USER_CREATE_TYPE = "HOME";
            bool result = repository.CreateUser(contract);

            return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// CheckUserID : User ID 중복체크
        /// </summary>
        /// <param name="userID">사용자 ID</param>
        /// <returns></returns>
        public JsonResult CheckUserID(string userID)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(userID))
            {
                result = "EMPTY";
                return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
            }
            UserRepository repository = new UserRepository();
            result = repository.CheckUserID(userID) ? "OK" : "NO";
            return Json(new { RESULT = result}, JsonRequestBehavior.AllowGet);
        }

        
        /// <summary>
        /// LoginCheckUser : ID, PWD 로 Login Check
        /// </summary>
        /// <param name="contract">사용자 Contract</param>
        /// <param name="returnUrl">returnUrl</param>
        /// <returns></returns>
        public JsonResult LoginCheckUser(UserContract contract, string returnUrl = "")
        {
            UserRepository repository = new UserRepository();
            Encoding encoding = Encoding.UTF8;

            // 01. AuthController - SignIn
            AuthController authController = new AuthController();
            SignInViewModel vm = new SignInViewModel();
            vm.User_ID = contract.UserEntity.USER_ID;
            vm.Password = contract.UserEntity.USER_PWD;
            HttpContextBase context = this.HttpContext;
            string result  = authController.SignIn(vm, context, returnUrl);
            //bool result = repository.LoginCheckUser(contract.UserEntity.USER_ID, GlobalAttribute.GlobalCrypto.EncryptSHA512(contract.UserEntity.USER_PWD, encoding));
            return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// LoginCheckUser : ID, PWD 로 Login Check
        /// </summary>
        /// <param name="userID">사용자 ID</param>
        /// <param name="userPWD">사용자 PWD</param>
        /// <param name="context">HttpContextBase</param>
        /// <returns></returns>
        public string SNSLoginCheckUser(string userID, string userPWD, HttpContextBase context)
        {
            UserRepository repository = new UserRepository();
            Encoding encoding = Encoding.UTF8;

            // 01. AuthController - SignIn
            AuthController authController = new AuthController();
            SignInViewModel vm = new SignInViewModel();
            vm.User_ID = userID;
            vm.Password = userPWD;
            string result = authController.SignIn(vm, context, "");
            return result;
        }


        /// <summary>
        /// LogoutUser : 로그아웃 액션
        /// </summary>
        /// <returns></returns>
        public ActionResult LogoutUser() 
        {
            AuthController authController = new AuthController();

            HttpContextBase context = this.HttpContext;

            authController.Signout(context);
            TempData.Add("MESSAGE", "로그아웃 되었습니다.");
            return RedirectToAction("UserLogin", "User");

        }

        /// <summary>
        /// LogoutNoMessage : 로그아웃 액션 : 메세지 없음
        /// </summary>
        /// <returns></returns>
        public ActionResult LogoutNoMessage()
        {
            AuthController authController = new AuthController();

            HttpContextBase context = this.HttpContext;

            authController.Signout(context);
            return RedirectToAction("UserLogin", "User");

        }

        /// <summary>
        /// MyPage : 마이페이지 뷰 담당
        /// </summary>
        /// <returns></returns>
        public ActionResult MyPage()
        {
            if (UserSessionModel != null && !string.IsNullOrEmpty(UserSessionModel.USER_ID))
            {
                ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
                ViewBag.SECOND_BREADCRUMB_NAME = "MyPage";

                ViewBag.MY_PAGE_VIEW = "MY_PAGE_VIEW";
                ViewBag.SELECT_LIST_USER_TYPE = UserSessionModel.USER_TYPE == EnumProperties.UserTypeFlag.ADM.ToString()
                                                    ? GlobalHelper.GetTextValueItem("USER", "USER_TYPE")
                                                    : GlobalHelper.GetTextValueItem("USER", "USER_TYPE").Where(w => !w.Value.Equals(EnumProperties.UserTypeFlag.ADM.ToString())).ToList();

                // 현재 액션명을 TempData 로 넘겨준다. User 사이드 Bar 에서 메뉴 Display 에 사용
                TempData["ACTION_NAME"] = RouteData.Values["Action"].ToString();

                UserRepository repository = new UserRepository();
                UserContract contract = new UserContract();
                contract.UserEntity = repository.SelectUserEntity(UserSessionModel.USER_ID, UserSessionModel.USER_EMAIL, UserSessionModel.USER_CREATE_TYPE);

                return View(contract);
            }
            else
            {
                TempData.Add("MESSAGE", "마이페이지는 로그인 후 이용 가능합니다.");
                return RedirectToAction("UserLogin", "User");
            }
        }

        /// <summary>
        /// UserPasswordChange : 비밀번호 변경 뷰 담당
        /// </summary>
        /// <returns></returns>
        public ActionResult UserPasswordChange()
        {
            if (UserSessionModel != null && !string.IsNullOrEmpty(UserSessionModel.USER_ID))
            {
                ViewBag.MY_PAGE_VIEW = "MY_PAGE_VIEW";
                ViewBag.USER_ID = UserSessionModel.USER_ID;

                ViewBag.FIRST_BREADCRUMB_NAME = CONTROLLER_NAME;
                ViewBag.SECOND_BREADCRUMB_NAME = "Change Password";

                // 현재 액션명을 TempData 로 넘겨준다. User 사이드 Bar 에서 메뉴 Display 에 사용
                TempData["ACTION_NAME"] = RouteData.Values["Action"].ToString();
                return View();
            }
            else
            {
                TempData.Add("MESSAGE", "비밀번호 변경은 로그인 후 이용 가능합니다.");
                return RedirectToAction("UserLogin", "User");
            }

        }

        /// <summary>
        /// ChangePassword : 비밀번호 변경 요청
        /// </summary>
        /// <param name="userId">사용자 ID</param>
        /// <param name="userPwd">사용자 PWD</param>
        /// <returns></returns>
        public ActionResult ChangePassword(string userId, string userPwd)
        {
            if (UserSessionModel != null && !string.IsNullOrEmpty(UserSessionModel.USER_ID))
            {
                UserRepository repository = new UserRepository();


                Encoding encoding = Encoding.UTF8;
                string encryptedPwd =  GlobalAttribute.GlobalCrypto.EncryptSHA512(userPwd, encoding);
                bool result = repository.UserChangePassword(userId, UserSessionModel.USER_EMAIL, UserSessionModel.USER_CREATE_TYPE, encryptedPwd, UserSessionModel.USER_ID);
                return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                TempData.Add("MESSAGE", "비밀번호 변경은 로그인 후 이용 가능합니다.");
                return RedirectToAction("UserLogin", "User");
            }
        }


        /// <summary>
        /// UpdateUser : 사용자 정보 수정
        /// </summary>
        /// <param name="contract">사용자 Contract</param>
        /// <returns></returns>
        public ActionResult UpdateUser(UserContract contract)
        {
            
            if (UserSessionModel != null && !string.IsNullOrEmpty(UserSessionModel.USER_ID))
            {
                UserRepository repository = new UserRepository();
                bool result = false;

                contract.UserEntity.DEL_YN = "N";
                contract.UserEntity.USER_ID = UserSessionModel.USER_ID;
                result = repository.UpdateUser(contract);
                return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                TempData.Add("MESSAGE", "사용자 정보 수정은 로그인 후 이용 가능합니다.");
                return RedirectToAction("UserLogin", "User");
            }
        }

        
    }
}