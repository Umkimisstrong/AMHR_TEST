using AMHR_WEB.GlobalAttribute;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace AMHR_WEB.Controllers
{
    public class KakaoController : Controller
    {
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

            return Json(resultOBJ, JsonRequestBehavior.AllowGet);
        }

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

                        string nickname = properties["nickname"].ToString();
                        string email = kakaoAccount["email"].ToString();

                        userInfo.Add("nickname", nickname);
                        userInfo.Add("email", email);
                    }
                }
            }
            catch (WebException exception)
            {
                Console.WriteLine(exception.ToString());
            }

            return userInfo;
        }

    }
}