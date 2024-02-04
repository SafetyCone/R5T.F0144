using System;


namespace R5T.F0144
{
    public static class Instances
    {
        public static L0066.IActionOperator ActionOperator => L0066.ActionOperator.Instance;
        public static IComponentRenderingContextOperator ComponentRenderingContextOperator => F0144.ComponentRenderingContextOperator.Instance;
        public static ILoggerFactoryOperator LoggerFactoryOperator => F0144.LoggerFactoryOperator.Instance;
        public static F0028.IServiceCollectionOperator ServiceCollectionOperator => F0028.ServiceCollectionOperator.Instance;
    }
}