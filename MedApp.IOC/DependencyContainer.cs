using Microsoft.Extensions.DependencyInjection;
using MedApp.Persistence.Repositories;
using MedApp.Application.Interfaces.Repositories;
using MedApp.Application.Services;
using MedApp.Application.Interfaces.IServices;
using MedApp.Application.Extension.Validators.PacienteValidators;

namespace MedApp.IOC
{
    public static class DependencyContainer
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IPacienteServices, PacienteService>();
            services.AddScoped<PacienteValidator>();
        }
    }
}
