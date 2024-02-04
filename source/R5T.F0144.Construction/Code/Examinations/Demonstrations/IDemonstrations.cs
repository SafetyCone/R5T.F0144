using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.T0141;


namespace R5T.F0144.Construction
{
    [DemonstrationsMarker]
    public partial interface IDemonstrations : IDemonstrationsMarker
    {
        public async Task Render_Component_ToString()
        {
            var pairs = new Dictionary<string, object>
            {
                { "Message", "Hello from the Render Message component!" }
            };

            var output = await Instances.BlazorRenderingOperator.Render<RenderMessage>(
                pairs);

            Console.WriteLine(output);
        }
    }
}
