#pragma checksum "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d460ec340ef4ca42b13836477ae898eabbc35fc1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_Index), @"mvc.1.0.view", @"/Views/Product/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d460ec340ef4ca42b13836477ae898eabbc35fc1", @"/Views/Product/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fe425b70ed0e4808eadefa6fc69a5d5cbf4cc194", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProductList>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "title-asc", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "title-desc", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "price-asc", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "price-desc", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "12", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "24", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "36", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "-1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
  
    ViewData["Title"] = "Sản phẩm";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<main>
    <div class=""shop_breadcrumb"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-md-12"">
                    <nav>
                        <a href=""../html/home.html"">Trang chủ</a>

                        <span class=""delimeter""><i class=""fa fa-angle-right"" aria-hidden=""true""></i></span>
                        <h1 class=""title"">Sản phẩm</h1>
                    </nav>

                    <a class=""back-history"" href=""javascript: history.go(-1)""><i class=""fa fa-angle-left"" aria-hidden=""true""></i> Quay lại trang trước</a>
                </div>
                <div class=""clearfix""></div>
            </div>
        </div>
    </div>
    <div class=""product"">
        <div class=""container"">

            <div class=""row"">
                <div class=""col-xl-3"">
                    <div class=""row"">
                        <div class=""panel-group category-products"" id=""accordian"">
                            <!--category-productsr-->");
            WriteLiteral("\n                            <h3 class=\"side-title\">Danh mục</h3>\r\n");
#nullable restore
#line 32 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                             if (Model.categories != null)
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                 foreach (var item in Model.categories)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <div class=\"form-check\">\r\n                                        <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("id", " id=\"", 1504, "\"", 1526, 2);
            WriteAttributeValue("", 1509, "category-", 1509, 9, true);
#nullable restore
#line 37 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
WriteAttributeValue("", 1518, item.Id, 1518, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"category\"");
            BeginWriteAttribute("value", " value=\"", 1543, "\"", 1559, 1);
#nullable restore
#line 37 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
WriteAttributeValue("", 1551, item.Id, 1551, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        <label class=\"form-check-label\"");
            BeginWriteAttribute("for", " for=\"", 1634, "\"", 1657, 2);
            WriteAttributeValue("", 1640, "category-", 1640, 9, true);
