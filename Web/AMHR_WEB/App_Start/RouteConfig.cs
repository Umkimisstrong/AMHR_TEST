﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AMHR_WEB
{
    /// <summary>
    /// RouteConfig : 애플리케이션에 사용될 라우팅을 정의한다.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// RegisterRoutes : 라우팅 등록
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
