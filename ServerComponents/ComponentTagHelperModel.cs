using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagHelperComponents.ServerComponents;

public abstract class ComponentTagHelperModel
{
    public TagHelperContent? ChildContent { get; set; }
}
