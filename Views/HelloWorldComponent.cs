using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.TagHelperComponents;

namespace TagHelperComponents.Views;

[HtmlTargetElement("hello-world")]
public class HelloWorldComponent : RazorComponentTagHelper
{
    public HelloWorldComponent() : base("~/Views/HelloWorld.cshtml")
    {
    }
}
