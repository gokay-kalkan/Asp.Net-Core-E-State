#pragma checksum "C:\Users\gkykl\OneDrive\Masaüstü\ASP.NET CORE\EState\EState\Views\User\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1943fcd27b88a30cfba33498b616f752f926d553"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Index), @"mvc.1.0.view", @"/Views/User/Index.cshtml")]
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
#line 1 "C:\Users\gkykl\OneDrive\Masaüstü\ASP.NET CORE\EState\EState\Views\_ViewImports.cshtml"
using EState;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\gkykl\OneDrive\Masaüstü\ASP.NET CORE\EState\EState\Views\_ViewImports.cshtml"
using EState.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\gkykl\OneDrive\Masaüstü\ASP.NET CORE\EState\EState\Views\_ViewImports.cshtml"
using EntityLayer.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\gkykl\OneDrive\Masaüstü\ASP.NET CORE\EState\EState\Views\User\Index.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1943fcd27b88a30cfba33498b616f752f926d553", @"/Views/User/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7e6851e433c1b2fc80a3e8623985e0ed8960d122", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\gkykl\OneDrive\Masaüstü\ASP.NET CORE\EState\EState\Views\User\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_UserLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n<div class=\"container\">\r\n    <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n            <div class=\"col-md-6\">\r\n                <div class=\"jumbotron\">\r\n                    <h2>");
#nullable restore
#line 14 "C:\Users\gkykl\OneDrive\Masaüstü\ASP.NET CORE\EState\EState\Views\User\Index.cshtml"
                   Write(Context.Session.GetString("FullName"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" Kullanıcı Paneline Hoş Geldiniz.</h2>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
