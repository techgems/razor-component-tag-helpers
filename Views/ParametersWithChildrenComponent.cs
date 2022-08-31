using Microsoft.AspNetCore.Razor.TagHelpers;
using ServerComponents.ServerComponents;

namespace ServerComponents.Views;

public class ParametersWithChildrenComponentModel : ServerComponentModel
{
    public string Sample { get; set; } = string.Empty;
}


[HtmlTargetElement("parameters-children")]
public class ParametersWithChildrenComponent : ServerComponent
{
    public ParametersWithChildrenComponent(IRazorRenderer razorRenderer) : base(razorRenderer)
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
