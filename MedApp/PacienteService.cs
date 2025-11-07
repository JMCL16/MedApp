using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedApp
{
    public class PacienteService
    {
        private readonly ConexionBD _conexionBD;
        
        public PacienteService(ConexionBD conexionBD)
        {
            _conexionBD = conexionBD;
        }

        public bool GuardarPaciente(Paciente paciente)
        {
            using(SqlConnection conn = _conexionBD.ObtenerCadenaConexion()) {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        //Insertar paciente
                        string queryPaciente = @"INSERT INTO Pacientes (Cedula, Nombre, Apellido, FechaNacimiento, Genero, Nacionalidad, Direccion, Ocupacion, Telefono, OperacionesPrevias, AntecedentesFamiliares, UsuarioRegistro) VALUES (@Cedula, @Nombre, @Apellido, @FechaNacimiento, @Genero, @Nacionalidad, @Direccion, @Ocupacion, @Telefono, @OperacionesPrevias, @AntecedentesFamiliares, @UsuarioRegistro);
                          SELECT CAST(SCOPE_IDENTITY() as int);";

                        int pacienteId;
                        using (SqlCommand cmd = new SqlCommand(queryPaciente, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Cedula", paciente.Cedula);
                            cmd.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                            cmd.Parameters.AddWithValue("@Apellido", paciente.Apellido);
                            cmd.Parameters.AddWithValue("@FechaNacimiento", paciente.FechaNacimiento);
                            cmd.Parameters.AddWithValue("@Genero", paciente.Genero ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Nacionalidad", paciente.Nacionalidad ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Direccion", paciente.Direccion ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Ocupacion", paciente.Ocupacion ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Telefono", paciente.Telefono ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@OperacionesPrevias", paciente.OperacionesPrevias ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@AntecedentesFamiliares", paciente.AntecedentesFamiliares ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@UsuarioRegistro", SesionActual.usuarioActual.Id);

                            pacienteId = (int)cmd.ExecuteScalar();
                        }

                        if (paciente.AntecedentesPatologicos != null && paciente.AntecedentesPatologicos.Count > 0)
                        {
                            string queryAntecedentes = @"INSERT INTO AntecedentesPatologicos (PacienteId, Antecedente) Values (@PacienteId, @Antecedente)";

                            foreach (string antecedente in paciente.AntecedentesPatologicos)
                            {
                                using (SqlCommand cmd = new SqlCommand(queryAntecedentes, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@PacienteId", pacienteId);
                                    cmd.Parameters.AddWithValue("@Antecedente", antecedente);
                                    cmd.ExecuteNonQuery();
                                }
                            }
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

        public Paciente ObtenerPacientePorCedula(string cedula)
        {
            using (SqlConnection conn = _conexionBD.ObtenerCadenaConexion())
            {
                conn.Open();
                string queryPaciente = @"SELECT Id, Cedula, Nombre, Apellido, FechaNacimiento, Edad, Genero, 
                        Nacionalidad, Direccion, Ocupacion, Telefono, OperacionesPrevias, 
                        AntecedentesFamiliares, FechaRegistro, UsuarioRegistro, Activo FROM Pacientes WHERE Cedula = @Cedula AND Activo = 1";

                Paciente paciente = null;

                using (SqlCommand cmd = new SqlCommand(queryPaciente, conn))
                {
                    cmd.Parameters.AddWithValue("@Cedula", cedula);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            paciente = new Paciente
                            {
                                Id = reader.GetInt32(0),
                                Cedula = reader.GetString(1),
                                Nombre = reader.GetString(2),
                                Apellido = reader.GetString(3),
                                FechaNacimiento = reader.GetDateTime(4),
                                Edad = reader.GetInt32(5).ToString(),
                                Genero = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                Nacionalidad = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                Direccion = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                Ocupacion = reader.IsDBNull(9) ? "" : reader.GetString(9),
                                Telefono = reader.IsDBNull(10) ? "" : reader.GetString(10),
                                OperacionesPrevias = reader.IsDBNull(11) ? "" : reader.GetString(11),
                                AntecedentesFamiliares = reader.IsDBNull(12) ? "" : reader.GetString(12),
                                FechaRegistro = reader.GetDateTime(13),
                                UsuarioRegistro = reader.GetInt32(14),
                                Activo = reader.GetBoolean(15)
                            };
                        }
                    }
                }

                if (paciente != null)
                {
                    string queryAntecedentes = @"SELECT Antecedente FROM AntecedentesPatologicos 
                                            WHERE PacienteId = @PacienteId";

                    using (SqlCommand cmd = new SqlCommand(queryAntecedentes, conn))
                    {
                        cmd.Parameters.AddWithValue("@PacienteId", paciente.Id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                paciente.AntecedentesPatologicos.Add(reader.GetString(0));
                            }
                        }
                    }
                }

                
                return paciente;
            }

        }

        public List<Paciente> BuscarPacientesPorNombre(string nombre)
        {
            List<Paciente> pacientes = new List<Paciente>();

            using (SqlConnection conn = _conexionBD.ObtenerCadenaConexion())
            {
                conn.Open();

                string query = @"SELECT Id, Cedula, Nombre, Apellido, FechaNacimiento, Edad, 
                        Genero, Nacionalidad, Direccion, Ocupacion, Telefono, 
                        OperacionesPrevias, AntecedentesFamiliares
                        FROM Pacientes 
                        WHERE (Nombre LIKE @Nombre OR Apellido LIKE @Nombre) 
                        AND Activo = 1
                        ORDER BY Nombre, Apellido";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Paciente paciente = new Paciente
                            {
                                Id = reader.GetInt32(0),
                                Cedula = reader.GetString(1),
                                Nombre = reader.GetString(2),
                                Apellido = reader.GetString(3),
                                FechaNacimiento = reader.GetDateTime(4),
                                Edad = reader.IsDBNull(5) ? "0" : reader.GetInt32(5).ToString(),
                                Genero = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                Nacionalidad = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                Direccion = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                Ocupacion = reader.IsDBNull(9) ? "" : reader.GetString(9),
                                Telefono = reader.IsDBNull(10) ? "" : reader.GetString(10),
                                OperacionesPrevias = reader.IsDBNull(11) ? "" : reader.GetString(11),
                                AntecedentesFamiliares = reader.IsDBNull(12) ? "" : reader.GetString(12)
                            };

                            pacientes.Add(paciente);
                        }
                    }
                }
            }
            
            return pacientes;
        }
    }
}
