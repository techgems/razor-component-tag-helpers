using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerComponents.ServerComponents;

public interface IRazorRenderer
{
    Task<IHtmlContent> RenderAsContent<T>(string path, T? model, ViewContext viewContext);
}
