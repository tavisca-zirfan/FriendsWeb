using System.Web.Optimization;

namespace Friends.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterJavascriptBundles(bundles);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css")
                            .Include("~/Content/bootstrap.css")
                            .Include("~/Scripts/Plugins/jquery-ui/jquery-ui.css")
                            .Include("~/Content/carousel.css")
                            .Include("~/Content/custom-styles.css")
                            .Include("~/Content/site.css"));
        }

        private static void RegisterJavascriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js")
                            .Include("~/Scripts/jquery-{version}.js")
                            .Include("~/Scripts/jquery-ui-{version}.js")
                            .Include("~/Scripts/Plugins/jquery-ui/jquery-ui.js")
                            .Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/scripts/primary")
                .Include("~/Scripts/Library/underscore-min.js")
                .Include("~/Scripts/Library/handlebars-1.3.js")
                .Include("~/Scripts/Library/backbone-min.js")
                .IncludeDirectory("~/Scripts/Library","*.js")
                .Include("~/Scripts/Utils/searchFilter.js")
                );

            // add scripts for post
            bundles.Add(new ScriptBundle("~/scripts/post")
                .Include("~/Scripts/Model/commentModel.js")
                .Include("~/Scripts/Model/postModel.js")
                .Include("~/Scripts/Model/profileModel.js")
                .Include("~/Scripts/Views/basePostView.js")
                .Include("~/Scripts/Views/commentView.js")
                );
        }
    }
}