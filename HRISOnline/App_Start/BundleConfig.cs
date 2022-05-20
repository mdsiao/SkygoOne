using System.Web;
using System.Web.Optimization;

namespace HRISOnline
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/bootstrap.css",
                       "~/Content/bootstrap.min.css",
                       "~/Content/jquery-ui.min.css",
                       "~/Content/jquery.fancybox.css",
                       "~/Content/fullcalendar.css",
                       "~/Content/select2.css",
                       "~/Content/bootstrap-timepicker.min.css",
                       "~/Content/datepicker.css",
                       "~/Content/dataTables.bootstrap.css",
                       "~/Content/bootstrap-dialog.min.css",
                       "~/Content/metisMenu.min.css",
                       "~/Content/sb-admin-2.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.7.1.js",
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/jquery.justifiedgallery.min.js",
                        "~/Scripts/tinymce.min.js",
                        "~/Scripts/jquery.tinymce.min.js",
                        "~/Scripts/bootstrap-timepicker.min.js",
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/dataTables.bootstrap.js",
                        "~/Scripts/bootstrap-dialog.min.js",
                        "~/Scripts/json2.js",
                        "~/Scripts/accounting.js",
                        "~/Scripts/select2.min.js",
                        "~/Scripts/metisMenu.min.js",
                        "~/Scripts/raphael-min.js",
                        "~/Scripts/sb-admin-2.js",
                        "~/Scripts/global-notifications.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));           
           
        }
    }
}