#nullable restore
#line 38 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
WriteAttributeValue("", 1649, item.Id, 1649, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 38 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                                                                           Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                                    </div>\r\n");
#nullable restore
#line 40 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 40 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                 
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </div><!--/category-productsr-->

                        <div class=""price-range"">
                            <!--price-range-->
                            <h3 class=""side-title"">Lọc theo giá</h3>
                            <div class=""well"">
                                <input type=""text"" class=""span2""");
            BeginWriteAttribute("value", " value=\"", 2131, "\"", 2139, 0);
            EndWriteAttribute();
            WriteLiteral(" data-slider-min=\"0\" data-slider-max=\"2000000\" data-slider-step=\"5\" data-slider-value=\"[0,2000000]\" id=\"sl2\"");
            BeginWriteAttribute("style", " style=\"", 2248, "\"", 2256, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                            </div>
                            <br>
                            <b>$ 0</b> <b class=""pull-right"">$ 2.000.000</b>
                            <p id=""amount"">Giá (VNĐ):</p>
                        </div>
                        <div id=""brands"">
                            <h3 class=""side-title"">Thương hiệu</h3>
                            <div class=""panel panel-default"">
                                <input id=""brands_input"" style=""display:none"" type=""text"" name=""brands"">
                                <ul>
                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 2860, "\"", 2867, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"js-choose-brands\" data-status=\"0\"><span class=\"brands_icon\"><i class=\"fa fa-square-o\" aria-hidden=\"true\"></i></span>Zara </a></li>\r\n                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 3050, "\"", 3057, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"js-choose-brands\" data-status=\"0\"><span class=\"brands_icon\"><i class=\"fa fa-square-o\" aria-hidden=\"true\"></i></span>Nike </a></li>\r\n                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 3240, "\"", 3247, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"js-choose-brands\" data-status=\"0\"><span class=\"brands_icon\"><i class=\"fa fa-square-o\" aria-hidden=\"true\"></i></span>Under Armour </a></li>\r\n                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 3438, "\"", 3445, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"js-choose-brands\" data-status=\"0\"><span class=\"brands_icon\"><i class=\"fa fa-square-o\" aria-hidden=\"true\"></i></span>Adidas </a></li>\r\n                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 3630, "\"", 3637, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"js-choose-brands\" data-status=\"0\"><span class=\"brands_icon\"><i class=\"fa fa-square-o\" aria-hidden=\"true\"></i></span>Puma</a></li>\r\n                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 3819, "\"", 3826, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""js-choose-brands"" data-status=""0""><span class=""brands_icon""><i class=""fa fa-square-o"" aria-hidden=""true""></i></span>ASICS </a></li>
                                </ul>
                            </div>
                        </div>
                    </div>

                </div>
                <div class=""col-xl-9"">
                    <div class=""row"">
                        <div class=""col-md-12"">
                            <div class=""custom-select"" style=""width: 160px;"">
                                <select class=""sort"" name=""Product"">
                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d460ec340ef4ca42b13836477ae898eabbc35fc113445", async() => {
                WriteLiteral("Sắp xếp");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d460ec340ef4ca42b13836477ae898eabbc35fc114441", async() => {
                WriteLiteral("AZ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d460ec340ef4ca42b13836477ae898eabbc35fc115633", async() => {
                WriteLiteral("ZA");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d460ec340ef4ca42b13836477ae898eabbc35fc116825", async() => {
                WriteLiteral("Tăng dần");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d460ec340ef4ca42b13836477ae898eabbc35fc118023", async() => {
                WriteLiteral("Giảm dần");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                </select>
                            </div>
                            <div class=""custom-select"" style="" width: 100px"">
                                <select id=""themes"" name=""pageSize"">
                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d460ec340ef4ca42b13836477ae898eabbc35fc119456", async() => {
                WriteLiteral("12");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d460ec340ef4ca42b13836477ae898eabbc35fc120648", async() => {
                WriteLiteral("24");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d460ec340ef4ca42b13836477ae898eabbc35fc121840", async() => {
                WriteLiteral("36");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d460ec340ef4ca42b13836477ae898eabbc35fc123032", async() => {
                WriteLiteral("Tất cả");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </select>\r\n                            </div>\r\n\r\n\r\n\r\n                            <div id=\"list-product-all\" class=\"product row\">\r\n");
#nullable restore
#line 95 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                 if (Model.products == null)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <h1> Hien chua co san pham nao</h1>\r\n");
#nullable restore
#line 98 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                }
                                else
                                {
                                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 101 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                     foreach (var item in Model.products)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                        <div class=""col-md-4 col-6"">
                                            <div class=""product-grid6"" id=""item_6"">
                                                <div class=""product-image6"">
                                                    <a");
            BeginWriteAttribute("src", " src=\"", 6127, "\"", 6158, 2);
            WriteAttributeValue("", 6133, "/home/details?Id=", 6133, 17, true);
#nullable restore
#line 106 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
WriteAttributeValue("", 6150, item.Id, 6150, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                                        <img class=\"pic-1\"");
            BeginWriteAttribute("src", " src=\"", 6236, "\"", 6308, 2);
            WriteAttributeValue("", 6242, "https://localhost:44360/user-content/", 6242, 37, true);
#nullable restore
#line 107 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
WriteAttributeValue("", 6279, item.ThumbnailImage.FileName, 6279, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 6309, "\"", 6326, 1);
#nullable restore
#line 107 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
WriteAttributeValue("", 6315, item.Title, 6315, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                                                    </a>
                                                </div>
                                                <div class=""product-content"">
                                                    <h3 class=""title""><a href=""#"">");
#nullable restore
#line 111 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                                                             Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h3>\r\n                                                    <div class=\"price\">\r\n                                                        $");
#nullable restore
#line 113 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                                    Write(item.Price.GetValueOrDefault(0).ToString("#,#.##"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                        <span>$");
#nullable restore
#line 114 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                                          Write(item.Sale.GetValueOrDefault(0).ToString("#,#.##"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                                    </div>\r\n                                                </div>\r\n                                                <ul class=\"social\">\r\n                                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 7175, "\"", 7182, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"js-show-modal1\" data-tip=\"Quick View\"><i class=\"fa fa-search\"></i></a></li>\r\n                                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 7326, "\"", 7333, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"js-add-wishlist\" data-status=\"true\" data-id=\"6\" data-tip=\"Add to Wishlist\"><i class=\"fal fa-heart\"></i></a></li>\r\n                                                    <li><a");
            BeginWriteAttribute("href", " href=\"", 7514, "\"", 7521, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""js-add-cart"" data-status=""true"" data-id=""6"" data-tip=""Add to Cart""><i class=""fa fa-shopping-basket"" aria-hidden=""true""></i></a></li>
                                                </ul>
                                            </div>
                                        </div>
");
#nullable restore
#line 124 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 124 "I:\SoureToan\Shop\Shop\Views\Product\Index.cshtml"
                                     
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n                            <div class=\"clearfix\"></div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n</main>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>

        $(function () {
            $('#themes').change(function (e) {
                this.form.submit();
                e.preventDefault();

            });
            $('#CategoryId').click(function (e) {
                this.form.submit();
                e.preventDefault();
            });
            $(""input[name='category']"").off('click').on('click', function () {
                getProduct();
            });
            $(""select[name='Product']"").on('change', function () {
                getProduct();
                console.log(1);
            });

            function getProduct(category) {
                var category = $(""input[name='category']:checked"").val();
                var brand = $(""input[name='brand']:checked"").val();
                var sort = $(""sort option:selected"").val();
                debugger;
                var query = """";
                if (category != null) {
                    query += (query.length == 0 ? ""?"" : ""&"") + ""cate");
                WriteLiteral(@"gory="" + category;
                }
                if (brand != null) {
                    query += (query.length == 0 ? ""?"" : ""&"") + ""brand="" + brand;
                }
                if (sort != null) {
                    query += (query.length == 0 ? ""?"" : ""&"") + ""sort="" + sort;
                }
                window.location = window.location.origin + window.location.pathname + query;
            }

        });
        $(document).ready(function () {

            function init() {
                var url_string = window.location.href;
                var url = new URL(url_string);
                var category = url.searchParams.get(""Category"");
                console.log(category);
                if (category != undefined) {
                    var categories = $(""input[name='category']"");
                    $.each(categories, function (i, item) {
                        let value = $(item).val();
                        if (value == category) {
                            ");
                WriteLiteral("$(item).prop(\'checked\', true);\r\n                        }\r\n                    })\r\n                }\r\n            }\r\n            init();\r\n        })\r\n    </script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProductList> Html { get; private set; }
    }
}
#pragma warning restore 1591
