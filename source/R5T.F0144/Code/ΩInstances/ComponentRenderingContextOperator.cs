using System;


namespace R5T.F0144
{
    public class ComponentRenderingContextOperator : IComponentRenderingContextOperator
    {
        #region Infrastructure

        public static IComponentRenderingContextOperator Instance { get; } = new ComponentRenderingContextOperator();


        private ComponentRenderingContextOperator()
        {
        }

        #endregion
    }
}
