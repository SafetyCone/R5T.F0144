using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Components;


namespace R5T.F0144
{
    public class ComponentRenderingContext
    {
        public IServiceCollection Services { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }


    /// <summary>
    /// A component-typed component rendering context.
    /// </summary>
    /// <typeparam name="TComponent">The type of component this rendering context will be used to render.</typeparam>
    /// <remarks>
    /// The component type is a dummy type here, but is used in component rendering context extension methods that use expressions on the component type to select properties of the
    /// component type.
    /// </remarks>
    public class ComponentRenderingContext<TComponent> :
        ComponentRenderingContext
        where TComponent : IComponent
    {
    }
}
