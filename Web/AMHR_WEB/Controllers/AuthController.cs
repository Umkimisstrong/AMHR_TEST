using AMHR_WEB.Authenticate;
using AMHR_WEB.Models;
using Contract;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AuthenticationException = AMHR_WEB.Authenticate.AuthenticationException;
using Microsoft.Owin.Host.SystemWeb;
using Repository;
using System.Diagnostics.Contracts;
using System.Text;
using Entity;

namespace AMHR_WEB.Controllers
{
    /// <summary>
    /// AuthController : 권한 검증 컨트롤러 담당
    /// </summary>
    public class AuthController : BaseController
    {
        /// <summary>
        /// SignIn : 안쓰이는 액션
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SignIn()
        { 

            if(UserSessionModel != null) 
            {
                return Redirect("/Home/Index");
            }

            return View();

        }

        /// <summary>
        /// SignIn : 사용자 로그인 시 검증및 Cookie 생성, UserSessionModel 을 생성하기 위한 메소드
        /// </summary>
        /// <param name="vm">SignInViewModel 인스턴스</param>
        /// <param name="context">Login 시 HttpContextBase 인스턴스</param>
        /// <param name="returnUrl">returnURL</param>
        /// <returns></returns>
        public string SignIn(SignInViewModel vm, HttpContextBase context, string returnUrl = default(string))
        {
            // 01. 반환할 문자열 선언
            string result = string.Empty;

            // 02. try ~ Catch
            try
            {
                // 03. 검증 후 UserSession 객체 반환
                var userSession = Authenticate(vm);

                // 04. 존재한다면(로그인 성공, UserSession 객체 생성 시에)
                if (userSession != null)
                {
                    // 05. Claim 생성
                    var identity = new ClaimsIdentity(
                                                            AuthenticationHelper.CreateClaim
                                                            (
                                                                userSession,
                                                                Models.Constants.UserRoles.Admin,
                                                                Models.Constants.UserRoles.General
                                                            ),
                                                            DefaultAuthenticationTypes.ApplicationCookie
                        );

                    // 06. 현재 HttpContext 에 SignIn(생성한 Claim Identity 를 입력)
                    AuthenticationManager(context).SignIn(new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddHours(1),
                    }
                    , identity);

                    // 07. 로그인 성공 문자열 OK 반환
                    result = "OK";
                    return result;
                }
                else
                {
                    // 08. 로그인 실패 문자열 NO 반환
                    result = "NO";
                    return result;
                }
            }
            catch (AuthenticationException e)
            {
                // 09. 예외 시 오류문자열 반환
                return e.Message;
            }


            
        }

        /// <summary>
        /// Authenticate : 내부 DB 연계하여 로그인 검증
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        private UserSessionModel Authenticate(SignInViewModel vm)
        {
            string USER_ID = string.Empty;
            string USER_NM = string.Empty;
            string USER_EMAIL = string.Empty;
            string USER_TYPE = string.Empty;
            string USER_CREATE_TYPE = string.Empty;
            string DisplayNM = string.Empty;
            
            if (vm != null)
            {
                UserRepository repository = new UserRepository();
                Encoding encoding = Encoding.UTF8;
                UserEntity entity = repository.LoginCheckUser(vm.User_ID, GlobalAttribute.GlobalCrypto.EncryptSHA512(vm.Password, encoding));
                if (entity != null && !string.IsNullOrEmpty(entity.USER_ID))
                {
                    USER_ID = entity.USER_ID;
                    USER_NM = entity.USER_NM;
                    USER_EMAIL = entity.USER_EMAIL;
                    USER_TYPE = entity.USER_TYPE;
                    USER_CREATE_TYPE = entity.USER_CREATE_TYPE;
                    DisplayNM = entity.USER_NM;

                }
                else
                {
                    return null;
                    //throw new AuthenticationException("Login Failed. Incorrect email address or password");
                }
            }


            return new UserSessionModel
            {
                UserId = Guid.NewGuid(),
                DisplayName = DisplayNM,

                USER_ID = USER_ID,
                USER_NM = USER_NM,
                USER_EMAIL = USER_EMAIL,
                USER_TYPE = USER_TYPE,
                USER_CREATE_TYPE= USER_CREATE_TYPE,

            };

        }

        /// <summary>
        /// Signout : 로그아웃 구현
        /// </summary>
        /// <param name="context"></param>
        public void Signout(HttpContextBase context)
        {
            AuthenticationManager(context).SignOut (DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
            //return Redirect("/Home/Index");
        }

        /// <summary>
        /// AuthenticationManager : 현재 OwinContext의 인스턴스 반환
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private IAuthenticationManager AuthenticationManager(HttpContextBase context)
        {
             return context.GetOwinContext().Authentication;
        }

    }
}