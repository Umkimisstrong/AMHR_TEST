using AMHR_WEB.Models;
using Contract;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        /// UserLogin : 로그인 뷰 담당
        /// </summary>
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
        /// <param name="contract">User 모델</param>
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
        /// <param name="contract">User 모델</param>
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
        /// <param name="contract">User 모델</param>
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
            //return Json(new { RESULT = result }, JsonRequestBehavior.AllowGet);
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
    }
}