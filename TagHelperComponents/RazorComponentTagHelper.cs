using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TechGems.TagHelperComponents;

public abstract class RazorComponentTagHelper : TagHelper
{
    private readonly string? _razorViewRoute;

    public RazorComponentTagHelper()
    {

    }

    public RazorComponentTagHelper(string razorViewRoute)
    {
        _razorViewRoute = razorViewRoute;
    }

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext? ViewContext { get; set; }

    [HtmlAttributeNotBound]
    public TagHelperContent? ChildContent { get; set; }

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
    /// By default we will render the razor template based on the default route.
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

    protected async Task RenderPartialView(TagHelperOutput output)
    {
        string defaultViewPath = $"~/TagHelpers/{GetType().Namespace!.Split('.').Last()}/Default.cshtml";

        await RenderPartialView(defaultViewPath, output);
    }
}

