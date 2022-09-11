using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorTagHelpers.ServerComponents;

namespace RazorTagHelpers.Views;

public class SimpleParametersComponentModel : RazorTagHelperModel
{
    public string Sample { get; set; } = string.Empty;
}


[HtmlTargetElement("simple-parameters")]
public class SimpleParametersComponent : RazorTagHelper
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
