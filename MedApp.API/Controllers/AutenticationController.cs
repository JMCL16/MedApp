
using Microsoft.AspNetCore.Mvc;


namespace MedApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {
        /*
        private readonly string _connectionString;

        public AutenticationController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            // Implement login logic here using _connectionString
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    // Example query - replace with actual authentication logic
                    string query = @"SELECT Id, UserName, PasswordKey, Roles, Activo FROM Users WHERE UserName = @UserName AND Password = @Password AND Activo = 1" ;
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserName", request.UserName);
                        cmd.Parameters.AddWithValue("@Password", request.Password);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                User user = new User
                                {
                                    Id = reader.GetInt32(0),
                                    UserName = reader.GetString(1),
                                    PasswordKey = reader.GetString(2),
                                    Roles = reader.GetString(3),
                                    Activo = reader.GetBoolean(4)
                                };
                                return Ok(new LoginResponse
                                {
                                    Success = true,
                                    Message = "Login successful",
                                    User = user
                                });
                            }
                        }                        
                    }
                }
                return Unauthorized(new LoginResponse
                {
                    Success = false,
                    Message = "Invalid username or password",
                    User = null
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new LoginResponse
                {
                    Success = false,
                    Message = $"An error occurred during login: {ex.Message}",
                    User = null
                });
            }
        }*/
    }
}
