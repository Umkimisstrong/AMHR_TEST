using AMHR_WEB.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB.GlobalAttribute
{
    /// <summary>
    /// GlobalHelper : 글로벌 헬퍼 클래스
    /// </summary>
    public static class GlobalHelper
    {
        /// <summary>
        /// ToJson : 특정 Object를 string 으로 변환하는 메소드
        /// </summary>
        /// <typeparam name="T">Json 형식의 Object 타입</typeparam>
        /// <param name="t">Object 매개변수</param>
        /// <returns></returns>
        public static string ToJson<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        /// <summary>
        /// ToObject : 특정 string 을 Object 로 변환하는 메소드( string.ToObject )
        /// </summary>
        /// <typeparam name="T">Json 형식의 Object 타입</typeparam>
        /// <param name="data">string 매개변수</param>
        /// <returns></returns>
        public static T ToObject<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// ToObject : 특정 string 을 Object 로 변환하는 메소드( ToObject(string) )
        /// </summary>
        /// <typeparam name="T">Json 형식의 Object 타입</typeparam>
        /// <param name="data">string 매개변수</param>
        /// <returns></returns>
        public static T stringToObject<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }


        public static List<SelectListItem> GetTextValueItem(string SYS_CODE_ID, string DIV_CODE_ID)
        {
            CodeRepository repository = new CodeRepository();
            DataSet ds = repository.GetCodeTextValueItem(SYS_CODE_ID, DIV_CODE_ID);

            List<SelectListItem> list = new List<SelectListItem>();

            if (ds != null && ds.Tables[0].Rows.Count>0)
            {
                foreach(DataRow rowItem in ds.Tables[0].Rows ) 
                {
                    SelectListItem selectListItem = new SelectListItem();
                    selectListItem.Text = rowItem["CD_NM"].ToString();
                    selectListItem.Value = rowItem["CD"].ToString();
                    list.Add(selectListItem);
                }
            }

            return list;
        }

    }
}