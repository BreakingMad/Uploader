using System.Web;
using System.Web.Optimization;

namespace UploadMVC
{
    public class BundleConfig
    {
        // Дополнительные сведения о Bundling см. по адресу http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jQuery.FileUpload").Include("~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
                         "~/Scripts/jQuery.FileUpload/jquery.fileupload-validate.js",
                         "~/Scripts/jQuery.FileUpload/jquery.fileupload-ui.js",
                         "~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/main.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-theme.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/jQuery.FileUpload").Include("~/Content/jQuery.FileUpload/css/jquery.fileupload.css"));
        }
    }
}