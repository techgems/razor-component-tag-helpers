---
title: 'Installation'
date: 2022-09-28T19:27:37+10:00
weight: 2
---

Installing Razor Component Tag Helpers is very easy. All you need to do is download the nuget package:

```s
dotnet add package TechGems.RazorComponentTagHelpers --version 1.0.0
```

or alternatively you can even just copy and paste the necessary base class file located [here](https://github.com/techgems/server-components/blob/master/RazorComponentTagHelpers/RazorComponentTagHelper.cs) into your project and use it the same way it is explained in [Basic Usage](/docs/basic-usage).

As with all tag helpers, you will need to go to the `_ViewImports.cshtml` file and add a reference to your project's namespace like so:

```
@addTagHelper *, Sample.Web
```

This applies to any custom tag helper, but without this step, none of your tag helpers will be compiled as such.