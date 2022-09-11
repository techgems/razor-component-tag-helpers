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

    protected async Task RenderPartialView<T>(string viewRoute, TagHelperOutput output, T model) where T : ComponentTagHelperModel
    {
        var childContent = await output.GetChildContentAsync();

        if (childContent is not null)
        {
            model.ChildContent = childContent;
        }

        var htmlHelper = GetHtmlHelper();

        var content = await htmlHelper.PartialAsync(viewRoute, model);

        output.Content.SetHtmlContent(content);
        output.TagName = null;
    }

    protected async Task RenderPartialView(string viewRoute, TagHelperOutput output)
    {
        var htmlHelper = GetHtmlHelper();

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

