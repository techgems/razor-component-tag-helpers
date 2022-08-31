using Microsoft.Extensions.DependencyInjection;
using ServerComponents.ServerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerComponents.DI;

public static class ComponentsDependencyInjection
{
    public static void AddComponentDependencies(this IServiceCollection services)
    {
        services.AddScoped<IRazorRenderer, RazorRenderer>();
    }
}
