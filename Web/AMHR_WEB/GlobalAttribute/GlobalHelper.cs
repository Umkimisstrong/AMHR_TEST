using AMHR_WEB.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AMHR_WEB.GlobalAttribute
{
    public static class GlobalHelper
    {
        public static string ToJson<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        public static T ToObject<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static T stringToObject<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }


    }
}