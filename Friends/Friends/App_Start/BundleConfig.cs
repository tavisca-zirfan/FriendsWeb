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
                            .Include("~/Content/site.css")
                            .Include("~/Scripts/Plugins/picEdit/css/font.css")
                            .Include("~/Scripts/Plugins/picEdit/css/picedit.css")
                            .Include("~/Scripts/Plugins/bootstrap-fileinput/bootstrap-fileinput.css")
                            );
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
                .Include("~/Scripts/Plugins/picEdit/js/picedit.js")
                .Include("~/Scripts/Plugins/bootstrap-fileinput/bootstrap-fileinput.js")
                );

            // add scripts for post
            bundles.Add(new ScriptBundle("~/scripts/post")
                .Include("~/Scripts/Model/postLikeModel.js")
                .Include("~/Scripts/Model/commentModel.js")
                .Include("~/Scripts/Model/postModel.js")
                .Include("~/Scripts/Model/profileModel.js")
                .Include("~/Scripts/Views/likeView.js")
                .Include("~/Scripts/Views/basePostView.js")
                .Include("~/Scripts/Views/commentView.js")
                .Include("~/Scripts/Views/onlineListView.js")
                .Include("~/Scripts/Views/postListView.js")
                );

            bundles.Add(new ScriptBundle("~/scripts/profile")
                .Include("~/Scripts/Views/profileView.js")
                .Include("~/Scripts/Views/profilePicView.js")
                );
        }
    }
}