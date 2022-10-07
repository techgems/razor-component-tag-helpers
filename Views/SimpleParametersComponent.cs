using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.RazorComponentTagHelpers;


namespace RazorComponentTagHelpers.Views;

[HtmlTargetElement("simple-parameters")]
public class SimpleParametersComponent : RazorComponentTagHelper
{
    public SimpleParametersComponent() : base("~/Views/SimpleParameters.cshtml")
    {
    }

    [HtmlAttributeName("sample")]
    public string Sample { get; set; } = string.Empty;

}
