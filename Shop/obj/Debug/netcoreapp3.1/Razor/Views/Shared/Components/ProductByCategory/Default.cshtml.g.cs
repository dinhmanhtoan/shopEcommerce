#pragma checksum "I:\SoureToan\Shop\Shop\Views\Shared\Components\ProductByCategory\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e2ed374a0d6768d1726c93000c0633637bcae9a1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_ProductByCategory_Default), @"mvc.1.0.view", @"/Views/Shared/Components/ProductByCategory/Default.cshtml")]
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
#line 1 "I:\SoureToan\Shop\Shop\Views\_ViewImports.cshtml"
using Shop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "I:\SoureToan\Shop\Shop\Views\_ViewImports.cshtml"
using Shop.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "I:\SoureToan\Shop\Shop\Views\_ViewImports.cshtml"
using Model.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "I:\SoureToan\Shop\Shop\Views\_ViewImports.cshtml"
using Model.ViewModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2ed374a0d6768d1726c93000c0633637bcae9a1", @"/Views/Shared/Components/ProductByCategory/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fe425b70ed0e4808eadefa6fc69a5d5cbf4cc194", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_ProductByCategory_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Model.Models.Product>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div class=\"container\">\r\n    <div class=\"row\">\r\n");
#nullable restore
#line 6 "I:\SoureToan\Shop\Shop\Views\Shared\Components\ProductByCategory\Default.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-md-3\">\r\n                <div class=\"img-thumbnail\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 241, "\"", 271, 1);
#nullable restore
#line 10 "I:\SoureToan\Shop\Shop\Views\Shared\Components\ProductByCategory\Default.cshtml"
WriteAttributeValue("", 247, item.Thumbnail.FileName, 247, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                </div>\r\n                <div class=\"infomation\">\r\n                    <h1 class=\"title\">");
#nullable restore
#line 13 "I:\SoureToan\Shop\Shop\Views\Shared\Components\ProductByCategory\Default.cshtml"
                                 Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n                    <div class=\"price\">\r\n");
#nullable restore
#line 15 "I:\SoureToan\Shop\Shop\Views\Shared\Components\ProductByCategory\Default.cshtml"
                         if (item.Sale != null && item.Sale > 0)
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "I:\SoureToan\Shop\Shop\Views\Shared\Components\ProductByCategory\Default.cshtml"
                       Write(item.Sale);

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <del>");
#nullable restore
#line 18 "I:\SoureToan\Shop\Shop\Views\Shared\Components\ProductByCategory\Default.cshtml"
                            Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</del>\r\n");
#nullable restore
#line 19 "I:\SoureToan\Shop\Shop\Views\Shared\Components\ProductByCategory\Default.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 23 "I:\SoureToan\Shop\Shop\Views\Shared\Components\ProductByCategory\Default.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Model.Models.Product>> Html { get; private set; }
    }
}
#pragma warning restore 1591