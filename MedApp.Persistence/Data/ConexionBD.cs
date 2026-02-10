using MedApp.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace MedApp.Persistence.Data
{
    public class ConexionBD : DbContext
    {

        public ConexionBD(DbContextOptions<ConexionBD> options ) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; } 
        public DbSet<Consulta> Consultas { get; set; }
    }
}
