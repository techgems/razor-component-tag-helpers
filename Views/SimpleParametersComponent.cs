using Microsoft.AspNetCore.Razor.TagHelpers;
using ServerComponents.ServerComponents;

namespace ServerComponents.Views;

public class SimpleParametersComponentModel : ServerComponentModel
{
    public string Sample { get; set; } = string.Empty;
}


[HtmlTargetElement("simple-parameters")]
public class SimpleParametersComponent : ServerComponent
{
    public SimpleParametersComponent(IRazorRenderer razorRenderer) : base(razorRenderer)
    {
    }

    [HtmlAttributeName("sample")]
    public string Sample { get; set; } = string.Empty;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var model = new SimpleParametersComponentModel()
        {
            Sample = Sample
        };

        await RenderPartialView("~/Views/SimpleParameters.cshtml", output, model);
    }
}
