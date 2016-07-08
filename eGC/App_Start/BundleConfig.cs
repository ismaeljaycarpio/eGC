using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace eGC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
                "~/Content/Site.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/bootstrap-datepicker.css",
                "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include
                ("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include
                ("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include
                ("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include
                ("~/Scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include
                ("~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datetimepicker").Include
                ("~/Scripts/bootstrap-datetimepicker.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}