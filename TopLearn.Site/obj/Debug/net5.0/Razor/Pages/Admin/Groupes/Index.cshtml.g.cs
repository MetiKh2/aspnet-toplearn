#pragma checksum "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "636181a990833f823df8f3e9e5683fb380e65811"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Pages_Admin_Groupes_Index), @"mvc.1.0.razor-page", @"/Pages/Admin/Groupes/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"636181a990833f823df8f3e9e5683fb380e65811", @"/Pages/Admin/Groupes/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9af4978b9c2bfca24ef48e96efe5f8573634464", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Admin_Groupes_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "AddGroupe", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-12"">
        <h1 class=""page-header"">لیست گروه ها</h1>
    </div>
    <!-- /.col-lg-12 -->
</div>

<div class=""row"">
    <div class=""col-lg-12"">
        <div class=""panel panel-default"">
            <div class=""panel-heading"">
                لیست گروه های سایت
            </div>
            <!-- /.panel-heading -->
            <div class=""panel-body"">
                <div class=""table-responsive"">
                    <div id=""dataTables-example_wrapper"" class=""dataTables_wrapper form-inline"" role=""grid"">

                        <div class=""col-md-12"" style=""margin: 10px 0;"">

                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "636181a990833f823df8f3e9e5683fb380e658114098", async() => {
                WriteLiteral("افزودن گروه جدید");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

                        </div>
                        <table class=""table table-striped table-bordered table-hover dataTable no-footer"" id=""dataTables-example"" aria-describedby=""dataTables-example_info"">
                            <thead>
                                <tr>
                                    <th>نام گروه</th>
                                    <th>زیر گروه ها</th>

                                    <th>دستورات</th>

                                </tr>
                            </thead>
                            <tbody>
");
#nullable restore
#line 40 "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml"
                                 foreach (var item in Model.Groupes.Where(p => p.ParentID == null))
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>");
#nullable restore
#line 43 "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml"
                                       Write(item.GroupeTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                                        <td>\r\n                                            <ul>\r\n");
#nullable restore
#line 47 "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml"
                                                 foreach (var sub in Model.Groupes.Where(p => p.ParentID == item.GroupeID))
                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                    <li>\r\n                                                        ");
#nullable restore
#line 50 "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml"
                                                   Write(sub.GroupeTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("--\r\n                                                        <a");
            BeginWriteAttribute("href", " href=\"", 2100, "\"", 2146, 2);
            WriteAttributeValue("", 2107, "/admin/Groupes/EditGroupe/", 2107, 26, true);
#nullable restore
#line 51 "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml"
WriteAttributeValue("", 2133, sub.GroupeID, 2133, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-outline btn-warning btn-xs\">\r\n                                                            ویرایش\r\n                                                        </a>--\r\n                                                        <a");
            BeginWriteAttribute("href", " href=\"", 2383, "\"", 2431, 2);
            WriteAttributeValue("", 2390, "/admin/Groupes/removeGroupe/", 2390, 28, true);
#nullable restore
#line 54 "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml"
WriteAttributeValue("", 2418, sub.GroupeID, 2418, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-outline btn-danger btn-xs\">\r\n                                                            حذف\r\n                                                        </a>\r\n                                                    </li>\r\n");
#nullable restore
#line 58 "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml"
                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            </ul>\r\n                                        </td>\r\n                                        <td>\r\n\r\n                                            <a");
            BeginWriteAttribute("href", " href=\"", 2906, "\"", 2953, 2);
            WriteAttributeValue("", 2913, "/admin/Groupes/editGroupe/", 2913, 26, true);
#nullable restore
#line 63 "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml"
WriteAttributeValue("", 2939, item.GroupeID, 2939, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-warning btn-sm\">\r\n                                                ویرایش\r\n                                            </a>\r\n                                            <a");
            BeginWriteAttribute("href", " href=\"", 3140, "\"", 3189, 2);
            WriteAttributeValue("", 3147, "/admin/Groupes/removeGroupe/", 3147, 28, true);
#nullable restore
#line 66 "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml"
WriteAttributeValue("", 3175, item.GroupeID, 3175, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-danger btn-sm\">\r\n                                                حذف\r\n                                            </a>\r\n                                            <a");
            BeginWriteAttribute("href", " href=\"", 3372, "\"", 3418, 2);
            WriteAttributeValue("", 3379, "/admin/Groupes/AddGroupe/", 3379, 25, true);
#nullable restore
#line 69 "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml"
WriteAttributeValue("", 3404, item.GroupeID, 3404, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-info btn-sm\">\r\n                                                افزودن زیر گروه\r\n                                            </a>\r\n                                        </td>\r\n                                    </tr>\r\n");
#nullable restore
#line 74 "D:\پروژه سایت تاپلرن\TopLearn\TopLearn.Site\Pages\Admin\Groupes\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            </tbody>
                        </table><div class=""row"">

                        </div>
                    </div>
                </div>

            </div>
            <!-- /.panel-body -->
        </div>
        <!-- /.panel -->
    </div>
    <!-- /.col-lg-12 -->
</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TopLearn.Site.Pages.Groupes.IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<TopLearn.Site.Pages.Groupes.IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<TopLearn.Site.Pages.Groupes.IndexModel>)PageContext?.ViewData;
        public TopLearn.Site.Pages.Groupes.IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
