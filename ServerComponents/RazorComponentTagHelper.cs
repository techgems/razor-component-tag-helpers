using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TagHelperComponents.ServerComponents;

public abstract class RazorComponentTagHelper : TagHelper
{

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext? ViewContext { get; set; }

    protected async Task<string> GetChildHtmlAsString(TagHelperOutput output)
    {
        var childContent = await output.GetChildContentAsync();
        var trimmedContent = childContent.GetContent().Trim();

        return trimmedContent;
    }

    protected async Task RenderPartialView<T>(string viewRoute, TagHelperOutput output, T model) where T : ComponentTagHelperModel
    {
        if (ViewContext is null)
        {
            throw new ArgumentNullException(nameof(ViewContext));
        }

        var childContent = await output.GetChildContentAsync();

        if (childContent is not null)
        {
            model.ChildContent = childContent;
        }

        IHtmlHelper? htmlHelper = ViewContext.HttpContext.RequestServices.GetService<IHtmlHelper>();
        ArgumentNullException.ThrowIfNull(htmlHelper);

        (htmlHelper as IViewContextAware)!.Contextualize(ViewContext);
        var content = await htmlHelper.PartialAsync(viewRoute, model);

        output.Content.SetHtmlContent(content);
        output.TagName = null;
    }

    protected async Task RenderPartialView(string viewRoute, TagHelperOutput output)
    {
        if (ViewContext is null)
        {
            throw new ArgumentNullException(nameof(ViewContext));
        }

        IHtmlHelper? htmlHelper = ViewContext.HttpContext.RequestServices.GetService<IHtmlHelper>();
        ArgumentNullException.ThrowIfNull(htmlHelper);

        (htmlHelper as IViewContextAware)!.Contextualize(ViewContext);
        var content = await htmlHelper.PartialAsync(viewRoute, null);


        output.TagName = null;
        output.Content.SetHtmlContent(content);
    }

    protected async Task RenderPartialView(TagHelperOutput output)
    {
        string defaultViewPath = $"~/TagHelpers/{GetType().Namespace!.Split('.').Last()}/Default.cshtml";

        await RenderPartialView(defaultViewPath, output);
    }

    protected async Task RenderPartialView<T>(TagHelperOutput output, T model) where T : ComponentTagHelperModel
    {
        string defaultViewPath = $"~/TagHelpers/{GetType().Namespace!.Split('.').Last()}/Default.cshtml";

        await RenderPartialView(defaultViewPath, output, model);
    }
}

