using System.Web;
using System.Web.Optimization;

namespace WebLayer
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/switAlert").Include(
                      "~/Scripts/switAlert.js"));

            bundles.Add(new ScriptBundle("~/bundles/ColorAdmin").Include(
                      "~/Scripts/ColorAdmin/jquery.slimscroll.js",
                      "~/Scripts/ColorAdmin/jquery.cookie.js",
                      "~/Scripts/ColorAdmin/jquery.gritter.js",
                      "~/Scripts/ColorAdmin/dashboard.js",
                      "~/Scripts/ColorAdmin/apps.js"));


            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/switAlert.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/switAlert.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/ColorAdmin").Include(
                      "~/Content/ColorAdmin/animate.css",
                      "~/Content/ColorAdmin/style.css",
                      "~/Content/ColorAdmin/style-responsive.css",
                      "~/Content/ColorAdmin/theme/blue.css",
                      "~/Content/ColorAdmin/jquery.gritter.css"
                      ));
        }
    }
}
