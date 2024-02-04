using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using R5T.T0132;

using R5T.F0144.Extensions;


namespace R5T.F0144
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Based on <see href="https://andrewlock.net/exploring-the-dotnet-8-preview-rendering-blazor-components-to-a-string/#rendering-components-without-a-di-container"/>
    /// </remarks>
    [FunctionalityMarker]
    public partial interface IBlazorRenderingOperator : IFunctionalityMarker
    {
        public Task<string> Render<TComponent>()
            where TComponent : IComponent
            => this.Render<TComponent>(
                ParameterView.Empty);

        public Task<string> Render<TComponent>(
            Dictionary<string, object> pairs)
            where TComponent : IComponent
            => this.Render<TComponent>(
                ParameterView.FromDictionary(pairs));


        public async Task<string> Render<TComponent>(
            ParameterView parameters)
            where TComponent : IComponent
        {
            var services = new ServiceCollection();

            await using var serviceProvider = services.BuildServiceProvider();

            using var loggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();

            await using var htmlRenderer = new HtmlRenderer(
                serviceProvider,
                loggerFactory);

            var output = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                var output = await htmlRenderer.RenderComponentAsync<TComponent>(parameters);
                return output.ToHtmlString();
            });

            return output;
        }

        public Task<string> Render<TComponent>(
            ComponentRenderingContext<TComponent> componentRenderingContext)
            where TComponent : IComponent
            => Instances.ComponentRenderingContextOperator.Render(
                componentRenderingContext);

        public Task<string> Render<TComponent>(
            Action<ComponentRenderingContext<TComponent>> componentRenderingContextAction = default)
            where TComponent : IComponent
            => Instances.ComponentRenderingContextOperator.Render(
                componentRenderingContextAction);
    }
}
