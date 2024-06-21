using System.Web;
using System.Web.Mvc;

namespace AMHR_WEB
{
    /// <summary>
    /// FilterConfig : 라우팅에 사용되는 필터를 정의한다.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// RegisterGlobalFilters : 전역 필터를 등록한다.
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)    
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
