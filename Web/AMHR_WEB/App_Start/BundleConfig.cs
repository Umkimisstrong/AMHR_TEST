using System.Web;
using System.Web.Optimization;

namespace AMHR_WEB
{
    public class BundleConfig
    {
        // 묶음에 대한 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=301862를 참조하세요.
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/amhr").Include(
                        "~/StaticContents/scripts/amhr_*"));

            // Modernizr의 개발 버전을 사용하여 개발하고 배우십시오. 그런 다음
            // 프로덕션에 사용할 준비를 하고 https://modernizr.com의 빌드 도구를 사용하여 필요한 테스트만 선택하세요.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new Bundle("~/bundles/bootstrap_datepicker").Include(
                      "~/Scripts/bootstrap-datepicker*"
                      ));

            bundles.Add(new ScriptBundle ("~/bundles/jquery_ui").Include(
                            "~/Scripts/jquery-ui.min.js"
                        ));
            bundles.Add(new StyleBundle("~/bundles/bootstrap_datepicker_ui").Include(
                      "~/Content/jquery-ui.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"
                      ));

            bundles.Add(new StyleBundle("~/bundles/amhrCss").Include(
                     "~/StaticContents/css/amhr_*"));
        }
    }
}
