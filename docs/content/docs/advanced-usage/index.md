---
title: 'Advanced Usage'
date: 2022-09-28T19:30:08+10:00
weight: 5
---

## Overriding ProcessAsync

As you may be aware, the method `ProcessAsync` is an essential part for implementing the functionality of a tag helper.

If all you need to do is render a partial view as part of the functionality of your tag helper, then you are fine using the default implementation included in `RazorComponentTagHelper`. However, if for some reason you need to do more with your tag helper, then you can override the default implementation.

Doing this will cause the rendering of the corresponding partial view to not happen, however, there are a couple of helper functions included in the base class that you can use to do render the partial view. It would look something like this:

```csharp
using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.RazorComponentTagHelpers;

namespace Sample.Views;

[HtmlTargetElement("hello-world")]
public class HelloWorldComponent : RazorComponentTagHelper
{
    public HelloWorldComponent() : base("~/Views/HelloWorld.cshtml")
    {
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        //Your custom logic here...

        await base.RenderPartialView(output);

        //Alternatively you can use the following if you don't want to use the inherited view route member.
        //await base.RenderPartialView("~/Views/DifferentView.cshtml", output);
    }
}
```

The one thing that is worthy of note is that the implementation of `RenderPartialView` method does use the output and sets a couple of things for partial view rendering to work, specifically:

- output.Content: Which is set as the output of the partial view.
- output.TagName: Which is set to null.

If you need to avoid using `RenderPartialView` altogether, you can, the implementation isn't too complex since this library is a single file library.

You can also use the HtmlHelper method inside the base class directly by calling inside your implementation of `ProcessAsync`:

```csharp
var htmlHelper = base.GetHtmlHelper();
var content = await htmlHelper.PartialAsync(viewRoute, yourModelForPartialView); //We use 'this' by default in the helper functions.
```

But overriding the default `ProcessAsync` can allow you to do data fetching or dynamically deciding which partial view to render.

## Client-side interactions with AlpineJS

This approach isn't unique to this library or to [AlpineJs](https://alpinejs.dev/), but it's an approach that fits this kind of UI composition very well. You can also very easily do this with [Petite Vue](https://github.com/vuejs/petite-vue) and it should work well.

But essentially, these kinds of JS libraries allow you to write client-side logic without actually writing a script in a very declarative way which meshes very well with server rendered html.

A simple example could be a collapsible tag helper like the one below, we will be using AlpineJS 3, which means it will not work property if you don't have the script in your Html.

```csharp
using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.RazorComponentTagHelpers;

namespace Sample.Views;

[HtmlTargetElement("collapsible-section")]
public class CollapsibleSectionComponent : RazorComponentTagHelper
{
    public CollapsibleSectionComponent() : base("~/Views/CollapsibleSection.cshtml")
    {
    }

    [HtmlAttributeName("is-open")]
    public bool IsOpen { get; set; } = false;
}
```

And the partial view:

```html
@using Sample.Views;
@model CollapsibleSectionComponent
@{
    var isOpenJs = Model.IsOpen.ToString().ToLower();
}

<div x-data="{ open: @isOpenJs }">
    <div x-show="open">
        @Model.ChildContent
    </div>

    <button @@click="open = !open">Toggle</button>
</div>
```

And you should be able to call it this way:

```html
<collapsible-section is-open="true">This content can be toggled on and off.</collapsible-section>
```

This sample doesn't have any proper styling, but it's sufficient enough to show how easy it can be to implement a reusable component.

### Summary

The approach of a low JS with static server rendered components approach isn't applicable for everything, but having options is a good thing and the same can be said for optimizing for tradeoffs in technologies. 

Also, this approach goes deeper than what I described, and currently there are very few blog posts of it's application alongside ASP.NET Core, but you can find some resources [here](https://www.saaspegasus.com/guides/modern-javascript-for-django-developers/) and [here](https://www.saaspegasus.com/guides/modern-javascript-for-django-developers/htmx-alpine/).