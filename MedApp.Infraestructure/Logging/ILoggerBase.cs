using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp.Infraestructure.Logging
{
    public interface ILoggerBase <TEntity> where TEntity : class
    {
        void LogInformation(string message, Object entity);
        void LogInformation(string message);
        void LogError(string message, Exception ex);
        void LogError(string message);
    }
}
