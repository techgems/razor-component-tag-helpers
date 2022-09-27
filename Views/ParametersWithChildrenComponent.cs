using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.TagHelperComponents;

namespace TagHelperComponents.Views;

[HtmlTargetElement("parameters-children")]
public class ParametersWithChildrenComponent : RazorComponentTagHelper
{
    public ParametersWithChildrenComponent() : base("~/Views/ParametersWithChildren.cshtml")
    {
    }

    [HtmlAttributeName("sample")]
    public string Sample { get; set; } = string.Empty;
}
