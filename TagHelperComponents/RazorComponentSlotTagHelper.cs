using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechGems.RazorComponentTagHelpers;

//TODO: Limit to being child of RazorComponentTagHelper

[HtmlTargetElement("slot")]
public class RazorComponentSlotTagHelper : RazorComponentTagHelper
{
    public RazorComponentSlotTagHelper()
    {
    }

    [HtmlAttributeName("name")]
    public string Name { get; set; } = "";

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (string.IsNullOrEmpty(Name))
            throw new ArgumentException("Slot Name is mandatory.");

        if (ParentComponent != null)
        {
            var childContent = await output.GetChildContentAsync();
            ParentComponent.NamedSlots[Name] = childContent;
        }

        output.SuppressOutput();
    }
}
