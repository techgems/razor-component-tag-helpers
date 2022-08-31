using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerComponents.ServerComponents;

public class RazorRenderer : IRazorRenderer
{
    private readonly IHtmlHelper _html;

    public RazorRenderer(IHtmlHelper html)
    {
        _html = html;
    }

    public async Task<IHtmlContent> RenderAsContent<T>(string path, T? model, ViewContext viewContext)
    {
        (_html as IViewContextAware)!.Contextualize(viewContext);

        var content = await _html.PartialAsync(path, model);

        return content;
    }
}
