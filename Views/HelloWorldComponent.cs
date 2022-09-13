using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.TagHelperComponents;

namespace TagHelperComponents.Views;

[HtmlTargetElement("hello-world")]
public class HelloWorldComponent : RazorComponentTagHelper
{
    public HelloWorldComponent()
    {
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await RenderPartialView("~/Views/HelloWorld.cshtml", output);
    }
}
