---
title: 'Overview'
date: 2022-09-28T15:14:39+10:00
weight: 1
---

## Razor Component Tag Helpers

Razor Component Tag Helpers is a minimalistic ASP.NET Core library that allows you to write UI components while maintaining compatibility with Razor Pages and MVC. With this library you can create your own component-like tag helpers using Razor syntax that receive and render child elements effortlessly.

If you wish to see a functional application of this library in a dashboard template, please look at [this](https://github.com/techgems/dotnet-6-custom-db-identity) repository.

## What is the use case of this library?

This library will work for you if you're working on an MVC or Razor Pages only project and you wish to have better UI composition. It can be of help both in modernizing and organizing older projects as well as starting new ones where using a single page application framework is considered unnecessary due to lack of heavy UI interactions or simply familiarity with the MVC pattern.

While Blazor is a great alternative, it is a dependency that relies either on a SignalR connection or a Web Assembly packaged application that is very heavy to download. If your use case doesn't need strong UI interactions but you still want some way of writing components, then this is a good way to do so. 

It should be noted that you can easily add bits of UI interaction where needed with a low JS approach, but there are more details on that in [Advanced Usage](/docs/advanced-usage).

## Doesn't using Blazor inside MVC make this obsolete?

No, it doesn't. Nor does it make obsolete partial views or view components for that matter. Blazor components can be rendered in normal MVC or Razor pages, but they do not support tag helpers, which makes their sinergize poorly with an MVC approach. There are also circumstances in which you might not want or need all the functionality and power of Blazor.

Blazor is an awesome project, but as with all things in tech, there are tradeoffs you have to consider. If all you need is UI encapsulation in MVC or Razor pages, then using Razor Tag Helpers will let you get the same benefits with zero additional dependencies and no overhead.

Additionally, ASP.NET Core is very customizable in the approaches you have for building a server. You could easily justify having one MVC page use Blazor WASM for a complex rich UI piece and have the rest of the app which is just forms use MVC with Razor Tag Helpers. 

You can find a more detailed dissertation about pros and cons of different approaches to UI composition [here](https://techgems.net/posts/2022/2022-09-10-ui-composition-in-asp-net-core/).