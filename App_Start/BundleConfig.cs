using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebAdmin.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include(
                            "~/Scripts/jquery-{version}.js",
                            "~/Scripts/toastr.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                          "~/Content/bootstrap.css",
                          "~/Content/site.css",
                          "~/Content/dash.css",
                          "~/Content/toastr.css",
                          "~/Content/font-awesome.css"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include(
                            "~/Scripts/angular.min.js",
                            "~/Scripts/angular-route.js",
                            "~/Scripts/App/App.js",
                            "~/Scripts/App/Services/Service.js",
                            "~/Scripts/App/Services/AlertasService.js",
                            "~/Scripts/App/Controllers/UsuarioController.js",
                            "~/Scripts/App/Controllers/TipoUsuarioController.js",
                            "~/Scripts/App/Controllers/CategoriaProdutoController.js",
                            "~/Scripts/App/Controllers/ProdutoController.js",
                            "~/Scripts/App/Controllers/ImagemProdutoController.js"
                        ));
        }

    }
}