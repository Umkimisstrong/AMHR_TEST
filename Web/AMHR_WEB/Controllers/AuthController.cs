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

namespace AMHR_WEB.Controllers
{
    public class AuthController : BaseController
    {
        [HttpGet]
        public ActionResult SignIn()
        { 

            if(UserSessionModel != null) 
            {
                return Redirect("/Home/Index");
            }

            return View();

        }

        public ActionResult SignIn(SignInViewModel vm, string returnUrl = default(string))
        {
            try
            {
                var userSession = Authenticate(vm);

                if (userSession!= null)
                {
                    var identity = new ClaimsIdentity(
                                                            AuthenticationHelper.CreateClaim
                                                            (
                                                                userSession,
                                                                Models.Constants.UserRoles.Admin,
                                                                Models.Constants.UserRoles.General
                                                            ),
                                                            DefaultAuthenticationTypes.ApplicationCookie
                        );
                    AuthenticationManager.SignIn (new AuthenticationProperties()
                    { 
                        AllowRefresh = true,
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddHours(1),
                    }
                    , identity);

                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (AuthenticationException e)
            { 
                vm.ErrorMessage = e.Message;
            }

            return View();
        }

        private UserSessionModel Authenticate(SignInViewModel vm)
        {
            // DB Connection (ID / PW)
            if (vm.ErrorMessage != "ckacl0233@naver.com" || vm.Password != "")
            {
                throw new AuthenticationException("Login Failed. Incorrect email address or password");
            }

            return new UserSessionModel
            {
                UserId = Guid.NewGuid(),
                DisplayName = ""
            };
        }

        public ActionResult Signout()
        {
            AuthenticationManager.SignOut (DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
            return Redirect("/Home/Index");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}