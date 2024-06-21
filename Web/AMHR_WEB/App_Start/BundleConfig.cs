using System.Web;
using System.Web.Optimization;

namespace AMHR_WEB
{
    public class BundleConfig
    {
        
        /// <summary>
        /// RegisterBundles : 묶음으로 관리할 Script / Css 를 경로를 지정하여 특정 이름으로 관리, Layout 에서 Binding 한다.
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            /* Script Bundle */
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/amhr").Include(
                        "~/StaticContents/scripts/amhr_*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/jquery_ui").Include(
                            "~/Scripts/jquery-ui.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jquery_fileupload").Include(
                            "~/Content/js/demo.js",
                            "~/Content/js/jquery.*",
                            "~/Content/js/vendor/jquery.ui.widget.js"
                        ));

            /* Bundle */
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new Bundle("~/bundles/bootstrap_datepicker").Include(
                      "~/Scripts/bootstrap-datepicker*"  
                      ));

            /* Style Bundle */
            bundles.Add(new StyleBundle("~/bundles/bootstrap_datepicker_ui").Include(
                      "~/Content/jquery-ui.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/css/jquery").Include(
                "~/Content/css/jquery.*"
                ));
            bundles.Add(new StyleBundle("~/bundles/amhrCss").Include(
                     "~/StaticContents/css/amhr_*"));
        }
    }
}
