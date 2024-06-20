using AMHR_WEB.GlobalAttribute;
using Newtonsoft.Json.Linq;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Contract;
using Contract.ENUM;
using Entity;

namespace AMHR_WEB.Controllers
{
    /// <summary>
    /// KakaoController : Kakao API 관련 Buisness Logic 담당
    /// </summary>
    public class KakaoController : Controller
    {
        /// <summary>
        /// 생성자 - Kakao 관련 Global 변수를 Set 한다..
        /// </summary>
        public KakaoController()
        {
            GlobalKakao.SetGlobalKakao();
        }

        /// <summary>
        /// KakaoLogin : 카카오 로그인을 위한 최초 진입 뷰 담당
        /// </summary>
        /// <returns></returns>
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

            // 01. Global 변수 세팅
            GlobalKakao.SetGlobalKakao();

            
            try
            {
                // 02. WebRequest 인스턴스 생성
                WebRequest request = WebRequest.Create(GlobalKakao.TOKEN_URI);

                // 03. 필요 값 입력
                request.Method = "POST";
                string postData = "grant_type=authorization_code" +
                                  "&client_id=" + GlobalKakao.CLIENT_ID + 
                                  "&redirect_uri=" + GlobalKakao.LOGIN_CALL_BACK_URI + 
                                  "&client_secret=" + GlobalKakao.CLIENT_SECRET + 
                                  "&code=" + code;

                byte[] byteArray = Encoding.ASCII.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                // 04. Stream > Write 요청을 내보낸다.
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                // 05. WebRequest 인스턴스로 WebResponse 인스턴스를 받아낸다.
                WebResponse response = request.GetResponse();
                Console.WriteLine("responseCode : " + ((HttpWebResponse)response).StatusCode);  // Test 시 확인

                // 06. Stream 으로 WebResponse 인스턴스의 Stream 을 읽어들인다.
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    string responseFromServer = reader.ReadToEnd();
                    Console.WriteLine("response body : " + responseFromServer); // Test 시 확인

                    // 07. Parse JSON response - 엑세스 토큰과 리프레시 토큰을 얻는다.
                    //     - 엑세스 토큰은 카카오 유저 정보를 얻기 위한 정보로 활용
                    dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseFromServer);
                    accessToken = jsonObject.access_token;
                    refreshToken = jsonObject.refresh_token;

                    Console.WriteLine("accessToken : " + accessToken);  // Test 시 확인
                    Console.WriteLine("refreshToken : " + refreshToken);    // Test 시 확인
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("WebException: " + e.Message);    // Test 시 확인
            }

            // 08. 엑세스 토큰으로 카카오 유저 정보를 얻어온다.
            Dictionary<string, object> resultOBJ = GetUserInfo(accessToken);

            // 09. 해당 카카오 유저가 AMHR 의 DataBase 에 존재하는 지 확인한다.
            //     - USER_ID
            //     - USER_EMAIL && USER_CREATE_TYPE
            //       의 무결성 점검
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

            // 10. 전부 TRUE 인 경우 - AMHR 에 카카오 유저 정보로 가입된 Data 가 없다는 의미
            //     카카오 유저 정보를 바탕으로 회원가입 한다.
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

                // 11. 회원가입 완료 시 
                if (SaveKakaoUser(entity))
                {
                    UserController userController = new UserController();
                    // 12. 가입정보(인증정보)를 바탕으로 AMHR 에 인증 토큰을 발급, 로그인 한다.
                    string loginResult = userController.SNSLoginCheckUser(entity.USER_ID, GlobalKakao.CONST_PWD, this.HttpContext);
                    if (loginResult.Equals("OK"))
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        TempData.Add("MESSAGE", "로그인에 실패하였습니다. 관리자에게 문의하십시오.");
                        return RedirectToAction("UserLogin", "User");
                    }
                }
                else
                {
                    TempData.Add("MESSAGE", "로그인에 실패하였습니다. 관리자에게 문의하십시오.");
                    return RedirectToAction("UserLogin", "User"); 
                }
            }
            // 13. 하나라도 FALSE 인 경우 - AMHR 에 등록된 Data 가 이미 있다는 의미
            //     사전에 가입된정보(인증된정보)를 바탕으로 AMHR에 인증 토큰을 발급, 로그인한다.
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
                    TempData.Add("MESSAGE", "로그인에 실패하였습니다. 관리자에게 문의하십시오.");
                    return RedirectToAction("UserLogin", "User");
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

        /// <summary>
        /// CheckUserID : 사용자 ID 중복확인 메소드
        /// </summary>
        /// <param name="userID">사용자 ID</param>
        /// <returns></returns>
        private bool CheckUserID(string userID)
        {

            UserRepository repository = new UserRepository();
            return repository.CheckUserID(userID);
        }

        /// <summary>
        /// CheckUserCreateType : 사용자 EMAIL, CREATE_TYPE 중복확인 메소드
        /// </summary>
        /// <param name="userEmail">사용자 EMAIL</param>
        /// <param name="userCreateType">사용자 CREATE TYPE</param>
        /// <returns></returns>
        private string CheckUserCreateType(string userEmail, string userCreateType)
        {
            UserRepository repository = new UserRepository();
            return repository.CheckUserCreateType(userEmail, userCreateType);
        }

        /// <summary>
        /// SaveKakaoUser : 사용자 정보 등록 메소드
        /// </summary>
        /// <param name="entity">UserEntity</param>
        /// <returns></returns>
        private bool SaveKakaoUser(UserEntity entity)
        { 
            UserRepository repository = new UserRepository();
            UserContract contract = new UserContract();
            contract.UserEntity = entity;
            return repository.CreateUser(contract);

        }

    }
}