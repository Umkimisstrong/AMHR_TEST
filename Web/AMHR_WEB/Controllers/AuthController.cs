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

        public string SignIn(SignInViewModel vm, HttpContextBase context, string returnUrl = default(string))
        {
            string result = string.Empty;
            try
            {
                var userSession = Authenticate(vm);

                if (userSession != null)
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
                    AuthenticationManager(context).SignIn(new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddHours(1),
                    }
                    , identity);

                    //if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    //{
                    //    return Redirect(returnUrl);
                    //}
                    result = "OK";
                    return result;
                }
                else
                {
                    result = "NO";
                    return result;
                }
            }
            catch (AuthenticationException e)
            {
                return e.Message;
                //vm.ErrorMessage = e.Message;
            }


            
        }

        private UserSessionModel Authenticate(SignInViewModel vm)
        {
            string DisplayNM = string.Empty;
            if (vm != null)
            {
                UserRepository repository = new UserRepository();
                Encoding encoding = Encoding.UTF8;
                UserEntity entity = repository.LoginCheckUser(vm.User_ID, GlobalAttribute.GlobalCrypto.EncryptSHA512(vm.Password, encoding));
                if (entity != null && !string.IsNullOrEmpty(entity.USER_ID))
                {
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
                DisplayName = DisplayNM
            };

        }

        public void Signout(HttpContextBase context)
        {
            AuthenticationManager(context).SignOut (DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
            //return Redirect("/Home/Index");
        }

        private IAuthenticationManager AuthenticationManager(HttpContextBase context)
        {
             return context.GetOwinContext().Authentication;
        }

    }
}