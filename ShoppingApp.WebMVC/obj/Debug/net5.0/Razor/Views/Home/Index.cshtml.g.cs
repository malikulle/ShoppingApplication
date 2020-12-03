#pragma checksum "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "34f93bf2726ab330f7954a24851e61e2798c91f5"
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
#line 2 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\_ViewImports.cshtml"
using ShoppingApp.Entities.Dtos;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\_ViewImports.cshtml"
using ShoppingApp.Entities.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\_ViewImports.cshtml"
using ShoppingApp.WebMVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34f93bf2726ab330f7954a24851e61e2798c91f5", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dd757a803eedd438cf59fe0a234d184897c8668f", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ShoppingApp.WebMVC.Models.ProductListModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_product", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!-- sidebar start -->\r\n<div id=\"sidebar\" class=\"col-md-3\">\r\n\r\n    ");
#nullable restore
#line 5 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml"
Write(await Component.InvokeAsync("CategoryList"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n    ");
#nullable restore
#line 7 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml"
Write(await Component.InvokeAsync("FeaturedProductList"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

</div>
<!-- sidebar end -->
<!-- right column start -->
<div class=""col-md-9"">

    <div class=""row"">

        <div class=""col-md-12 wow fadeIn"">
            <div class=""row product-results"">
                <div class=""product-results"">
                    <div class=""col-xs-8"">
                        <p class=""woocommerce-result-count"">Showing 1–3 of ");
#nullable restore
#line 20 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml"
                                                                      Write(Model.PageInfo.TotalItems);

#line default
#line hidden
#nullable disable
            WriteLiteral(" results</p>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n\r\n\r\n");
#nullable restore
#line 28 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml"
         foreach (var item in Model.Products)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "34f93bf2726ab330f7954a24851e61e2798c91f55295", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 30 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => item);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("for", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 31 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        <div class=""col-md-12 wow fadeIn"">
            <div id=""pagination"" class=""row text-center"">
                <nav aria-label=""Page navigation"">
                    <ul class=""pagination"">
                        <li class=""disabled"">
                            <a href=""#"" aria-label=""Previous"">
                                <span aria-hidden=""true"">&laquo;</span>
                            </a>
                        </li>
");
#nullable restore
#line 42 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml"
                         for (int i = 1; i <= Model.PageInfo.TotalPages(); i++)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <li");
            BeginWriteAttribute("class", " class=\"", 1438, "\"", 1495, 1);
#nullable restore
#line 44 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml"
WriteAttributeValue("", 1446, i == Model.PageInfo.CurrentPage ? "active" :"", 1446, 49, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><a");
            BeginWriteAttribute("href", " href=\"", 1499, "\"", 1515, 2);
            WriteAttributeValue("", 1506, "/?page=", 1506, 7, true);
#nullable restore
#line 44 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml"
WriteAttributeValue("", 1513, i, 1513, 2, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 44 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml"
                                                                                                         Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n");
#nullable restore
#line 45 "C:\Users\malik\source\repos\ShoppingApp\ShoppingApp.WebMVC\Views\Home\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <li>
                            <a href=""#"" aria-label=""Next"">
                                <span aria-hidden=""true"">&raquo;</span>
                            </a>
                        </li>
                    </ul>
                </nav>
            </div>
        </div>
    </div>

</div>
<!-- right column end -->");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ShoppingApp.WebMVC.Models.ProductListModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
