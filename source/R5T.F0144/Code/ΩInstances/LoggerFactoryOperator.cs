using System;


namespace R5T.F0144
{
    public class LoggerFactoryOperator : ILoggerFactoryOperator
    {
        #region Infrastructure

        public static ILoggerFactoryOperator Instance { get; } = new LoggerFactoryOperator();


        private LoggerFactoryOperator()
        {
        }

        #endregion
    }
}
