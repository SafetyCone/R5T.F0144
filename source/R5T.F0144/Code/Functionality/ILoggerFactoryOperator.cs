using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.T0132;


namespace R5T.F0144
{
    [FunctionalityMarker]
    public partial interface ILoggerFactoryOperator : IFunctionalityMarker
    {
        public ILoggerFactory Get_LoggerFactory_OrNullLoggerFactory(IServiceProvider serviceProvider)
        {
            var hasLoggerFactory = serviceProvider.Try_Get_Service<ILoggerFactory>(
                out var loggerFactory);

            if (!hasLoggerFactory)
            {
                // Use the default.
                loggerFactory = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();
            }

            return loggerFactory;
        }
    }
}
