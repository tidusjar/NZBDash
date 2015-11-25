using System;
using NLog;

using ILogger = NZBDash.Common.Interfaces.ILogger;

namespace NZBDash.Common
{
    public class NLogLogger : ILogger
    {
        private readonly Logger _logger;

        public NLogLogger(Type t)
        {
            _logger = LogManager.GetLogger(t.Name);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            _logger.Error(x);
        }

        public void Error(string message, Exception x)
        {
            _logger.Error(x, message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            _logger.Fatal(x);
        }
    }
}

