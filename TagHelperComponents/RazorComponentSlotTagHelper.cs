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

        if (ParentComponent is null)
            throw new ArgumentNullException($"A slot tag helper component cannot be used without a parent component.");

        //Get the content of the slot and insert it in the parent component.
        var childContent = await output.GetChildContentAsync();

        if (childContent is null)
            throw new ArgumentNullException("A slot must have child content in order to work as intended. Child content cannot be null.");

        var result = ParentComponent.NamedSlots.TryAdd(Name, childContent);

        if (!result)
            throw new ArgumentException("A slot identifier has been repeated and was not processed. Slots require unique name values when used inside a single parent element");

        output.SuppressOutput();
    }
}
