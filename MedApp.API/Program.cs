
using Microsoft.Extensions.Options;
using System.Reflection;
using MedApp.IOC;
using MedApp.Persistence.Data;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Configurar CORS
        builder.Services.AddCors(Options =>
        {
            Options.AddPolicy("DevPolicy", policy =>
            {
                policy.WithOrigins("http://localhost:5052")
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Registrar servicio personalizado
        builder.Services.AddDependencies();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseCors("DevPolicy");
        app.MapControllers();

        app.Run();
    }
}

