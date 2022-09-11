using Microsoft.AspNetCore.Razor.TagHelpers;
using TagHelperComponents.ServerComponents;

namespace TagHelperComponents.Views;

public class ParametersWithChildrenComponentModel : ComponentTagHelperModel
{
    public string Sample { get; set; } = string.Empty;
}


[HtmlTargetElement("parameters-children")]
public class ParametersWithChildrenComponent : RazorComponentTagHelper
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
