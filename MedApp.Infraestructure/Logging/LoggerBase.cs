using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Infraestructure.Logging
{
    public class LoggerBase<T> : ILoggerBase<T> where T : class 
    {
        public readonly ILoggerBase<T> _Logger;
        public LoggerBase(ILoggerBase<T> logger)
        {
            _Logger = logger;
        }

        public void LogError(string message, Exception ex)
        {
            _Logger.LogError(message, ex);
        }

        public void LogError(string message)
        {
            _Logger.LogError(message);
        }

        public void LogInformation(string message, object entity)
        {
            _Logger.LogInformation(message, entity);
        }

        public void LogInformation(string message)
        {
            _Logger.LogInformation(message);
        }
    }
}
