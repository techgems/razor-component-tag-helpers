using Microsoft.AspNetCore.Razor.TagHelpers;
using TagHelperComponents.ServerComponents;

namespace TagHelperComponents.Views;

public class HelloWorldComponentModel : ComponentTagHelperModel
{
}


[HtmlTargetElement("hello-world")]
public class HelloWorldComponent : RazorComponentTagHelper
{
    public HelloWorldComponent()
    {
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var model = new HelloWorldComponentModel()
        {

        };

        await RenderPartialView("~/Views/HelloWorld.cshtml", output, model);
    }
}
