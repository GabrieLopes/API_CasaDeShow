#pragma checksum "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1ab8441caa507648aac49f538223a20553e59de0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\_ViewImports.cshtml"
using CasaEventos;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\_ViewImports.cshtml"
using CasaEventos.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1ab8441caa507648aac49f538223a20553e59de0", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e7cf04895d1937ee7394cd7305ba33521703d16", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CasaEventos.Models.Evento>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Compras", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Compra", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";
    int count = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div id=""carouselExampleIndicators"" class=""carousel slide"" data-ride=""carousel"">
      <div class=""carousel-inner"">
    <div class=""carousel-item active"" data-interval=""2000"">
      <img src=""/Img/technic_upcoming_still_1280x720_jpg.jpg"" class=""d-block w-100"" style=""height: 800px;"">
       <div class=""carousel-caption d-none d-md-block"">
        <h5>Proximos Eventos</h5>
      </div>
    </div>
");
#nullable restore
#line 16 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
 if(Model.Count() != 0 ){
    count++;
    if(count < 5)
    {
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
     foreach (var item in Model)
      {

#line default
#line hidden
#nullable disable
            WriteLiteral("     <div class=\"carousel-item\" data-interval=\"4000\">\r\n      <img");
            BeginWriteAttribute("src", " src=\"", 697, "\"", 715, 1);
#nullable restore
#line 23 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
WriteAttributeValue("", 703, item.Imagem, 703, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"d-block w-100\" style=\"height: 800px;\">\r\n       <div class=\"carousel-caption d-none d-md-block\">\r\n        <h4><b>");
#nullable restore
#line 25 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
          Write(item.NomeEvento);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></h4>\r\n        <h5><b>Ingressos: ");
#nullable restore
#line 26 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
                     Write(item.QuantidadeIngressos);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></h5>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1ab8441caa507648aac49f538223a20553e59de06497", async() => {
                WriteLiteral("COMPRAR AGORA");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 27 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
                                                   WriteLiteral(item.EventoId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n      </div>\r\n    </div>\r\n");
#nullable restore
#line 30 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
      }

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
       

    }
}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"  <a class=""carousel-control-prev"" href=""#carouselExampleIndicators"" role=""button"" data-slide=""prev"">
    <span class=""carousel-control-prev-icon"" aria-hidden=""true""></span>
    <span class=""sr-only"">Previous</span>
  </a>
  <a class=""carousel-control-next"" href=""#carouselExampleIndicators"" role=""button"" data-slide=""next"">
    <span class=""carousel-control-next-icon"" aria-hidden=""true""></span>
    <span class=""sr-only"">Next</span>
  </a>
</div> 
</div>
<br>


<table class=""table"">
    <tbody>
<div class=""container"">
<div class=""row"">
");
#nullable restore
#line 51 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"col-4\">\r\n    <div class=\"card mb-3 card border-dark\" style=\"width: 18rem;\">\r\n    <img");
            BeginWriteAttribute("src", " src=\"", 1809, "\"", 1827, 1);
#nullable restore
#line 54 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
WriteAttributeValue("", 1815, item.Imagem, 1815, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-fluid card-img-top\" alt=\"Responsive image\">\r\n        <div class=\"card-body\">\r\n        <h5 class=\"card-title\">");
#nullable restore
#line 56 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
                          Write(item.NomeEvento);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n        <p class=\"card-text\"> <b>Ingressos Restantes:</b> ");
#nullable restore
#line 57 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
                                                     Write(item.QuantidadeIngressos);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br>\r\n        <b>O local do evento é:</b> ");
#nullable restore
#line 58 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
                               Write(item.Casa.Nome);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <b>e seu foco é em:</b> ");
#nullable restore
#line 58 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
                                                                       Write(item.Genero.GeneroNome);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br>\r\n        <b>O evento ocorrerá em:</b> ");
#nullable restore
#line 59 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
                                Write(item.DataEvento);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1ab8441caa507648aac49f538223a20553e59de012223", async() => {
                WriteLiteral("Comprar");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 60 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
                                                   WriteLiteral(item.EventoId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    </div>\r\n");
#nullable restore
#line 64 "C:\Users\GLLV\Desktop\Code\CasaEventos\Views\Home\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>\r\n    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CasaEventos.Models.Evento>> Html { get; private set; }
    }
}
#pragma warning restore 1591
