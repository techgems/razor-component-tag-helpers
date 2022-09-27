using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.TagHelperComponents;


namespace TagHelperComponents.Views;

[HtmlTargetElement("simple-parameters")]
public class SimpleParametersComponent : RazorComponentTagHelper
{
    public SimpleParametersComponent() : base("~/Views/SimpleParameters.cshtml")
    {
    }

    [HtmlAttributeName("sample")]
    public string Sample { get; set; } = string.Empty;

    //public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    //{
    //    await RenderPartialView("~/Views/SimpleParameters.cshtml", output);
    //}
}
