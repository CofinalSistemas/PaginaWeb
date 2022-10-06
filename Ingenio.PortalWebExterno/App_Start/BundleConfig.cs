using System.Web;
using System.Web.Optimization;

namespace Ingenio.PortalWebExterno
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts Bundle
            bundles.Add( new ScriptBundle( "~/bundles/lightbox-" ).Include(
                              "~/Scripts/lightbox/lightbox-plus-jquery.min.js",
                              "~/Scripts/lightbox/lightbox.min.map",
                              "~/Scripts/lightbox/lightbox.min.js",
                              "~/Scripts/lightbox/lightbox-plus-jquery.min.map"
                         ) );


            bundles.Add( new ScriptBundle( "~/bundles/jquery" ).Include(
                              "~/Scripts/jquery-1.11.0.min.js",
                                "~/Scripts/jquery-ui.min.js",
                         "~/Scripts/parallax.min.js"
                         ) );
            bundles.Add( new ScriptBundle( "~/bundles/editorHtml" ).Include(
                      "~/Scripts/jquery.tinymce.min.js"

                       ) );

            bundles.Add( new ScriptBundle( "~/bundles/jqueryval" ).Include(
                        "~/Scripts/jquery.validate*" ) );

            bundles.Add( new ScriptBundle( "~/bundles/plantilla" ).Include(
                        "~/Scripts/jquery.cookie.js",
                        "~/Scripts/waypoints.min.js",
                        "~/Scripts/jquery.counterup.min.js",
                        "~/Scripts/front.js",
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/jquery.price_format.2.0.js",
                        "~/Scripts/sweetalert.min.js"
                        ) );
            bundles.Add( new ScriptBundle( "~/bundles/plantillaCarousel" ).Include(
                       "~/Scripts/owl.carousel.min.js"
                       ) );
            bundles.Add( new ScriptBundle( "~/bundles/Nomenclaturas" ).Include(
                       "~/Scripts/Nomenclaturas.min.js" ) );

            bundles.Add( new ScriptBundle( "~/bundles/Mascaras" ).Include(
                "~/Scripts/jquery.mask.js"
                        ) );
            bundles.Add( new ScriptBundle( "~/bundles/md5" ).Include(
               "~/Scripts/md5.js"
                       ) );

            bundles.Add( new ScriptBundle( "~/bundles/maps" ).Include(
               "~/Scripts/gmaps.js",
               "~/Scripts/gmaps.init.js"
                       ) );

            bundles.Add( new ScriptBundle( "~/bundles/modernizr" ).Include(
                      "~/Scripts/modernizr-*" ) );

            bundles.Add( new ScriptBundle( "~/bundles/bootstrap" ).Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/pgwslideshow.min.js",
                      "~/Scripts/respond.js" ) );
            //libreria de editor html js
            bundles.Add( new ScriptBundle( "~/bundles/roalaEditorjs" ).Include(
                    "~/Scripts/editorHtml/froala_editor.min.js",
                    "~/Scripts/editorHtml/align.min.js",
                    "~/Scripts/editorHtml/code_beautifier.min.js",
                    "~/Scripts/editorHtml/code_view.min.js",
                     "~/Scripts/editorHtml/colors.min.js",
                      "~/Scripts/editorHtml/emoticons.min.js",
                      "~/Scripts/editorHtml/font_size.min.js",
                      "~/Scripts/editorHtml/font_family.min.js",
                      "~/Scripts/editorHtml/image.min.js",
                      "~/Scripts/editorHtml/file.min.js",
                      "~/Scripts/editorHtml/image_manager.min.js",
                      "~/Scripts/editorHtml/line_breaker.min.js",
                      "~/Scripts/editorHtml/link.min.js",
                      "~/Scripts/editorHtml/lists.min.js",
                      "~/Scripts/editorHtml/paragraph_format.min.js",
                      "~/Scripts/editorHtml/paragraph_style.min.js",
                      "~/Scripts/editorHtml/video.min.js",
                      "~/Scripts/editorHtml/table.min.js",
                      "~/Scripts/editorHtml/url.min.js",
                      "~/Scripts/editorHtml/entities.min.js",
                      "~/Scripts/editorHtml/char_counter.min.js",
                      "~/Scripts/editorHtml/inline_style.min.js",
                      "~/Scripts/editorHtml/save.min.js",
                      "~/Scripts/editorHtml/fullscreen.min.js",
                      "~/Scripts/editorHtml/quote.min.js",
                      "~/Scripts/editorHtml/quick_insert.min.js",
                      "~/Scripts/editorHtml/es.js" ) );
            #endregion

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.


            #region Styles Bundles

            //DNTO
            bundles.Add( new StyleBundle( "~/Content/lightbox" ).Include(
             "~/Content/lightbox/lightbox.min.csss"
             ) );


            bundles.Add( new StyleBundle( "~/Content/css" ).Include(
                       "~/Content/jquery-ui.min.css",
                       "~/Content/jquery.dataTables.css",
                        "~/Content/font-awesome.css",
                        "~/Content/bootstrap.min.css",
                        "~/Content/pgwslideshow.min.css"
                       ) );

            bundles.Add( new StyleBundle( "~/Content/plantilla" ).Include(
                        "~/Content/animate.css",
                        "~/Content/style.red.css",
                        "~/Content/custom.css",
                        "~/Content/sweetalert.css",
                        "~/Content/sass/main_style.min.css"
                      ) );
            bundles.Add( new StyleBundle( "~/Content/plantillafundacion" ).Include(
                       "~/Content/animate.css",
                       "~/Content/style.amarillo.css",
                       "~/Content/custom.css",
                       "~/Content/sweetalert.css"
                     ) );

            bundles.Add( new StyleBundle( "~/Content/plantillaHome" ).Include(
                        "~/Content/owl.carousel.css",
                        "~/Content/owl.theme.css"
                        ) );
            //libreria de editor html js

            bundles.Add( new StyleBundle( "~/Content/roalaEditorcss" ).Include(
                        "~/Content/editorHtml/froala_editor.min.css",
                         "~/Content/editorHtml/froala_style.min.css",
                         "~/Content/editorHtml/code_view.min.css",
                        "~/Content/editorHtml/colors.min.css",
                        "~/Content/editorHtml/emoticons.min.css",
                        "~/Content/editorHtml/image_manager.min.css",
                        "~/Content/editorHtml/image.min.css",
                        "~/Content/editorHtml/line_breaker.min.css",
                        "~/Content/editorHtml/table.min.css",
                        "~/Content/editorHtml/char_counter.min.css",
                        "~/Content/editorHtml/video.min.css",
                        "~/Content/editorHtml/fullscreen.min.css",
                        "~/Content/editorHtml/quick_insert.min.css",
                        "~/Content/editorHtml/file.min.css"
                        ) );
            #endregion
        }
    }
}
