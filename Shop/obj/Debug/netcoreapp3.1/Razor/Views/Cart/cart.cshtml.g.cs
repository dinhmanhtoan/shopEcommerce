#pragma checksum "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8e21bd1f26afb7bfb81228f5255eeb5b37b2d4a3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cart_cart), @"mvc.1.0.view", @"/Views/Cart/cart.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8e21bd1f26afb7bfb81228f5255eeb5b37b2d4a3", @"/Views/Cart/cart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fe425b70ed0e4808eadefa6fc69a5d5cbf4cc194", @"/Views/_ViewImports.cshtml")]
    public class Views_Cart_cart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CheckoutViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("return-shop"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Product", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "cart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "checkout", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-black"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n<h2>GIỎ HÀNG</h2>\r\n");
            DefineSection("head", async() => {
                WriteLiteral("\r\n    <link rel=\"stylesheet\" href=\"../css/cart.css\">\r\n");
            }
            );
#nullable restore
#line 7 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
 if (Model.CartItems.Count > 0)
{
    decimal? total = 0;
    int stt = 1;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<main>
    <div id=""woocommerce"">

    </div>
    <div id=""cart"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-md-8"">
                    <div class=""cart"">
                        <table class=""table"">
                            <thead>
                                <tr>
                                    <td scope=""col""><span class=""hidden-sm-down "">Sản phẩm</span></td>
                                    <td scope=""col"" width=""120"" class=""hidden-sm-down "">Giá</td>
                                    <td scope=""col"" width=""100"">Số lượng</td>
                                    <td scope=""col"" width=""120"" class=""hidden-sm-down "">Tổng</td>
                                </tr>
                            </thead>
                            <tbody>
");
#nullable restore
#line 30 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                 foreach (var item in Model.CartItems)
                                {
                                    decimal? thanhtien = 0;
                                    if (item.product.Sale == null)
                                    {
                                        thanhtien += item.quantity * item.product.Price;
                                        total += thanhtien;
                                    }
                                    else
                                    {
                                        thanhtien += item.quantity * item.product.Sale;
                                        total += thanhtien;
                                    }


#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr class=\"item-in-cart\" data-id=\"");
#nullable restore
#line 44 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                 Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""">
                                        <td>
                                            <div class=""cart-item-details"" style=""display:flex;"">
                                                <div>
                                                    <a href=""../html/single.html""><img");
            BeginWriteAttribute("src", " src=\"", 2133, "\"", 2207, 2);
            WriteAttributeValue("", 2139, "https://localhost:5002/user-content/", 2139, 36, true);
#nullable restore
#line 48 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
WriteAttributeValue("", 2175, item.product.Thumbnail.FileName, 2175, 32, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></a>\r\n                                                </div>\r\n\r\n\r\n                                                <div class=\"cart-item-price hidden-md-up\">\r\n                                                    <a href=\"../html/single.html\">");
#nullable restore
#line 53 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                             Write(item.product.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                                                    <p class=\"no-margin\">\r\n                                                        <span>");
#nullable restore
#line 55 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                         Write(item.quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> X <span class=\"price\">\r\n");
#nullable restore
#line 56 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                             if (item.product.Sale == null)
                                                            {
                                                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                            Write(item.product.Price);

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                                     
                                                            }
                                                            else
                                                            {
                                                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                            Write(item.product.Sale);

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                                    
                                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                        </span>
                                                    </p>
                                                    <a class=""remove-in-cart"">Remove</a>
                                                </div>

                                            </div>
                                        </td>
                                        <td class=""hidden-sm-down"">
                                            <div class=""cart-item-price"">
");
#nullable restore
#line 73 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                 if (item.product.Sale == null)
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <span style=\"color:black\">");
#nullable restore
#line 75 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                         Write(item.product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 76 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                }
                                                else
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <span style=\"color:black\">");
#nullable restore
#line 79 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                         Write(item.product.Sale);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                                    <i class=\"fa fa-caret-left\" aria-hidden=\"true\"></i>\r\n                                                    <span><del>");
#nullable restore
#line 81 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                          Write(item.product.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</del></span>\r\n");
#nullable restore
#line 82 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                            </div>
                                        </td>
                                        <td class=""text-center"">
                                            <div class=""cart-item-quantity"">
                                                <div class=""quantity"">
                                                    <div class=""wrap-num-product"">
                                                        <div class=""btn-num-product-down"">
                                                            <i class=""fa fa-minus"" aria-hidden=""true""></i>
                                                        </div>
                                                        <input class=""txt-center num-product"" type=""number"" name=""num-product""");
            BeginWriteAttribute("value", " value=\"", 5298, "\"", 5320, 1);
#nullable restore
#line 93 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
WriteAttributeValue("", 5306, item.quantity, 5306, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">

                                                        <div class=""btn-num-product-up"">
                                                            <i class=""fa fa-plus"" aria-hidden=""true""></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                        <td class=""hidden-sm-down"">
");
#nullable restore
#line 103 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                             if (item.product.Sale == null)
                                            {
                                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 105 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                            Write(item.product.Price * item.quantity);

#line default
#line hidden
#nullable disable
#nullable restore
#line 105 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                                     
                                            }
                                            else
                                            {
                                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 109 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                            Write(item.product.Sale * item.quantity);

#line default
#line hidden
#nullable disable
#nullable restore
#line 109 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                                    
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        </td>\r\n                                    </tr>\r\n");
#nullable restore
#line 113 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </tbody>\r\n                        </table>\r\n                    </div>\r\n\r\n                    <hr>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8e21bd1f26afb7bfb81228f5255eeb5b37b2d4a317160", async() => {
                WriteLiteral("<i class=\"fa fa-angle-left\" aria-hidden=\"true\"></i> Tiếp tục mua hàng ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                </div>
                <div class=""col-md-4 total"">
                    <div class=""box"">

                        <h3 class=""title"">Tổng đơn hàng</h3>

                        <table class=""table"">
                            <tbody>
                                <tr>
                                    <td>Tổng</td>
                                    <td class=""text-right"">");
#nullable restore
#line 131 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                      Write(total);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" <small>₫</small></td>
                                </tr>
                                <tr>
                                    <td>Phí vận chuyển</td>
                                    <td class=""text-right"">25.000 <small>₫</small></td>
                                </tr>
                                <tr>
                                    <td colspan=""2"">
                                        Mã giảm giá
                                        <br>
                                        <input class=""input_style1 m_t_10"" type=""text"" name=""discount"">
                                    </td>

                                </tr>
                                <tr>
                                    <td>Thanh toán</td>
                                    <td class=""text-right"">");
#nullable restore
#line 147 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                       Write(total + 25000);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <small>₫</small></td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8e21bd1f26afb7bfb81228f5255eeb5b37b2d4a320570", async() => {
                WriteLiteral("Thanh toán");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

                    </div>
                </div>
            </div>

        </div>

    </div>
    <div class=""clearfix""></div>
    <div class=""product"">
        <div class=""container"">
            <div class=""row"">
                <h2 class=""title"">Sản phẩm vừa xem</h2>
");
#nullable restore
#line 165 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                 if (Model.Viewed.Count > 0)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 167 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                     foreach (var item in Model.Viewed)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"col-md-3 col-6 item-product\">\r\n                            <div class=\"product-grid6\">\r\n                                <div class=\"product-image6\">\r\n                                    <a");
            BeginWriteAttribute("href", " href=\"", 8931, "\"", 8958, 2);
            WriteAttributeValue("", 8938, "/chi-tiet/", 8938, 10, true);
#nullable restore
#line 172 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
WriteAttributeValue("", 8948, item.Slug, 8948, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        <img class=\"pic-1\"");
            BeginWriteAttribute("src", " src=\"", 9020, "\"", 9086, 2);
            WriteAttributeValue("", 9026, "https://localhost:5002/user-content/", 9026, 36, true);
#nullable restore
#line 173 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
WriteAttributeValue("", 9062, item.Thumbnail.FileName, 9062, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    </a>\r\n                                </div>\r\n                                <div class=\"product-content\">\r\n                                    <h3 class=\"title\"><a");
            BeginWriteAttribute("href", " href=\"", 9291, "\"", 9317, 2);
            WriteAttributeValue("", 9298, "chi-tiet/", 9298, 9, true);
#nullable restore
#line 177 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
WriteAttributeValue("", 9307, item.Slug, 9307, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 177 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                               Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h3>\r\n                                    <div>\r\n");
#nullable restore
#line 179 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                         if (item.Sale > 0)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span>");
#nullable restore
#line 181 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                             Write(item.Sale);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ₫</span>\r\n                                            <i class=\"fa fa-caret-left\" aria-hidden=\"true\"></i>\r\n                                            <span class=\"sale\"><del>");
#nullable restore
#line 183 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                               Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ₫</del></span>\r\n");
#nullable restore
#line 184 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <span><del>");
#nullable restore
#line 187 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                  Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ₫</del></span>\r\n");
#nullable restore
#line 188 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </div>\r\n                                </div>\r\n                                <ul class=\"social\">\r\n                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 10189, "\"", 10216, 2);
            WriteAttributeValue("", 10196, "/chi-tiet/", 10196, 10, true);
#nullable restore
#line 192 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
WriteAttributeValue("", 10206, item.Slug, 10206, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"js-show-modal1\" data-id=\"");
#nullable restore
#line 192 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                                                  Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-tip=\"Quick View\"><i class=\"fa fa-search\"></i></a></li>\r\n                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 10363, "\"", 10370, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"js-add-wishlist\" data-status=\"true\" data-id=\"");
#nullable restore
#line 193 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                                                  Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-tip=\"Add to Wishlist\"><i class=\"fal fa-heart\"></i></a></li>\r\n                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 10542, "\"", 10549, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"js-add-cart\" data-status=\"true\" data-id=\"");
#nullable restore
#line 194 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                                                                              Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-tip=\"Add to Cart\"><i class=\"fa fa-shopping-basket\" aria-hidden=\"true\"></i></a></li>\r\n                                </ul>\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 198 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 198 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n</main>\r\n");
#nullable restore
#line 207 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p class=\"alert alert-danger\">Giỏ hàng trống</p>\r\n");
#nullable restore
#line 211 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
}

#line default
#line hidden
#nullable disable
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
<script>
    $(document).on('click', '.remove-in-cart', function (e) {
        var parent = $(this).parents('.item-in-cart');
            var id = parseInt(parent.attr('data-id'));
            $.ajax({
                type: 'POST',
                url: '");
#nullable restore
#line 219 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                 Write(Url.ActionLink("RemoveCart","Cart"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\',\r\n                data: {\r\n                    productid: id,\r\n                },\r\n                success: function (result) {\r\n                    //  addtocart();\r\n                    window.location.href = \'");
#nullable restore
#line 225 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                       Write(Url.ActionLink("cart","cart"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
                }
            })
        });
    $(document).on('change', '.num-product', function () {
        var parent = $(this).parents('.item-in-cart');
        var id = parseInt(parent.attr('data-id'));
        var value = parseInt($(this).val());
        console.log(value);
        if (value == null || value == '') {
            (this).val();
        } else {
            $.ajax({
                type: 'POST',
                url: '");
#nullable restore
#line 239 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                 Write(Url.ActionLink("UpdateCart", "Cart"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                data: {
                    productid: id,
                    quantity: value,
                },
                success: function (result) {
                    //addtocart();
                            window.location.href = '");
#nullable restore
#line 246 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                               Write(Url.ActionLink("cart","cart"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
                }
            });
        }
    })
    $(document).on('click', '.fa-plus', function () {
        var parent = $(this).parents('.item-in-cart');
        var id = parseInt(parent.attr('data-id'));
        var value = parseInt(parent.find('.num-product').val());
        value++;
        $.ajax({
            type: 'POST',
            url: '");
#nullable restore
#line 258 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
             Write(Url.ActionLink("UpdateCart", "Cart"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\',\r\n            data: {\r\n                productid: id,\r\n                quantity: value,\r\n            },\r\n            success: function (result) {\r\n                // addtocart();\r\n                        window.location.href = \'");
#nullable restore
#line 265 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                           Write(Url.ActionLink("cart","cart"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
            }
        });
    });
    $(document).on('click', '.fa-minus', function () {
        var parent = $(this).parents('.item-in-cart');
        var id = parseInt(parent.attr('data-id'));
        var value = parseInt(parent.find('.num-product').val());
        if (value == 1) {
            $.ajax({
                type: 'POST',
                url: '");
#nullable restore
#line 276 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                 Write(Url.ActionLink("RemoveCart", "Cart"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                data: {
                    productid: id,
                },
                success: function (result) {
                    addtocart();
                }
            });
        } else
        {
            value--;
            $.ajax({
                type: 'POST',
                url: '");
#nullable restore
#line 289 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                 Write(Url.ActionLink("UpdateCart", "Cart"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\',\r\n                data: {\r\n                    productid: id,\r\n                    quantity: value,\r\n                },\r\n                success: function (result) {\r\n                    // addtocart();\r\n                    window.location.href = \'");
#nullable restore
#line 296 "I:\SoureToan\Shop\shop2\Shop\Views\Cart\cart.cshtml"
                                       Write(Url.ActionLink("cart","cart"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n                }\r\n            });\r\n        }\r\n    });\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CheckoutViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
