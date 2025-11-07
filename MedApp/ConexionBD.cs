using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MedApp
{
    public class ConexionBD
    {
        private readonly string connectionString;

        public ConexionBD()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");

            if (!File.Exists(path))
            {
                throw new Exception("No se encontro el archivo config.txt dentro de la carpeta del programa.");
            }
            connectionString = File.ReadAllText(path).Trim();
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("El archivo config.txt está vacío o no contiene una cadena de conexión válida.");
            }
        }

       
        public SqlConnection ObtenerCadenaConexion()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
        public User Autenticar(string username, string password)
        {
            try
            {
                

                using (SqlConnection conn = ObtenerCadenaConexion())
                {
                    conn.Open();
                    string query = @"SELECT Id, UserName, PasswordKey, Roles, Activo
                                    FROM LoginUser
                                    WHERE UserName = @userName AND passwordKey = @passwordKey AND Activo = 1" ;

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userName", username);
                        cmd.Parameters.AddWithValue("@passwordKey", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    Id = reader.GetInt32(0),
                                    UserName = reader.GetString(1),
                                    PasswordKey = reader.GetString(2),
                                    Roles = reader.GetString(3),
                                    Activo = reader.GetBoolean(4)
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al autenticar: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            
        }
    }
}
