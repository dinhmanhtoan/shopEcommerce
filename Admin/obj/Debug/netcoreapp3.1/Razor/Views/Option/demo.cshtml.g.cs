#pragma checksum "D:\souc\source\.net\Shop2\shop2\Admin\Views\Option\demo.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "70614ef42bcc37c6c7cc518703a8e2f9f4844a4e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Option_demo), @"mvc.1.0.view", @"/Views/Option/demo.cshtml")]
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
#line 1 "D:\souc\source\.net\Shop2\shop2\Admin\Views\_ViewImports.cshtml"
using Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\souc\source\.net\Shop2\shop2\Admin\Views\_ViewImports.cshtml"
using Admin.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\souc\source\.net\Shop2\shop2\Admin\Views\_ViewImports.cshtml"
using Model.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\souc\source\.net\Shop2\shop2\Admin\Views\_ViewImports.cshtml"
using Model.ViewModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\souc\source\.net\Shop2\shop2\Admin\Views\_ViewImports.cshtml"
using Model.ViewModel.Account;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\souc\source\.net\Shop2\shop2\Admin\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"70614ef42bcc37c6c7cc518703a8e2f9f4844a4e", @"/Views/Option/demo.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"48b77e8fbac7087c986ea5cb8b8ef87f94230207", @"/Views/_ViewImports.cshtml")]
    public class Views_Option_demo : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""card"">
    <div class=""card-header"">
        <h2>Thêm Lựa Chọn</h2>
    </div>

    <div class=""card-body"">

        <div class=""form-group "">
            <label> Thêm lựa chọn</label>
            <input class=""form-control"" type=""text"" name=""option"" placeholder=""Default input"">
            <button class=""btn btn-success"">Thêm</button>
        </div>
        <div>
            <ul class=""add-option"">
                <li class=""option-item""></li>
            </ul>
        </div>

    </div>
</div>
");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        $(document).ready(function () {
            $("".btn"").on(""click"", function () {
              var value =   $(""input[name=option]"").val();
                $("".add-option"").append(`<li class=""option-item"">${value}</li>`);
            })
        })
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
