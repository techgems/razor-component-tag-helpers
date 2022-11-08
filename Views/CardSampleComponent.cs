using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.RazorComponentTagHelpers;

namespace RazorComponentTagHelpers.Views;

[HtmlTargetElement("card-sample")]
public class CardSampleComponent : RazorComponentTagHelper
{
    public CardSampleComponent() : base("~/Views/CardSample.cshtml")
    {
    }
}
