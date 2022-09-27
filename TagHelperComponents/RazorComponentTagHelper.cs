using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TechGems.TagHelperComponents;

/// <summary>
/// The base class for a component tag helper that leverages a razor partial view. Sends itself as the view model for the razor template.
/// </summary>
public abstract class RazorComponentTagHelper : TagHelper
{
    private readonly string? _razorViewRoute;

    /// <summary>
    /// Creates the tag helper with a razor view route using default route.
    /// </summary>
    public RazorComponentTagHelper()
    {

    }

    /// <summary>
    /// Creates the tag helper with a razor view route that overrides the default route.
    /// </summary>
    /// <param name="razorViewRoute"></param>
    public RazorComponentTagHelper(string razorViewRoute)
    {
        _razorViewRoute = razorViewRoute;
    }

    /// <summary>
    /// The View Context necessary to get the Html Helper that renders the partial views.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext? ViewContext { get; set; }

    /// <summary>
    /// Child content for rendering in the razor template.
    /// </summary>
    [HtmlAttributeNotBound]
    public TagHelperContent? ChildContent { get; set; }

    /// <summary>
    /// Gets the Html Helper from the View Context. Used for rendering partial views.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private IHtmlHelper GetHtmlHelper()
    {
        if (ViewContext is null)
        {
            throw new ArgumentNullException(nameof(ViewContext));
        }

        IHtmlHelper? htmlHelper = ViewContext.HttpContext.RequestServices.GetService<IHtmlHelper>();
        ArgumentNullException.ThrowIfNull(htmlHelper);

        (htmlHelper as IViewContextAware)!.Contextualize(ViewContext);

        return htmlHelper;
    }


    /// <summary>
    /// Default ProcessAsync method. Will render the default razor view if a route is not provided in the base class.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext is null)
        {
            throw new ArgumentNullException(nameof(ViewContext));
        }

        if(_razorViewRoute is null) 
        { 
            await RenderPartialView(output);
        }
        else
        {
            await RenderPartialView(_razorViewRoute, output);
        }
    }

    /// <summary>
    /// Uses the HtmlHelper to render the partial view. Defaults tag name to null and adds child content if there was any.
    /// Will send the child class as the view model for the partial view.
    /// </summary>
    /// <param name="viewRoute"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    protected async Task RenderPartialView(string viewRoute, TagHelperOutput output) 
    {
        var childContent = await output.GetChildContentAsync();

        if (childContent is not null)
        {
            ChildContent = childContent;
        }

        var htmlHelper = GetHtmlHelper();

        var content = await htmlHelper.PartialAsync(viewRoute, this);

        output.Content.SetHtmlContent(content);
        output.TagName = null;
    }

    /// <summary>
    /// Render the razor view from the default route.
    /// </summary>
    /// <param name="output"></param>
    /// <returns></returns>
    protected async Task RenderPartialView(TagHelperOutput output)
    {
        string defaultViewPath = $"~/TagHelpers/{GetType().Namespace!.Split('.').Last()}/Default.cshtml";

        await RenderPartialView(defaultViewPath, output);
    }
}

