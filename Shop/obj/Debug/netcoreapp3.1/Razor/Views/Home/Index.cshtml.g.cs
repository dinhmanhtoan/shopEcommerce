#pragma checksum "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6b3883e25ad5255369d6b8539a08aa44b0d1d2d2"
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
#line 1 "D:\souc\source\.net\Shop2\shop2\Shop\Views\_ViewImports.cshtml"
using Shop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\souc\source\.net\Shop2\shop2\Shop\Views\_ViewImports.cshtml"
using Shop.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\souc\source\.net\Shop2\shop2\Shop\Views\_ViewImports.cshtml"
using Model.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\souc\source\.net\Shop2\shop2\Shop\Views\_ViewImports.cshtml"
using Model.ViewModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b3883e25ad5255369d6b8539a08aa44b0d1d2d2", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fe425b70ed0e4808eadefa6fc69a5d5cbf4cc194", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProductList>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("w-100"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/banner/slider1.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/banner/slider2.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/banner/slider3.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            DefineSection("Scripts", async() => {
                WriteLiteral(@" 
    <script>
        $("".owlCarousel_slider"").owlCarousel({
            autoplay: true, // tự động chuyển slider
            //loop: true,
            items: 1,
            nav: true, // hiện thị bật tắt nút next,back
            dots: true,
            lazyLoad: true,
            // slideBy:1,
            // // lazyLoadEager: 4,
            // // lazyLoad : true,
            autoHeight: true,
            navText: ['<i class=""fa fa-chevron-left"" aria-hidden=""true""></i>', '<i class=""fa fa-chevron-right"" aria-hidden=""true""></i>'] /// nút next,back
        });
    </script>
");
            }
            );
            WriteLiteral("<div class=\"banner\">\r\n    <div id=\"banner\" class=\"carousel slide\" data-ride=\"carousel\">\r\n        <div class=\"owl-carousel owlCarousel_slider owl-theme\">\r\n            <div class=\"item active\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "6b3883e25ad5255369d6b8539a08aa44b0d1d2d25922", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n            <div class=\"item\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "6b3883e25ad5255369d6b8539a08aa44b0d1d2d27110", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                <!-- <div class=""carousel-caption "">
                    <div class=""caption-content col-md-6 f-right"">
                        <h3 class=""fadeInRight"">Bộ sưu tập nam</h3>
                        <p class=""fadeInRight"">Những sản phẩm mới nhất dành cho nam</p>
                        <a href=""#"" class=""btn btn-black fadeInRight"">Mua ngay</a>
                    </div>
                </div> -->
            </div>
            <div class=""item"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "6b3883e25ad5255369d6b8539a08aa44b0d1d2d28725", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                <!-- <div class=""carousel-caption "">
                    <div class=""caption-content col-md-6 f-right"">
                        <h3 class=""fadeInRight"">Bộ sưu tập nam</h3>
                        <p class=""fadeInRight"">Những sản phẩm mới nhất dành cho nam</p>
                        <a href=""#"" class=""btn btn-black fadeInRight"">Mua ngay</a>
                    </div>
                </div> -->
            </div>
        </div>
    </div>
</div>
<div id=""product-new"" class=""product"">
    <div class=""container"">
        <div class=""row"">
            <h2 class=""title"">Sản phẩm mới</h2>
            <!-- <div class=""col-md-3 col-6"">
                    <div class=""product-grid6"" id=""item_1"">
                        <div class=""product-image6"">
                            <a href=""#"">
                                <img class=""pic-1"" src=""http://bestjquery.com/tutorial/product-grid/demo10/images/img-1.jpg"">
                            </a>
                        </div>
    ");
            WriteLiteral(@"                    <div class=""product-content"">
                            <h3 class=""title""><a href=""#"">Men's Shirt 1</a></h3>
                            <div class=""price"">$11.00
                                <span>$14.00</span>
                            </div>
                        </div>
                        <ul class=""social"">
                            <li><a href="""" class=""js-show-modal1"" data-tip=""Quick View""><i class=""fa fa-search""></i></a></li>
                            <li><a href="""" class=""js-add-wishlist""  data-status=""true"" data-id=""1"" data-tip=""Add to Wishlist""><i class=""fal fa-heart""></i></a></li>
                            <li><a href="""" class=""js-add-cart"" data-status=""true"" data-id=""1"" data-tip=""Add to Cart""><i class=""fa fa-shopping-basket"" aria-hidden=""true""></i></a></li>
                        </ul>
                    </div>
                </div>
                <div class=""col-md-3 col-6"">
                    <div class=""product-grid6"" id=""item_2"">
    ");
            WriteLiteral(@"                    <div class=""product-image6"">
                            <a href=""#"">
                                <img class=""pic-1"" src=""http://bestjquery.com/tutorial/product-grid/demo10/images/img-2.jpg"">
                            </a>
                        </div>
                        <div class=""product-content"">
                            <h3 class=""title""><a href=""#"">Women's Red Top</a></h3>
                            <div class=""price"">$8.00
                                <span>$12.00</span>
                            </div>
                        </div>
                        <ul class=""social"">
                            <li><a href="""" class=""js-show-modal1"" data-tip=""Quick View""><i class=""fa fa-search""></i></a></li>
                            <li><a href="""" class=""js-add-wishlist"" data-status=""true"" data-id=""2"" data-tip=""Add to Wishlist""><i class=""fal fa-heart""></i></a></li>
                            <li><a href="""" class=""js-add-cart"" data-status=""true"" data-id");
            WriteLiteral(@"=""2"" data-tip=""Add to Cart""><i class=""fa fa-shopping-basket"" aria-hidden=""true""></i></a></li>
                        </ul>
                    </div>
                </div>
                <div class=""col-md-3 col-6"">
                    <div class=""product-grid6"" id=""item_3"">
                        <div class=""product-image6"">
                            <a href=""#"">
                                <img class=""pic-1"" src=""http://bestjquery.com/tutorial/product-grid/demo10/images/img-3.jpg"">
                            </a>
                        </div>
                        <div class=""product-content"">
                            <h3 class=""title""><a href=""#"">Men's Shirt</a></h3>
                            <div class=""price"">$11.00
                                <span>$14.00</span>
                            </div>
                        </div>
                        <ul class=""social"">
                            <li><a href="""" class=""js-show-modal1"" data-tip=""Quick View""><i cla");
            WriteLiteral(@"ss=""fa fa-search""></i></a></li>
                            <li><a href="""" class=""js-add-wishlist"" data-status=""true"" data-id=""3"" data-tip=""Add to Wishlist""><i class=""fal fa-heart""></i></a></li>
                            <li><a href="""" class=""js-add-cart"" data-status=""true"" data-id=""3"" data-tip=""Add to Cart""><i class=""fa fa-shopping-basket"" aria-hidden=""true""></i></a></li>
                        </ul>
                    </div>
                </div>
                <div class=""col-md-3 col-6"">
                    <div class=""product-grid6"" id=""item_4"">
                        <div class=""product-image6"">
                            <a href=""#"">
                                <img class=""pic-1"" src=""http://bestjquery.com/tutorial/product-grid/demo10/images/img-4.jpg"">
                            </a>
                        </div>
                        <div class=""product-content"">
                            <h3 class=""title""><a href=""#"">Men's Shirt</a></h3>
                            <");
            WriteLiteral(@"div class=""price"">$11.00
                                <span>$14.00</span>
                            </div>
                        </div>
                        <ul class=""social"">
                            <li><a href="""" class=""js-show-modal1"" data-tip=""Quick View""><i class=""fa fa-search""></i></a></li>
                            <li><a href="""" class=""js-add-wishlist"" data-status=""true"" data-id=""4"" data-tip=""Add to Wishlist""><i class=""fal fa-heart""></i></a></li>
                            <li><a href="""" class=""js-add-cart"" data-status=""true"" data-id=""4"" data-tip=""Add to Cart""><i class=""fa fa-shopping-basket"" aria-hidden=""true""></i></a></li>
                        </ul>
                    </div>
                </div> -->


        </div>
    </div>
</div>
<div class=""product"">

    <div class=""container "">
        <div class=""row"">
");
            WriteLiteral("            <h2 class=\"title\">Sản phẩm hot</h2>\r\n");
#nullable restore
#line 146 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
             if (Model.products == null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h1>khong co san pham nao</h1>\r\n");
#nullable restore
#line 149 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-md-6\">\r\n");
#nullable restore
#line 153 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                     for (int i = 0; i < Model.products.Count; i++)
                    {

                        if (i <= 3 && Model.products.Count >= 3)
                        {


#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"col-md-6 col-6 product-item\" >\r\n                                <div class=\"product-grid6 \"");
            BeginWriteAttribute("id", " id=\"", 8248, "\"", 8279, 2);
            WriteAttributeValue("", 8253, "item_", 8253, 5, true);
#nullable restore
#line 160 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
WriteAttributeValue("", 8258, Model.products[i].Id, 8258, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-id=\"");
#nullable restore
#line 160 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                                                                                Write(Model.products[i].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                                    <div class=\"product-image6\">\r\n                                        <a");
            BeginWriteAttribute("href", " href=\"", 8423, "\"", 8463, 2);
            WriteAttributeValue("", 8430, "/chi-tiet/", 8430, 10, true);
#nullable restore
#line 162 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
WriteAttributeValue("", 8440, Model.products[i].Slug, 8440, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            <img class=\"pic-1\"");
            BeginWriteAttribute("src", " src=\"", 8529, "\"", 8614, 2);
            WriteAttributeValue("", 8535, "https://localhost:44360/user-content/", 8535, 37, true);
#nullable restore
#line 163 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
WriteAttributeValue("", 8572, Model.products[i].ThumbnailImage.FileName, 8572, 42, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 8615, "\"", 8645, 1);
#nullable restore
#line 163 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
WriteAttributeValue("", 8621, Model.products[i].Title, 8621, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        </a>\r\n                                    </div>\r\n                                    <div class=\"product-content\">\r\n                                        <h3 class=\"title\"><a href=\"#\">");
#nullable restore
#line 167 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                                                 Write(Model.products[i].Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h3>\r\n                                        <div>\r\n");
#nullable restore
#line 169 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                             if (@Model.products[i].Sale > 0)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span class=\"price\">$");
#nullable restore
#line 171 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                                            Write(Model.products[i].Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ₫- </span>\r\n                                            <span class=\"sale\">");
#nullable restore
#line 172 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                                          Write(Model.products[i].Sale);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ₫</span>\r\n");
#nullable restore
#line 173 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                            }
                                            else
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                             <span class=\"price\">$");
#nullable restore
#line 176 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                                             Write(Model.products[i].Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ₫</span>\r\n");
#nullable restore
#line 177 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </div>\r\n                                       \r\n                                    </div>\r\n                                    <ul class=\"social\">\r\n                                        <li><a");
            BeginWriteAttribute("href", " href=\"", 9816, "\"", 9856, 2);
            WriteAttributeValue("", 9823, "/chi-tiet/", 9823, 10, true);
#nullable restore
#line 183 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
WriteAttributeValue("", 9833, Model.products[i].Slug, 9833, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"js-show-modal1\" data-tip=\"Quick View\"><i class=\"fa fa-search\"></i></a></li>\r\n                                        <li><a");
            BeginWriteAttribute("href", " href=\"", 9988, "\"", 9995, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""js-add-wishlist"" data-status=""true"" data-id=""6"" data-tip=""Add to Wishlist""><i class=""fal fa-heart""></i></a></li>
                                        <li><a href=""javascript:void(0)"" class=""js-add-cart"" data-status=""true"" data-id=""6"" data-tip=""Add to Cart""><i class=""fa fa-shopping-basket"" aria-hidden=""true""></i>
                                            </a></li>
                                    </ul>
                                </div>
                            </div>
");
#nullable restore
#line 190 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                        }
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n                <div class=\"col-md-6 product-item\">\r\n                    <div class=\"product-grid6\"");
            BeginWriteAttribute("id", " id=\"", 10670, "\"", 10701, 2);
            WriteAttributeValue("", 10675, "item_", 10675, 5, true);
#nullable restore
#line 194 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
WriteAttributeValue("", 10680, Model.products[4].Id, 10680, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-id=\"");
#nullable restore
#line 194 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                                                                   Write(Model.products[4].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n");
            WriteLiteral(@"<div id=""clockdiv"">
                            <div>
                                <span class=""days""></span>
                                <div class=""smalltext""></div>
                            </div>
                            <div>
                                <span class=""hours""></span>
                                <div class=""smalltext""></div>
                            </div>
                            <div>
                                <span class=""minutes""></span>
                                <div class=""smalltext""></div>
                            </div>
                            <div>
                                <span class=""seconds""></span>
                                <div class=""smalltext""></div>
                            </div>
                        </div>

                        <div class=""product-image6"">
                            <a");
            BeginWriteAttribute("href", " href=\"", 12272, "\"", 12312, 2);
            WriteAttributeValue("", 12279, "/chi-tiet/", 12279, 10, true);
#nullable restore
#line 232 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
WriteAttributeValue("", 12289, Model.products[4].Slug, 12289, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                <img class=\"pic-1\"");
            BeginWriteAttribute("src", " src=\"", 12366, "\"", 12451, 2);
            WriteAttributeValue("", 12372, "https://localhost:44360/user-content/", 12372, 37, true);
#nullable restore
#line 233 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
WriteAttributeValue("", 12409, Model.products[4].ThumbnailImage.FileName, 12409, 42, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            </a>\r\n                        </div>\r\n                        <div class=\"product-content\">\r\n                            <h3 class=\"title\"><a href=\"#\">");
#nullable restore
#line 237 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                                     Write(Model.products[4].Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h3>\r\n                            <div>\r\n");
#nullable restore
#line 239 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                 if (@Model.products[4].Sale > 0)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"price\">$");
#nullable restore
#line 241 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                                    Write(Model.products[4].Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ₫- </span>\r\n                                    <span class=\"sale\">");
#nullable restore
#line 242 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                                  Write(Model.products[4].Sale);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ₫</span>\r\n");
#nullable restore
#line 243 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"price\">$");
#nullable restore
#line 246 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                                    Write(Model.products[4].Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ₫</span>\r\n");
#nullable restore
#line 247 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n\r\n                        </div>\r\n                        <ul class=\"social\">\r\n                            <li><a");
            BeginWriteAttribute("href", " href=\"", 13378, "\"", 13418, 2);
            WriteAttributeValue("", 13385, "/chi-tiet/", 13385, 10, true);
#nullable restore
#line 253 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
WriteAttributeValue("", 13395, Model.products[4].Slug, 13395, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"js-show-modal1\" data-tip=\"Quick View\"><i class=\"fa fa-search\"></i></a></li>\r\n                            <li><a");
            BeginWriteAttribute("href", " href=\"", 13538, "\"", 13545, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""js-add-wishlist"" data-status=""true"" data-id=""6"" data-tip=""Add to Wishlist""><i class=""fal fa-heart""></i></a></li>
                            <li><a href=""javascript:void(0)"" class=""js-add-cart"" data-status=""true"" data-id=""6"" data-tip=""Add to Cart""><i class=""fa fa-shopping-basket"" aria-hidden=""true""></i></a></li>
                        </ul>
                        <span class=""product-new-label"">Sale</span>
                        <span class=""product-discount-label"">20%</span>
                    </div>

                </div>  
");
#nullable restore
#line 262 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Home\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n<div id=\"product-hot\" class=\"product\">\r\n\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProductList> Html { get; private set; }
    }
}
#pragma warning restore 1591
