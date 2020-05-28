#pragma checksum "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "40365e921b65a8294b30b479d8df1305dc34892a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Microsoft.eShopWeb.Web.Pages.Order.Views_Order_Detail), @"mvc.1.0.view", @"/Views/Order/Detail.cshtml")]
namespace Microsoft.eShopWeb.Web.Pages.Order
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
#line 1 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\_ViewImports.cshtml"
using Microsoft.eShopWeb.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\_ViewImports.cshtml"
using Microsoft.eShopWeb.Web.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\_ViewImports.cshtml"
using Microsoft.eShopWeb.Web.ViewModels.Account;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\_ViewImports.cshtml"
using Microsoft.eShopWeb.Web.ViewModels.Manage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\_ViewImports.cshtml"
using Microsoft.eShopWeb.Web.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\_ViewImports.cshtml"
using Microsoft.eShopWeb.Infrastructure.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"40365e921b65a8294b30b479d8df1305dc34892a", @"/Views/Order/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d260bd13a7f7d1b0143d034151ea04de3e5ffda", @"/Views/_ViewImports.cshtml")]
    public class Views_Order_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<OrderViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
  
    ViewData["Title"] = "My Order History";

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
  
    ViewData["Title"] = "Order Detail";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""esh-orders-detail"">
    <div class=""container"">
        <section class=""esh-orders-detail-section"">
            <article class=""esh-orders-detail-titles row"">
                <section class=""esh-orders-detail-title col-xs-3"">Order number</section>
                <section class=""esh-orders-detail-title col-xs-3"">Date</section>
                <section class=""esh-orders-detail-title col-xs-2"">Total</section>
                <section class=""esh-orders-detail-title col-xs-3"">Status</section>
            </article>

            <article class=""esh-orders-detail-items row"">
                <section class=""esh-orders-detail-item col-xs-3"">");
#nullable restore
#line 20 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                            Write(Model.OrderNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n                <section class=\"esh-orders-detail-item col-xs-3\">");
#nullable restore
#line 21 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                            Write(Model.OrderDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n                <section class=\"esh-orders-detail-item col-xs-2\">$");
#nullable restore
#line 22 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                             Write(Model.Total.ToString("N2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n                <section class=\"esh-orders-detail-item col-xs-3\">");
#nullable restore
#line 23 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                            Write(Model.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</section>
            </article>
        </section>

        <section class=""esh-orders-detail-section"">
            <article class=""esh-orders-detail-titles row"">
                <section class=""esh-orders-detail-title col-xs-12"">Shipping Address</section>
            </article>

            <article class=""esh-orders-detail-items row"">
                <section class=""esh-orders-detail-item col-xs-12"">");
#nullable restore
#line 33 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                             Write(Model.ShippingAddress.Street);

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n            </article>\r\n\r\n            <article class=\"esh-orders-detail-items row\">\r\n                <section class=\"esh-orders-detail-item col-xs-12\">");
#nullable restore
#line 37 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                             Write(Model.ShippingAddress.City);

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n            </article>\r\n\r\n            <article class=\"esh-orders-detail-items row\">\r\n                <section class=\"esh-orders-detail-item col-xs-12\">");
#nullable restore
#line 41 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                             Write(Model.ShippingAddress.Country);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</section>
            </article>
        </section>

        <section class=""esh-orders-detail-section"">
            <article class=""esh-orders-detail-titles row"">
                <section class=""esh-orders-detail-title col-xs-12"">ORDER DETAILS</section>
            </article>

");
#nullable restore
#line 50 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
             for (int i = 0; i < Model.OrderItems.Count; i++)
            {
                var item = Model.OrderItems[i];

#line default
#line hidden
#nullable disable
            WriteLiteral("                <article class=\"esh-orders-detail-items esh-orders-detail-items--border row\">\r\n                    <section class=\"esh-orders-detail-item col-md-4 hidden-md-down\">\r\n                        <img class=\"esh-orders-detail-image\"");
            BeginWriteAttribute("src", " src=\"", 2581, "\"", 2603, 1);
#nullable restore
#line 55 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
WriteAttributeValue("", 2587, item.PictureUrl, 2587, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    </section>\r\n                    <section class=\"esh-orders-detail-item esh-orders-detail-item--middle col-xs-3\">");
#nullable restore
#line 57 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                                                               Write(item.ProductName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n                    <section class=\"esh-orders-detail-item esh-orders-detail-item--middle col-xs-1\">$ ");
#nullable restore
#line 58 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                                                                 Write(item.UnitPrice.ToString("N2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n                    <section class=\"esh-orders-detail-item esh-orders-detail-item--middle col-xs-1\">");
#nullable restore
#line 59 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                                                               Write(item.Units);

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n                    <section class=\"esh-orders-detail-item esh-orders-detail-item--middle col-xs-2\">$ ");
#nullable restore
#line 60 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                                                                 Write(Math.Round(item.Units * item.UnitPrice, 2).ToString("N2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n                </article>\r\n");
#nullable restore
#line 62 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </section>

        <section class=""esh-orders-detail-section esh-orders-detail-section--right"">
            <article class=""esh-orders-detail-titles esh-basket-titles--clean row"">
                <section class=""esh-orders-detail-title col-xs-9""></section>
                <section class=""esh-orders-detail-title col-xs-2"">TOTAL</section>
            </article>

            <article class=""esh-orders-detail-items row"">
                <section class=""esh-orders-detail-item col-xs-9""></section>
                <section class=""esh-orders-detail-item esh-orders-detail-item--mark col-xs-2"">$ ");
#nullable restore
#line 73 "C:\Users\Sam\source\repos\BTTichHopDL\Web\Views\Order\Detail.cshtml"
                                                                                           Write(Model.Total.ToString("N2"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n            </article>\r\n        </section>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<OrderViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
