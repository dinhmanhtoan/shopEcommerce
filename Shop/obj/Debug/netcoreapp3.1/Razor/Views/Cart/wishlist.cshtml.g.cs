#pragma checksum "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e096d57c5a879939a280ad7c376a5e9b7fa75cf1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cart_wishlist), @"mvc.1.0.view", @"/Views/Cart/wishlist.cshtml")]
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
#line 1 "I:\SoureToan\Shop\shop2\Shop\Views\_ViewImports.cshtml"
using Shop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "I:\SoureToan\Shop\shop2\Shop\Views\_ViewImports.cshtml"
using Shop.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "I:\SoureToan\Shop\shop2\Shop\Views\_ViewImports.cshtml"
using Model.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "I:\SoureToan\Shop\shop2\Shop\Views\_ViewImports.cshtml"
using Model.ViewModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e096d57c5a879939a280ad7c376a5e9b7fa75cf1", @"/Views/Cart/wishlist.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fe425b70ed0e4808eadefa6fc69a5d5cbf4cc194", @"/Views/_ViewImports.cshtml")]
    public class Views_Cart_wishlist : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Product>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <div class=\"cart\">\r\n\r\n            <h2 >Danh sách yêu thích</h2>\r\n\r\n        <div class=\"cart-body\">\r\n            <div class=\"col-md-12 col-12 product-item\">\r\n                <ul style=\" display: flex;flex-wrap: wrap; list-style-type:none\">\r\n");
#nullable restore
#line 9 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
                     foreach (var item in Model)
                    {


#line default
#line hidden
#nullable disable
            WriteLiteral("                        <li class=\"col-md-3 col-6 product-item\">\r\n\r\n                            <div class=\"product-grid6 \"");
            BeginWriteAttribute("id", " id=\"", 464, "\"", 482, 2);
            WriteAttributeValue("", 469, "item_", 469, 5, true);
#nullable restore
#line 14 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
WriteAttributeValue("", 474, item.Id, 474, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-id=\"");
#nullable restore
#line 14 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
                                                                               Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                                <div class=\"product-image6\">\r\n                                    <a");
            BeginWriteAttribute("href", " href=\"", 605, "\"", 632, 2);
            WriteAttributeValue("", 612, "/chi-tiet/", 612, 10, true);
#nullable restore
#line 16 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
WriteAttributeValue("", 622, item.Slug, 622, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        <img class=\"pic-1\"");
            BeginWriteAttribute("src", " src=\"", 694, "\"", 760, 2);
            WriteAttributeValue("", 700, "https://localhost:5002/user-content/", 700, 36, true);
#nullable restore
#line 17 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
WriteAttributeValue("", 736, item.Thumbnail.FileName, 736, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 761, "\"", 778, 1);
#nullable restore
#line 17 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
WriteAttributeValue("", 767, item.Title, 767, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    </a>\r\n                                </div>\r\n                                <div class=\"product-content\">\r\n                                    <h3 class=\"title\"><a href=\"#\">");
#nullable restore
#line 21 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
                                                             Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h3>\r\n                                    <div>\r\n");
#nullable restore
#line 23 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
                                         if (item.Sale > 0)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span class=\"price\">");
#nullable restore
#line 25 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
                                                           Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ₫- </span>\r\n                                            <span class=\"sale\">");
#nullable restore
#line 26 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
                                                          Write(item.Sale);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ₫</span>\r\n");
#nullable restore
#line 27 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span class=\"price\">");
#nullable restore
#line 30 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
                                                           Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ₫</span>\r\n");
#nullable restore
#line 31 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </div>\r\n                                </div>\r\n                                <ul class=\"social\">\r\n                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 1777, "\"", 1804, 2);
            WriteAttributeValue("", 1784, "/chi-tiet/", 1784, 10, true);
#nullable restore
#line 35 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"
WriteAttributeValue("", 1794, item.Slug, 1794, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""js-show-modal1"" data-tip=""Quick View""><i class=""fa fa-search""></i></a></li>
                                    <li><a href=""javascript:void(0)"" class=""remove-wishlish"" data-status=""true"" data-id=""6"" data-tip=""Remove to Wishlist""><i class=""fas fa-times-circle""></i></a></li>
                                    <li>
                                        <a href=""javascript:void(0)"" class=""js-add-cart"" data-status=""true"" data-id=""6"" data-tip=""Add to Cart"">
                                            <i class=""fa fa-shopping-basket"" aria-hidden=""true""></i>
                                        </a>
                                </ul>
                            </div>
                        </li>
");
#nullable restore
#line 44 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\wishlist.cshtml"



                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </ul>\r\n            </div>\r\n        </div>\r\n\r\n    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Product>> Html { get; private set; }
    }
}
#pragma warning restore 1591
