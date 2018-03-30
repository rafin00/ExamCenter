using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace app
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            ScriptBundle scriptBundle = new ScriptBundle("~/scripts");
            scriptBundle.Include("~/Scripts/jquery-3.0.0.js");
            scriptBundle.Include("~/Scripts/myScript.js");
            scriptBundle.Include("~/Content/style.css");
            bundles.Add(scriptBundle);

            StyleBundle styleBundle=new StyleBundle("~/styles");
            styleBundle.Include("~/Content/style.css");
            bundles.Add(styleBundle);

            //BundleTable.EnableOptimizations = true;
        }
    }
}