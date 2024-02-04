using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using R5T.F0144.Extensions;
using R5T.T0132;


namespace R5T.F0144
{
    [FunctionalityMarker]
    public partial interface IComponentRenderingContextOperator : IFunctionalityMarker
    {
        public IServiceCollection Get_InitialServices_WithHtmlRendererRequiredServices()
        {
            // No services are required by the HTML renderer.
            var output = Instances.ServiceCollectionOperator.New();
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Adapted from: https://github.com/conficient/BlazorTemplater/blob/91fa80058dde63e8866642460a587d0520377976/BlazorTemplater/ComponentRenderer.cs#L99
        /// Prior work: R5T.F0079.
        /// </remarks>
        public string Get_ParameterName<TComponent, TValue>(
            Expression<Func<TComponent, TValue>> parameterSelector)
            where TComponent : IComponent
        {
            if (false
                || parameterSelector.Body is not MemberExpression memberExpression
                || memberExpression.Member is not PropertyInfo propInfoCandidate)
            {
                throw new ArgumentException($"The parameter selector '{parameterSelector}' does not resolve to a public property on the component '{typeof(TComponent)}'.", nameof(parameterSelector));
            }

            var componentType = typeof(TComponent);

            var declaringTypeIsComponent = propInfoCandidate.DeclaringType == componentType;

            var propertyInfo = declaringTypeIsComponent
                ? componentType.GetProperty(propInfoCandidate.Name, propInfoCandidate.PropertyType)
                : propInfoCandidate
                ;

            var attribute = propertyInfo?.GetCustomAttribute<ParameterAttribute>(inherit: true);

            var attributeSelectFailed = false
                || propertyInfo is null
                || attribute is null
                ;

            if (attributeSelectFailed)
            {
                throw new ArgumentException($"The parameter selector '{parameterSelector}' does not resolve to a public property on the component '{typeof(TComponent)}' with a [Parameter] or [CascadingParameter] attribute.", nameof(parameterSelector));
            }

            var output = propertyInfo.Name;
            return output;
        }

        public async Task<TComponentRenderer> Modify_With<TComponentRenderer>(
            TComponentRenderer componentRenderer,
            Func<TComponentRenderer, Task> componentRendererAction)
            where TComponentRenderer : ComponentRenderingContext
        {
            await Instances.ActionOperator.Run_OkIfDefault(
                componentRenderer,
                componentRendererAction);

            return componentRenderer;
        }

        public TComponentRenderer Modify_With<TComponentRenderer>(
            TComponentRenderer componentRenderer,
            Action<TComponentRenderer> componentRendererAction)
            where TComponentRenderer : ComponentRenderingContext
        {
            Instances.ActionOperator.Run_OkIfDefault(
                componentRenderer,
                componentRendererAction);

            return componentRenderer;
        }

        public ComponentRenderingContext<TComponent> New_RenderingContext<TComponent>(IServiceCollection services)
            where TComponent : IComponent
        {
            var output = new ComponentRenderingContext<TComponent>
            {
                Services = services,
                Parameters = [],
            };

            return output;
        }

        public ComponentRenderingContext<TComponent> New_RenderingContext<TComponent>()
            where TComponent : IComponent
        {
            var services = this.Get_InitialServices_WithHtmlRendererRequiredServices();

            var output = this.New_RenderingContext<TComponent>(services);
            return output;
        }

        public async Task<string> Render<TComponent>(
            ComponentRenderingContext<TComponent> componentRenderingContext)
            where TComponent : IComponent
        {
            await using var serviceProvider = componentRenderingContext.Services.BuildServiceProvider();

            using var loggerFactory = Instances.LoggerFactoryOperator.Get_LoggerFactory_OrNullLoggerFactory(serviceProvider);

            await using var htmlRenderer = new HtmlRenderer(
                serviceProvider,
                loggerFactory);

            var parameters = ParameterView.FromDictionary(
                componentRenderingContext.Parameters);

            var output = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                var output = await htmlRenderer.RenderComponentAsync<TComponent>(
                    parameters);

                return output.ToHtmlString();
            });

            return output;
        }

        public async Task<string> Render<TComponent>(
            Action<ComponentRenderingContext<TComponent>> componentRenderingContextAction = default)
            where TComponent : IComponent
        {
            var componentRenderingContext = Instances.ComponentRenderingContextOperator.New_RenderingContext<TComponent>()
                .Modify_With(componentRenderingContextAction);

            var output = await this.Render(
                componentRenderingContext);

            return output;
        }

        public void Set_Parameter<TComponent, TValue>(
            ComponentRenderingContext<TComponent> componentRenderingContext,
            Expression<Func<TComponent, TValue>> parameterSelector,
            TValue value)
            where TComponent : IComponent
        {
            var parameterName = this.Get_ParameterName(parameterSelector);

            componentRenderingContext.Parameters.Add(parameterName, value);
        }
    }
}
