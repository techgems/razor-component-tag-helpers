---
title: 'Slots and Child Content'
date: 2022-10-29T19:27:37+10:00
weight: 4
---

Similar to how you can have child content and slots in Single Page Application frameworks, Razor Component Tag Helpers allows you to do this as well. Razor Component Tag Helpers only has one tag helper defined in it, which is the `RazorComponentSlotTagHelper`, it can be used only inside classes that inherit from `RazorComponentTagHelper`.

## Child Content vs Slots

The main difference between child content and slots is the way that they're used. ChildContent is shown by using the expression `@Model.ChildContent` in razor. Slots are not considered child content and will not be rendered along with the `ChildContent`. This means you can use both ChildContent and Slots in the same Razor Tag Helper.

## Slots

Slots work a little bit different than ChildContent. To use a slot you will need to run the `RenderSlot` function, with a valid slot name in the Razor Tag Helper.

The following is an example of how to set this up:

```csharp
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
```

Then you need a view like this one:

```html
@using RazorComponentTagHelpers.Views;
@model CardSampleComponent

<div class="cardHeader">
    @Model.RenderSlot("cardHeader")
</div>

<div class="child-content">
    <span>@Model.ChildContent</span>
</div>

<div class="cardFooter">
    @Model.RenderSlot("cardFooter") 
</div>

```

Finally, the `CardSampleComponent` tag helper is used declaratively like this:

```html
<card-sample>
    <slot name="cardHeader">
        <h4>This is your first slot.</h4>
    </slot>
    <h4>Child Content</h4>
    <slot name="cardFooter">
        <h4>This is your second slot.</h4>
    </slot>
</card-sample>
```

You will see that in the `card-sample` there is no reference to the slots and there shouldn't be, since slots are a declarative UI concept. But we do see them in the declarative use of the tag helper as well as in the razor template.

The finalized output of the previous declaration is the following:

```html
<div>
    <div class="cardHeader">
        <h4>This is your first slot.</h4>
    </div>
    <div class="child-content">
        <span>
            <h4>Child Content</h4>
        </span>
    </div>
    <div class="cardFooter">
        <h4>This is your second slot.</h4>
    </div>
</div>
```

UI composition with slots can be incredibly useful when building UI elements that need many levels of customization. Sometimes you want to build a sidebar in which most of the CSS and HTML is set, but you still need to expose both a mobile links area and a desktop links area.

While slightly more advanced, this technique allows you more freedom in your component composition and it enables you to not need to write more components than you should if you didn't have access to slots. It also allows you to write components that are more easily reusable by anyone you expose your components to.

## Fallback content

Sometimes, when you use slots, you will want content fallbacks in case it doesn't make sense to just not display any content.

Fallbacks exist both for child content and for slots.

We will use the same example tag helper above, but we will only change the razor template to look like this:

```html
@using RazorComponentTagHelpers.Views;
@model CardSampleComponent

<div>
    <div class="cardHeader">
        @if (Model.IsSlotContentNullOrEmpty("cardHeader"))
        {
            <h5>slot1 Fallback</h5>
        }
        else
        {
            @Model.RenderSlot("cardHeader")
        }
    </div>
    <div class="child-content">
        <span>
            @if (Model.IsChildContentNullOrEmpty)
            {
                @: Child Content Fallback here
            }
            else
            {
                @Model.ChildContent
            }
        </span>
    </div>
    <div class="cardFooter">
        @if (Model.IsSlotContentNullOrEmpty("cardFooter"))
        {
            <h5>slot2 Fallback</h5>
        }
        else
        {
            @Model.RenderSlot("cardFooter")
        }
    </div>
</div>
```

We have some additional functions that allow us to determine if a slot is actually being used or not, the same applies with child content.

By using the function `IsSlotContentNullOrEmpty` you can determine if the slot was declared or if it wasn't. Likewise, normal child content can be validated with the property `IsChildContentNullOrEmpty`.

## Best practice for slot names

A good approach to not end up using magic strings everywhere when writing components that use slots is to use static string variables for the names of the allowed slots.

Going to our first example, instead of having something like this:

```csharp
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
```

```html
@using RazorComponentTagHelpers.Views;
@model CardSampleComponent

<div class="cardHeader">
    @Model.RenderSlot("cardHeader")
</div>

<div class="child-content">
    <span>@Model.ChildContent</span>
</div>

<div class="cardFooter">
    @Model.RenderSlot("cardFooter") 
</div>
```

You could write it like this:

```csharp
using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.RazorComponentTagHelpers;

namespace RazorComponentTagHelpers.Views;

[HtmlTargetElement("card-sample")]
public class CardSampleComponent : RazorComponentTagHelper
{
    public static readonly string CardHeaderSlot = "cardHeader";
    public static readonly string CardFooterSlot = "cardFooter";

    public CardSampleComponent() : base("~/Views/CardSample.cshtml")
    {
    }
}
```

```html
@using RazorComponentTagHelpers.Views;
@model CardSampleComponent

<div class="cardHeader">
    @Model.RenderSlot(CardSampleComponent.CardHeaderSlot)
</div>

<div class="child-content">
    <span>@Model.ChildContent</span>
</div>

<div class="cardFooter">
    @Model.RenderSlot(CardSampleComponent.CardFooterSlot) 
</div>
```

```html
<card-sample>
    <slot name="@CardSampleComponent.CardHeaderSlot">
        <h4>This is your first slot.</h4>
    </slot>
    <h4>Child Content</h4>
    <slot name="@CardSampleComponent.CardFooterSlot">
        <h4>This is your second slot.</h4>
    </slot>
</card-sample>
```

This is a simple approach to make sure you don't find errors with your slot names due to a typo and also to make your components easy to refactor.

