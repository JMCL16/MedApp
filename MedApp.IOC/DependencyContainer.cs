using Microsoft.Extensions.DependencyInjection;
using MedApp.Persistence.Repositories;
using MedApp.Application.Interfaces.Repositories;
using MedApp.Application.Services;
using MedApp.Application.Interfaces.IServices;
using MedApp.Application.Extension.Validators.PacienteValidators;
using MedApp.Application.Extension.Validators.ConsultaValidators;
using MedApp.Application.Interfaces.IRepositories;

namespace MedApp.IOC
{
    public static class DependencyContainer
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IPacienteServices, PacienteService>();
            services.AddScoped<PacienteValidator>();
            services.AddScoped<PacienteUpdateValidator>();
            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddScoped<IConsultaService, ConsultaService>();
            services.AddScoped<ConsultaValidator>();
            services.AddScoped<ConsultaUpdateValidator>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();

        }
    }
}
