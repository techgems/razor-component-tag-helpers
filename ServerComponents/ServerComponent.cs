using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace ServerComponents.ServerComponents;

public abstract class ServerComponent : TagHelper
{
    private readonly IRazorRenderer _razorRenderer;

    public ServerComponent(IRazorRenderer razorRenderer)
    {
        _razorRenderer = razorRenderer;
    }

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext? ViewContext { get; set; }

    protected async Task<string> GetChildHtmlAsString(TagHelperOutput output)
    {
        var childContent = await output.GetChildContentAsync();
        var trimmedContent = childContent.GetContent().Trim();

        return trimmedContent;
    }

    protected async Task RenderPartialView<T>(string viewRoute, TagHelperOutput output, T model) where T : ServerComponentModel
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

        var content = await _razorRenderer.RenderAsContent(viewRoute, model, ViewContext);
        output.Content.SetHtmlContent(content);
        output.TagName = null;
    }

    protected async Task RenderPartialView(string viewRoute, TagHelperOutput output)
    {
        if (ViewContext is null)
        {
            throw new ArgumentNullException(nameof(ViewContext));
        }

        var content = await _razorRenderer.RenderAsContent<object>(viewRoute, null, ViewContext);

        output.TagName = null;
        output.Content.SetHtmlContent(content);
    }
}

