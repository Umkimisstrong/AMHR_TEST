using System.Configuration;
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
            
            /* Web.Config 에서 테마를 확인한다. ON 인 테마를 prefix 문자열로 만들어 Bundle Load 시 적용 */
            string LUX = ConfigurationManager.AppSettings["cssTheme_LUX"].ToString();
            string JOURNAL = ConfigurationManager.AppSettings["cssTheme_JOURNAL"].ToString();
            string prefixTHEME = LUX.Equals("ON") ? "LUX_" : (JOURNAL.Equals("ON") ? "JOURNAL_" : LUX);


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
            

            /* Bundle */
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new Bundle("~/bundles/bootstrap_datepicker").Include(
                      "~/Scripts/bootstrap-datepicker*"  
                      ));

            /* Style Bundle */
            bundles.Add(new StyleBundle("~/bundles/bootstrap_datepicker_ui").Include(
                      "~/Content/" + prefixTHEME + "jquery-ui.css"
                      ));   // DatePicker Style

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/"+ prefixTHEME+"bootstrap.css",
                      "~/Content/"+ prefixTHEME+"bootstrap.min.css",
                      "~/Content/site.css"
                      ));   // BootStrap All Style

            bundles.Add(new StyleBundle("~/Content/css/jquery").Include(
                "~/Content/css/jquery.*"
                ));

            bundles.Add(new StyleBundle("~/bundles/amhrCss").Include(
                     "~/StaticContents/css/"+ prefixTHEME+"amhr_*"));
            // Amhr Custom Style

            BundleTable.EnableOptimizations = false;
        }
    }
}
