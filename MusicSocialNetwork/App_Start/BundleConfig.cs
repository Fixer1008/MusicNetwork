﻿using System.Web;
using System.Web.Optimization;

namespace MusicSocialNetwork
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery.validate.*"));
                    //"~/Scripts/ViewModels/LoginFormValidation.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                    "~/Scripts/knockout-3.1.0.*",
                    "~/Scripts/knockout.validation.*",
                    "~/Scripts/pager.js"));

            bundles.Add(new ScriptBundle("~/bundles/viewModels").Include(
                     "~/Scripts/ViewModels/NetworkViewModel.js",
                     "~/Scripts/ViewModels/AudioViewModel.js"));
        }
    }
}
