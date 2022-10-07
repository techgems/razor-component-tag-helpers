---
title: 'Basic Usage'
date: 2022-09-28T19:27:37+10:00
weight: 3
---

Razor Component Tag Helpers is used by inheriting the base class `RazorComponentTagHelper`. This class itself inherits from `TagHelperBase` which is necessary for any class to be a tag helper.

In code a simple example looks like this:

```csharp
using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.RazorComponentTagHelpers;

namespace Sample.Views;

[HtmlTargetElement("hello-world")]
public class HelloWorldComponent : RazorComponentTagHelper
{
    public HelloWorldComponent()
    {
    }
}
```

This will immediately and with no additional configuration search and render it's corresponding View in `Views/TagHelpers/HelloWorldComponent/Default.cshtml`. If it's not found an error will be thrown.

It is possible to override the default view route by specifying the route to the razor view of your choosing in the base constructor, like this:

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
}
```

It's important to note that your razor template will be rendered as a partial that by default will receive the class `HelloWorldComponent` as it's view model. This is instrumental to be able to receive parameters and render child content like you would be able to do in any other tag helper.

In this example we will expect both one parameter and some child content. 

```csharp
using Microsoft.AspNetCore.Razor.TagHelpers;
using TechGems.RazorComponentTagHelpers;

namespace Sample.Views;

[HtmlTargetElement("parameters-children")]
public class ParametersWithChildrenComponent : RazorComponentTagHelper
{
    public ParametersWithChildrenComponent() : base("~/Views/ParametersWithChildren.cshtml")
    {
    }

    [HtmlAttributeName("sample")]
    public string Sample { get; set; } = string.Empty;
}
```

The html in the view would look something like this:

```html
@model Sample.Views.ParametersWithChildrenComponent

<span>@Model.Sample</span>

<div>
    @Model.ChildContent
</div>
```

and you'd be able to invoke it in your code like this:

```html
<parameters-children sample="text">
  <div>Child html</div>
</parameters-children>
```

## Summary

These examples should cover about 95% of use cases for this library. However, you can override default behaviour should you need to. Look into [Advanced Usage](/docs/advanced-usage) for more information about overriding more defaults.