namespace CKE.Infra.Logging.Application
{
    using System;
    using Microsoft.Extensions.Logging;
    using Serilog.AspNetCore;

    public class StartupLogger : IStartLogger
    {
        private readonly ILogger _microsoftLogger;
        private readonly ILoggerFactory _serilogFactory;

        public StartupLogger(Serilog.ILogger logger)
        {
            _serilogFactory = new SerilogLoggerFactory(logger);
            _microsoftLogger = _serilogFactory.CreateLogger(typeof(StartupLogger).Name);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _microsoftLogger.BeginScope(state);
        }

        public void Close()
        {
            (_microsoftLogger as IDisposable)?.Dispose();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _microsoftLogger.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _microsoftLogger.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}
