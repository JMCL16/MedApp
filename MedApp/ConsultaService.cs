using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedApp
{
    public class ConsultaService
    {
        private readonly ConexionBD _conexionBD;

        public ConsultaService(ConexionBD conexionBD)
        {
            _conexionBD = conexionBD;
        }
        public bool GuardarConsulta(Consulta consulta)
        {
            using (SqlConnection conn = _conexionBD.ObtenerCadenaConexion())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string queryConsulta = @"INSERT INTO Consulta (PacienteId, FechaConsulta, Diagnostico, Tratamiento)
                             VALUES (@PacienteId, @FechaConsulta, @Diagnostico, @Tratamiento); SELECT CAST(SCOPE_IDENTITY()AS INT);";

                        int consultaId;
                        using (SqlCommand cmd = new SqlCommand(queryConsulta, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@PacienteId", consulta.PacienteId);
                            cmd.Parameters.AddWithValue("@FechaConsulta", consulta.FechaConsulta);
                            cmd.Parameters.AddWithValue("@Diagnostico", consulta.Diagnostico);
                            cmd.Parameters.AddWithValue("@Tratamiento", consulta.Tratamiento);
                            
                            consultaId = (int)cmd.ExecuteScalar();
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(string.Format("Error al guardar paciente: {0}", ex.Message),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;

                    }
                    
                }
            }
        }
    }
}
