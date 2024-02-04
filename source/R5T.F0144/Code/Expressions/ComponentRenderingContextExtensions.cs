using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;


namespace R5T.F0144.Extensions
{
    public static class ComponentRenderingContextExtensions
    {
        public static async Task<TComponentRenderer> Modify_With<TComponentRenderer>(this TComponentRenderer componentRenderer,
            Func<TComponentRenderer, Task> componentRendererAction)
            where TComponentRenderer : ComponentRenderingContext
        {
            await Instances.ComponentRenderingContextOperator.Modify_With(
                componentRenderer,
                componentRendererAction);

            return componentRenderer;
        }

        public static TComponentRenderer Modify_With<TComponentRenderer>(this TComponentRenderer componentRenderer,
            Action<TComponentRenderer> componentRendererAction)
            where TComponentRenderer : ComponentRenderingContext
        {
            Instances.ComponentRenderingContextOperator.Modify_With(
                componentRenderer,
                componentRendererAction);

            return componentRenderer;
        }

        public static ComponentRenderingContext<TComponent> Set_Parameter<TComponent, TValue>(this ComponentRenderingContext<TComponent> componentRenderingContext,
            Expression<Func<TComponent, TValue>> parameterSelector,
            TValue value)
            where TComponent : IComponent
        {
            Instances.ComponentRenderingContextOperator.Set_Parameter(
                componentRenderingContext,
                parameterSelector,
                value);

            return componentRenderingContext;
        }
    }
}
