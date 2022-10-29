using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.RazorComponentTagHelpers;

namespace RazorComponentTagHelpers.Views;

[HtmlTargetElement("slot-component")]
public class SlotComponent : RazorComponentTagHelper
{
    public SlotComponent() : base("~/Views/Slot.cshtml")
    {
    }
}
