using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedApp
{
    public static class AppConfig
    {
        public static IConfiguration Configuration { get; }
        static AppConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public static string ApiUrl => Configuration["ApiSettings:BaseUrl"] ?? throw new Exception("ApiSettings not found in configuration");
    }
}
