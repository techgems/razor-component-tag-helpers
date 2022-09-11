using Microsoft.AspNetCore.Razor.TagHelpers;
using RazorTagHelpers.ServerComponents;

namespace RazorTagHelpers.Views;

public class HelloWorldComponentModel : RazorTagHelperModel
{
}


[HtmlTargetElement("hello-world")]
public class HelloWorldComponent : RazorTagHelper
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
