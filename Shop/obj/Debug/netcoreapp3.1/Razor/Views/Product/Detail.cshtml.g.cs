#pragma checksum "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "42f35b6b68b956b6c9110355df98aea6210941a3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_Detail), @"mvc.1.0.view", @"/Views/Product/Detail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42f35b6b68b956b6c9110355df98aea6210941a3", @"/Views/Product/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fe425b70ed0e4808eadefa6fc69a5d5cbf4cc194", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DetailsVm>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/single.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "cart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "checkout", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-black"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("main-review-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("contact-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("contact-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Rating", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Add", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/single.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
  
    ViewData["Title"] = Model.product.Title;

#line default
#line hidden
#nullable disable
            DefineSection("head", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "42f35b6b68b956b6c9110355df98aea6210941a38371", async() => {
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
                WriteLiteral("\r\n\r\n");
            }
            );
            WriteLiteral("<div class=\"single\">\r\n    <div class=\"container\">\r\n\r\n        <div class=\"row\">\r\n            <div class=\"col-md-9\">\r\n                <div class=\"col-md-6 single-left gallery-lb\">\r\n");
#nullable restore
#line 15 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                     for (var i = 0; i < Model.product.Images.Count; i++)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"mySlides\" style=\"display: none;\">\r\n                            <div class=\"numbertext\">");
#nullable restore
#line 18 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                Write(i+1);

#line default
#line hidden
#nullable disable
            WriteLiteral(" / ");
#nullable restore
#line 18 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                        Write(Model.product.Images.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                            <a class=\"zoom \"");
            BeginWriteAttribute("href", " href=\"", 636, "\"", 719, 2);
            WriteAttributeValue("", 643, "https://localhost:44360/user-content/", 643, 37, true);
#nullable restore
#line 19 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
WriteAttributeValue("", 680, Model.product.Images[i].Media.FileName, 680, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" tabindex=\"0\">\r\n                                <i class=\"fal fa-expand\"></i>\r\n                            </a>\r\n                            <img");
            BeginWriteAttribute("src", " src=\"", 865, "\"", 947, 2);
            WriteAttributeValue("", 871, "https://localhost:44360/user-content/", 871, 37, true);
#nullable restore
#line 22 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
WriteAttributeValue("", 908, Model.product.Images[i].Media.FileName, 908, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"width:100%\">\r\n                        </div>\r\n");
#nullable restore
#line 24 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"

                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <button class=""img-prev"" onclick=""plusSlides(-1)""><i class=""fal fa-angle-left"" aria-hidden=""true""></i></button>
                    <button class=""img-next"" onclick=""plusSlides(1)""><i class=""fal fa-angle-right"" aria-hidden=""true""></i></button>

                    <div class=""image-slide"">
");
#nullable restore
#line 30 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                         for (var i = 0; i < Model.product.Images.Count; i++)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"column\">\r\n                                <img class=\"demo cursor\"");
            BeginWriteAttribute("src", " src=\"", 1554, "\"", 1636, 2);
            WriteAttributeValue("", 1560, "https://localhost:44360/user-content/", 1560, 37, true);
#nullable restore
#line 33 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
WriteAttributeValue("", 1597, Model.product.Images[i].Media.FileName, 1597, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"width:100%\" onclick=\"currentSlide(1)\"");
            BeginWriteAttribute("alt", " alt=\"", 1682, "\"", 1708, 1);
#nullable restore
#line 33 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
WriteAttributeValue("", 1688, Model.product.Title, 1688, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            </div>\r\n");
#nullable restore
#line 35 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"

                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n\r\n\r\n                </div><!--Hình ảnh-->\r\n\r\n                <div class=\"col-md-6 single-right pd-xs-0 product-details\">\r\n                    <div class=\" product-details-item\" data-id=\"");
