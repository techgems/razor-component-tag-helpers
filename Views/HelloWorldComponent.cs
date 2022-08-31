using Microsoft.AspNetCore.Razor.TagHelpers;
using ServerComponents.ServerComponents;

namespace ServerComponents.Views;

public class HelloWorldComponentModel : ServerComponentModel
{
}


[HtmlTargetElement("hello-world")]
public class HelloWorldComponent : ServerComponent
{
    public HelloWorldComponent(IRazorRenderer razorRenderer) : base(razorRenderer)
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
