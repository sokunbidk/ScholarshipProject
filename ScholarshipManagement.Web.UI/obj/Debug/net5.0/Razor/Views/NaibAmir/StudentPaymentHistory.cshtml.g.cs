#pragma checksum "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d218e3f9c0e969199b832862d7ae485f2eba39ac"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_NaibAmir_StudentPaymentHistory), @"mvc.1.0.view", @"/Views/NaibAmir/StudentPaymentHistory.cshtml")]
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
#line 1 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\_ViewImports.cshtml"
using ScholarshipManagement.Web.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\_ViewImports.cshtml"
using ScholarshipManagement.Web.UI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d218e3f9c0e969199b832862d7ae485f2eba39ac", @"/Views/NaibAmir/StudentPaymentHistory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"300b42ce203afd2a73f3ebe7de32c0bce8e54483", @"/Views/_ViewImports.cshtml")]
    public class Views_NaibAmir_StudentPaymentHistory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IList<ScholarshipManagement.Data.DTOs.PendingApplicationsDto>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger  text-lg-right font-weight-bold"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "NaibAmir", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "PendingApplicationsFocus", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "text", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control btn btn-primary "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
  
    ViewData["Title"] = "Payment History";
    int count = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>Payment History</h1>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d218e3f9c0e969199b832862d7ae485f2eba39ac5407", async() => {
                WriteLiteral("---Dashboard");
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
            WriteLiteral("\r\n\r\n    <div class=\"card-body\">\r\n");
#nullable restore
#line 11 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
         if (Model.Count() == 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div>\r\n                <h5 class=\"text-center text-danger\">No Payment History!</h5>\r\n            </div>\r\n");
#nullable restore
#line 16 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <table class=""table  table-responsive-sm table-bordered"">
                <thead class=""table-title"">
                    <tr class=""text-white"">
                        <th>S/N</th>
                        <th>Name</th>
                        <th>Name Of School</th>
                        <th>Academic Level</th>
                        <th>Session</th>
                        <th>Status</th>
                        <th>Granted</th>
                        <th>Bank</th>
                        <th>DateDisbursed</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 34 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
                     foreach (var PendingApplicationsDto in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td> ");
#nullable restore
#line 37 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
                         Write(++count);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 38 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
                       Write(PendingApplicationsDto.Names);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td> ");
#nullable restore
#line 39 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
                        Write(PendingApplicationsDto.NameOfSchool);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 40 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
                       Write(PendingApplicationsDto.AcademicLevel);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td> ");
#nullable restore
#line 41 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
                        Write(PendingApplicationsDto.SchoolSession);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "d218e3f9c0e969199b832862d7ae485f2eba39ac10240", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#nullable restore
#line 43 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => PendingApplicationsDto.Status);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </td>\r\n                        <td>");
#nullable restore
#line 45 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
                       Write(PendingApplicationsDto.AmountGranted);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 46 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
                       Write(PendingApplicationsDto.BankAccountName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                        <td>");
#nullable restore
#line 47 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
                       Write(PendingApplicationsDto.DateDisbursed);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n\r\n\r\n\r\n                    </tr>\r\n");
#nullable restore
#line 52 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n");
#nullable restore
#line 55 "C:\Project\ScholarshipManagement04-09-2021\ScholarshipManagement.Web.UI\Views\NaibAmir\StudentPaymentHistory.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IList<ScholarshipManagement.Data.DTOs.PendingApplicationsDto>> Html { get; private set; }
    }
}
#pragma warning restore 1591