#nullable restore
#line 43 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                           Write(Model.product.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                        <h1 class=\"single-title\">");
#nullable restore
#line 44 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                            Write(Model.product.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n                        <p class=\"price\">\r\n");
#nullable restore
#line 46 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                             if (Model.product.Sale > 0)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <span>");
#nullable restore
#line 48 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                 Write(Model.product.Sale.GetValueOrDefault(0).ToString("#,#.##"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </span> <del>");
#nullable restore
#line 48 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                                                                          Write(Model.product.Price.GetValueOrDefault(0).ToString("#,#.##"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</del>\r\n");
#nullable restore
#line 49 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                            }
                            else
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 52 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                           Write(Model.product.Price.GetValueOrDefault(0).ToString("#,#.##"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 52 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                                                            
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            VNĐ\r\n                        </p>\r\n                        <div class=\"product-rating\">\r\n                            <span class=\"rating star-rating\"> ");
#nullable restore
#line 57 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                         Write(Model.product.Rating.Average(x => x.Scores));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n\r\n                            <a href=\"#reviews\" class=\"review-link\" rel=\"nofollow\">(<span class=\"count\">");
#nullable restore
#line 59 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                                                                  Write(Model.product.Rating.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> nhận xét)</a>\r\n                        </div>\r\n                        <div itemprop=\"description\" class=\"short-description\">\r\n                            <p>");
#nullable restore
#line 62 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                          Write(Model.product.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                        </div>

                        <!--Số lượng-->
                        <div class=""quantity"">
                            <div class=""wrap-num-product"">
                                <div class=""btn-num-product-down"">
                                    <i class=""fal fa-minus"" aria-hidden=""true""></i>
                                </div>
                                <input class=""txt-center num-product"" type=""number"" name=""num-product"" value=""1"" data-val=""1"">

                                <div class=""btn-num-product-up"">
                                    <i class=""fal fa-plus"" aria-hidden=""true""></i>
                                </div>
                            </div>
                        </div>

                        <!--Thêm giỏ hàng-->
                        <div class=""add-cart "" style=""display:flex;"">
                            <a href=""#"" class=""btn btn-black""> <span><i class=""fal fa-shopping-basket"" aria-hidden=""true""></i></span>Th");
            WriteLiteral("êm giỏ hàng</a>\r\n\r\n                        </div>\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "42f35b6b68b956b6c9110355df98aea6210941a319127", async() => {
                WriteLiteral("Thanh toán");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                        <div class=""clearfix""></div>
                        <div class=""add-wishlist"">
                            <a href=""#""> <span><i class=""fal fa-heart"" aria-hidden=""true""></i></span>Thêm danh sách yêu thích</a>
                        </div>

                        <div class=""clearfix""></div>
                        <div class=""product-meta"">
                            <span class=""product_brand"">
                                Thương hiệu:
                                <a href=""#""></a>
                            </span>
                            <span class=""product_sku"">
                                Xuất xứ:
                                <a href=""#"">N/A</a>
                            </span>
                            <span class=""product_category"">
                                Danh mục:
                                <a href=""#"">");
#nullable restore
#line 102 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                       Write(Model.product.Category.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a>
                            </span>
                            <span class=""product_share"">
                                Chia sẻ:
                                <a href=""#"" class=""share-facebook"" style=""color: #025aa5 !important""><i class=""fa fa-facebook-official"" aria-hidden=""true""></i></a>
                            </span>
                        </div>
                        <div class=""clearfix""></div>
                    </div>


                </div><!--Thông tin cơ bản-->
            </div>
            <div class=""col-md-3"">
                <div class=""single-product-sidebar"">
                    <span class=""title"">Có thể bạn thích</span>
                    <div class=""div-btn"">
                        <ul>
                            <li><a href=""#""><i class=""fal fa-angle-left"" aria-hidden=""true""></i></a></li>
                            <li><a href=""#""><i class=""fal fa-angle-right"" aria-hidden=""true""></i></a></li>
                        </ul>
                    ");
            WriteLiteral("</div>\r\n                    <hr>\r\n                    <ul class=\"owl-carousel owl-theme\">\r\n");
#nullable restore
#line 126 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                         foreach (var item in Model.products)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <li class=\"item\">\r\n                                <a");
            BeginWriteAttribute("href", " href=\"", 6579, "\"", 6606, 2);
            WriteAttributeValue("", 6586, "/chi-tiet/", 6586, 10, true);
#nullable restore
#line 129 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
WriteAttributeValue("", 6596, item.Slug, 6596, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><img");
            BeginWriteAttribute("src", " src=\"", 6612, "\"", 6679, 2);
            WriteAttributeValue("", 6618, "https://localhost:44360/user-content/", 6618, 37, true);
#nullable restore
#line 129 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
WriteAttributeValue("", 6655, item.Thumbnail.FileName, 6655, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"width: 70px\"></a>\r\n                                <div class=\"product-item-right\">\r\n                                    <p class=\"product-title\">\r\n                                        <a");
            BeginWriteAttribute("href", " href=\"", 6878, "\"", 6905, 2);
            WriteAttributeValue("", 6885, "/chi-tiet/", 6885, 10, true);
#nullable restore
#line 132 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
WriteAttributeValue("", 6895, item.Slug, 6895, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" title=\"The Trend Polo\">");
#nullable restore
#line 132 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                                                         Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                                    </p>\r\n");
#nullable restore
#line 134 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                     if (item.Sale > 0)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <p class=\"price\"><span>");
#nullable restore
#line 136 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                          Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ₫</span> - <del>");
#nullable restore
#line 136 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                                                      Write(item.Sale);

#line default
#line hidden
#nullable disable
            WriteLiteral("</del><small>₫</small> </p>\r\n");
#nullable restore
#line 137 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <p class=\"price\"><span>");
#nullable restore
#line 140 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                          Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ₫</span> </p>\r\n");
#nullable restore
#line 141 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </div>\r\n                            </li>\r\n");
#nullable restore
#line 145 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


                    </ul>
                </div>
                <div class=""Utilities-block"">
                    <span class=""title"">Tiện ích</span>
                    <hr>
                    <table>
                        <tbody>
                            <tr>
                                <td class=""size-45"" style=""font-size: 35px""><i class=""fal fa-usd-circle"" aria-hidden=""true""></i></td>
                                <td>Tiết kiệm tiền cho những đơn hàng tiếp theo</td>
                            </tr>
                            <tr>
                                <td class=""size-45"" style=""font-size: 35px""><i class=""fal fa-truck"" aria-hidden=""true""></i></td>
                                <td>Giao hàng toàn quốc</td>
                            </tr>

                        </tbody>
                    </table>
                </div>
            </div>

            <div class=""product-information col-md-12"">
                <div class=""div_link"">
               ");
            WriteLiteral(@"     <ul class=""nav"">
                        <li><a href=""#information"" class=""active"" data-toggle=""tab"" role=""tab"">Thông tin thêm</a></li>
                        <li><a href=""#reviews"" data-toggle=""tab"" role=""tab"">Nhận xét</a></li>
                    </ul>
                </div>
                <div class=""tab-content"">
                    <div id=""information"" class=""tab-pane fade in active show"" role=""tabpanel"" style=""min-height: 300px"">
                        ");
#nullable restore
#line 179 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                   Write(Html.Raw(Model.product.Detail));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    </div>
                    <div id=""reviews"" class=""tab-pane fade in"" role=""tabpanel"" style=""min-height: 300px"">
                        <div class=""col-md-6 list-review"">
                            <div class=""row"">
                                <h1 class=""title"">Đánh giá cho sản phẩm này</h1>
                                <div>
                                    <ul>
");
#nullable restore
#line 187 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                         foreach (var rating in Model.product.Rating)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <li>\r\n                                                <div class=\"review-avatar\"><a");
            BeginWriteAttribute("href", " href=\"", 9766, "\"", 9773, 0);
            EndWriteAttribute();
            WriteLiteral(@"><img src=""../img/user.png""></a></div>
                                                <div class=""review-content"">
                                                    <div class=""me-information"">
                                                        <span class=""me-name"">");
#nullable restore
#line 193 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                                         Write(rating.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                                        <span class=\"me-rating\">\r\n                                                            <span class=\"rating star-rating\">");
#nullable restore
#line 195 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                                                        Write(rating.Scores);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span>
                                                        </span>
                                                        <div class=""clearfix""></div>
                                                    </div>

                                                    <p>");
#nullable restore
#line 200 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                  Write(rating.Content);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                                    <time>");
#nullable restore
#line 201 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
                                                     Write(rating.CreatedOn.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy hh:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</time>\r\n                                                </div>\r\n                                                <div class=\"clearfix\"></div>\r\n                                            </li>\r\n");
#nullable restore
#line 205 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"

                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class=""col-md-6 add-review"">
                            <div class=""row"">
                                <h1 class=""title"">Thêm đánh giá</h1>

                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "42f35b6b68b956b6c9110355df98aea6210941a332829", async() => {
                WriteLiteral(@"
                                    <div class=""review-rating"">
                                        <label>Xếp hạng của bạn</label>
                                        <span class=""wrap-rating fs-18 cl11 pointer"">
                                            <i class=""fa fa-star zmdi zmdi-star-outline"" aria-hidden=""true""></i>
                                            <i class=""fa fa-star zmdi zmdi-star-outline"" aria-hidden=""true""></i>
                                            <i class=""fa fa-star zmdi zmdi-star-outline"" aria-hidden=""true""></i>
                                            <i class=""fa fa-star zmdi zmdi-star-outline"" aria-hidden=""true""></i>
                                            <i class=""fa fa-star zmdi zmdi-star-outline"" aria-hidden=""true""></i>

                                            <input type=""number"" name=""Scores"" hidden>
                                        </span>
                                    </div>
                                    <input t");
                WriteLiteral("ype=\"text\" name=\"productId\"");
                BeginWriteAttribute("value", " value=\"", 12473, "\"", 12498, 1);
#nullable restore
#line 229 "D:\souc\source\.net\Shop2\shop2\Shop\Views\Product\Detail.cshtml"
WriteAttributeValue("", 12481, Model.product.Id, 12481, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" readonly hidden>
                                    <input type=""text"" name=""name"" required=""required"" placeholder=""Tên *"">

                                    <input type=""email"" name=""email"" required=""required"" placeholder=""Email *"">
                                    <textarea name=""content"" id=""message"" required=""required"" class=""count-content"" rows=""3"" placeholder=""Nội dung *""></textarea>
                                    <span class=""views-count""></span>

                                    <br>
                                    <p class=""note"">* trường dữ liệu bắt buộc</p>

                                    <button type=""submit"" name=""submit"" class=""btn btn-black"">Gửi đi</button>

                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "42f35b6b68b956b6c9110355df98aea6210941a337499", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
    <script>
        $("".owl-carousel"").owlCarousel({
            autoplay: false, // tự động chuyển slider
            //loop: true,
            items: 1,
            nav: true, // hiện thị bật tắt nút next,back
            dots: false,
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DetailsVm> Html { get; private set; }
    }
}
#pragma warning restore 1591
