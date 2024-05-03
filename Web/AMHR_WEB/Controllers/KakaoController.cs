using AMHR_WEB.GlobalAttribute;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using static System.Net.WebRequestMethods;
using Contract;
using Contract.ENUM;
using Entity;
using System.Security.Cryptography;

namespace AMHR_WEB.Controllers
{
    /// <summary>
    /// KakaoController : Kakao API 관련 Buisness Logic 담당
    /// </summary>
    public class KakaoController : Controller
    {
        public KakaoController()
        {
            GlobalKakao.SetGlobalKakao();
        }
        public ActionResult KakaoLogin()
        {
            string url = GlobalKakao.CODE_URI + "?"
                         + "client_id=" + GlobalKakao.CLIENT_ID
                         + "&redirect_uri=" + GlobalKakao.LOGIN_CALL_BACK_URI
                         + "&response_type=code";

            return Redirect(url);
        }

        /// <summary>
        /// KakaoLoginCallBack : 카카오 로그인 이후 콜백 액션
        /// </summary>
        /// <param name="code">생성되는 코드</param>
        /// <returns></returns>
        public ActionResult KakaoLoginCallBack(string code)
        {
            string accessToken = "";
            string refreshToken = "";

            GlobalKakao.SetGlobalKakao();
            try
            {
                WebRequest request = WebRequest.Create(GlobalKakao.TOKEN_URI);
                request.Method = "POST";

                string postData = "grant_type=authorization_code" +
                                  "&client_id=" + GlobalKakao.CLIENT_ID + 
                                  "&redirect_uri=" + GlobalKakao.LOGIN_CALL_BACK_URI + 
                                  "&client_secret=" + GlobalKakao.CLIENT_SECRET + 
                                  "&code=" + code;

                byte[] byteArray = Encoding.ASCII.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                WebResponse response = request.GetResponse();
                Console.WriteLine("responseCode : " + ((HttpWebResponse)response).StatusCode);

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    string responseFromServer = reader.ReadToEnd();
                    Console.WriteLine("response body : " + responseFromServer);

                    // Parse JSON response
                    dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseFromServer);
                    accessToken = jsonObject.access_token;
                    refreshToken = jsonObject.refresh_token;

                    Console.WriteLine("accessToken : " + accessToken);
                    Console.WriteLine("refreshToken : " + refreshToken);
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("WebException: " + e.Message);
            }

            Dictionary<string, object> resultOBJ = GetUserInfo(accessToken);

            bool checkUserID = false;
            if (resultOBJ != null && resultOBJ.Count > 0 && resultOBJ["USER_ID"] != null && !string.IsNullOrEmpty(resultOBJ["USER_ID"].ToString()))
            {
                checkUserID = CheckUserID(resultOBJ["USER_ID"].ToString());
            }

            bool checkUserEmail = false;
            if (resultOBJ != null && resultOBJ.Count > 0 && resultOBJ["USER_EMAIL"] != null && !string.IsNullOrEmpty(resultOBJ["USER_EMAIL"].ToString()))
            {
                checkUserEmail = 
                    EnumProperties.UserCreateTypeFlag.KAKAO.ToString().Equals
                    (
                            CheckUserCreateType
                            (
                                    resultOBJ["USER_EMAIL"].ToString()
                                ,   EnumProperties.UserCreateTypeFlag.KAKAO.ToString()
                            )
                    )
                   ?  false
                   :  true;
            }

            if (checkUserID && checkUserEmail)
            {
                UserEntity entity = new UserEntity();
                entity.USER_ID = resultOBJ["USER_ID"].ToString();
                entity.USER_NM = resultOBJ["USER_NM"].ToString();
                entity.USER_PWD = GlobalCrypto.EncryptSHA512(GlobalKakao.CONST_PWD, Encoding.UTF8).ToString();
                entity.USER_TYPE = EnumProperties.UserTypeFlag.GNL.ToString();
                entity.USER_EMAIL = resultOBJ["USER_EMAIL"].ToString();
                entity.USER_DESCRIPTION = "카카오 로그인";
                entity.USE_YN = "Y";
                entity.DEL_YN = "N";
                entity.USER_CREATE_TYPE = EnumProperties.UserCreateTypeFlag.KAKAO.ToString();
                entity.CREATE_ID = resultOBJ["USER_ID"].ToString();
                entity.UPDATE_ID = resultOBJ["USER_ID"].ToString();

                if (SaveKakaoUser(entity))
                {
                    UserController userController = new UserController();

                    string loginResult = userController.SNSLoginCheckUser(entity.USER_ID, GlobalKakao.CONST_PWD, this.HttpContext);
                    if (loginResult.Equals("OK"))
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        return Redirect("/User/UserLogin");
                    }
                }
                else
                {
                    return Redirect("/User/UserLogin");
                }
            }
            else
            {
                UserController userController = new UserController();
                string loginResult = userController.SNSLoginCheckUser(resultOBJ["USER_ID"].ToString(), GlobalKakao.CONST_PWD, this.HttpContext);
                if (loginResult.Equals("OK"))
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    return Redirect("/User/UserLogin");
                }
            }
        }


        /// <summary>
        /// GetUserInfo : 엑세스토큰으로 사용자 정보 가져오는 메서드
        /// </summary>
        /// <param name="accessToken">엑세스토큰</param>
        /// <returns></returns>
        public Dictionary<string, object> GetUserInfo(string accessToken)
        {
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            string postURL = "https://kapi.kakao.com/v2/user/me";

            try
            {
                WebRequest request = WebRequest.Create(postURL);
                request.Method = "POST";
                request.Headers.Add("Authorization", "Bearer " + accessToken);

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        Console.WriteLine("response body : " + responseFromServer);

                        JObject jsonResponse = JObject.Parse(responseFromServer);
                        JObject properties = (JObject)jsonResponse["properties"];
                        JObject kakaoAccount = (JObject)jsonResponse["kakao_account"];

                        string USER_ID = jsonResponse["id"].ToString();
                        string USER_NM = properties["nickname"].ToString();
                        string USER_EMAIL = string.Empty;
                        if (
                            (bool)kakaoAccount["has_email"] == true
                            &&
                            (bool)kakaoAccount["is_email_valid"] == true
                            &&
                            (bool)kakaoAccount["is_email_verified"] == true
                            )
                        { 
                            USER_EMAIL = kakaoAccount["email"].ToString();
                        }

                        userInfo.Add("USER_ID", USER_ID);
                        userInfo.Add("USER_NM", USER_NM);
                        userInfo.Add("USER_EMAIL", USER_EMAIL);
                    }
                }
            }
            catch (WebException exception)
            {
                Console.WriteLine(exception.ToString());
            }

            return userInfo;
        }

        private bool CheckUserID(string userID)
        {

            UserRepository repository = new UserRepository();
            return repository.CheckUserID(userID);
        }

        private string CheckUserCreateType(string userEmail, string userCreateType)
        {
            UserRepository repository = new UserRepository();
            return repository.CheckUserCreateType(userEmail, userCreateType);
        }

        private bool SaveKakaoUser(UserEntity entity)
        { 
            UserRepository repository = new UserRepository();
            UserContract contract = new UserContract();
            contract.UserEntity = entity;
            return repository.CreateUser(contract);

        }

    }
}