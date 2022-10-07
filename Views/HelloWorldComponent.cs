using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.RazorComponentTagHelpers;

namespace RazorComponentTagHelpers.Views;

[HtmlTargetElement("hello-world")]
public class HelloWorldComponent : RazorComponentTagHelper
{
    public HelloWorldComponent() : base("~/Views/HelloWorld.cshtml")
    {
    }
}
