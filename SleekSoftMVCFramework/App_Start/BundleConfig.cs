using System.Web;
using System.Web.Optimization;

namespace SleekSoftMVCFramework
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            // "~/Scripts/jquery-{version}.js"
                            "~/Scripts/jquery-2.2.3.min.js",
                        "~/Scripts/jquery-3.1.1.min.js",
                         "~/Scripts/jquery-1.10.2.min.js",
                        "~/Scripts/tether.min.js",
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery.matchHeight.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",

                      "~/Scripts/toastr.js",
                      "~/Scripts/Apptoastr.js",
                      //"~/Scripts/jquery.dataTables.min.js",
                      //"~/Scripts/dataTables.bootstrap.min.js",
                      //"~/Scripts/dataTables.buttons.min.js",
                      //"~/Scripts/buttons.flash.min.js",
                      //"~/Scripts/jszip.min.js",
                      //"~/Scripts/pdfmake.min.js",
                      //"~/Scripts/vfs_fonts.js",
                      //"~/Scripts/buttons.html5.min.js",
                      //"~/Scripts/buttons.print.min.js",
                      //"~/Scripts/select2.full.js"
                      //"~/Scripts/bootstrap.min.js",
                      "~/Scripts/fastclick.min.js",
                      "~/Scripts/app.js",
                      "~/Scripts/moment.min.js",
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/dataTables.bootstrap.min.js",
                      "~/Scripts/dataTables.buttons.min.js",
                      "~/Scripts/buttons.flash.min.js",
                      "~/Scripts/jszip.min.js",
                      "~/Scripts/pdfmake.min.js",
                      "~/Scripts/vfs_fonts.js",
                      "~/Scripts/buttons.html5.min.js",
                      "~/Scripts/buttons.print.min.js",
                      "~/Scripts/jquery.sparkline.min.js",
                      "~/Scripts/jquery.slimscroll.min.js",
                      "~/Scripts/select2.full.min.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/bootstrap-colorpicker.min.js",
                      "~/Scripts/jasny-bootstrap.min.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/fullcalendar.min.js",
                      "~/Scripts/fullcalendar.print.min.js",
                      "~/Scripts/custom.js",
                      "~/Scripts/summernote.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/bootstrap.min.css",
                      "~/Content/_bootstrap-datetimepicker.less",
                      "~/Content/font-awesome.min.css",
                      "~/Content/ionicons.min.css",
                      "~/Content/site.css",
                       "~/Content/App.css",
                      "~/Content/toastr.css",
                    "~/Content/buttons.dataTables.min.css",
                     "~/Content/dataTables.bootstrap.css",
                    "~/Content/jquery.dataTables.css",
                     "~/Content/datepicker3.css",
                      "~/Content/select2.min.css",
                     "~/Content/jasny-bootstrap.min.css",
                      "~/Content/bootstrap-colorpicker.min.css",
                      "~/Content/AdminLTE.css",
                      "~/Content/skins/_all-skins.min.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/Logincss").Include(
                      "~/Content/bootstrap.min.css",
                     "~/Content/font-awesome.min.css",
                     "~/Content/ionicons.min.css",
                     "~/Content/AdminLTE.css",
                     "~/Content/skins/skin-blue.css"));


            bundles.Add(new ScriptBundle("~/bundles/Loginjs").Include(

                    "~/Scripts/jquery-2.2.3.min.js",
                    "~/Scripts/jquery-3.1.1.min.js",
                    "~/Scripts/jquery-1.10.2.min.js",
                      "~/Scripts/jquery.min.js",
                     "~/Scripts/bootstrap.min.js",
                     "~/Scripts/icheck.min.js"
                     
                     ));

        }
    }
}
