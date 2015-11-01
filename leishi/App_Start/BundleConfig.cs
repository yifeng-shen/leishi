using System.Web.Optimization;

namespace leishi.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Style bundles.
            bundles.Add(new StyleBundle("~/bundles/Styles/LayoutShared").Include(
                    "~/Content/Shared/bootstrap.min.css", new CssRewriteUrlTransform()).Include(
                    "~/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()).Include(
                    "~/Content/Shared/Site.css", new CssRewriteUrlTransform()));

            // Currently we are using CDN to load jquery script.
            // bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            // bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugin").Include(
                      "~/Scripts/Plugin/*.js"));
        }
    }
}
