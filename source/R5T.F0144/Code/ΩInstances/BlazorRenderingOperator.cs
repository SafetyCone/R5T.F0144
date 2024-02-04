using System;


namespace R5T.F0144
{
    public class BlazorRenderingOperator : IBlazorRenderingOperator
    {
        #region Infrastructure

        public static IBlazorRenderingOperator Instance { get; } = new BlazorRenderingOperator();


        private BlazorRenderingOperator()
        {
        }

        #endregion
    }
}
