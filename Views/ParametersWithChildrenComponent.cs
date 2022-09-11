using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorTagHelpers.ServerComponents;

namespace RazorTagHelpers.Views;

public class ParametersWithChildrenComponentModel : RazorTagHelperModel
{
    public string Sample { get; set; } = string.Empty;
}


[HtmlTargetElement("parameters-children")]
public class ParametersWithChildrenComponent : RazorTagHelper
{
    public ParametersWithChildrenComponent()
    {
    }

    [HtmlAttributeName("sample")]
    public string Sample { get; set; } = string.Empty;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var model = new ParametersWithChildrenComponentModel()
        {
            Sample = Sample
        };

        await RenderPartialView("~/Views/ParametersWithChildren.cshtml", output, model);
    }
}
