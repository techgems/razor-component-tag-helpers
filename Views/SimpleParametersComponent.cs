using Microsoft.AspNetCore.Razor.TagHelpers;
using TagHelperComponents.ServerComponents;

namespace TagHelperComponents.Views;

public class SimpleParametersComponentModel : ComponentTagHelperModel
{
    public string Sample { get; set; } = string.Empty;
}


[HtmlTargetElement("simple-parameters")]
public class SimpleParametersComponent : RazorComponentTagHelper
{
    public SimpleParametersComponent()
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
