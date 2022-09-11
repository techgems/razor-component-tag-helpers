using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorTagHelpers.ServerComponents;

public abstract class RazorTagHelperModel
{
    public TagHelperContent? ChildContent { get; set; }
}
