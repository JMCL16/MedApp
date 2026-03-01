
using MedApp.Application.DTOs.Login;
using MedApp.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;


namespace MedApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {
        private readonly ILogger<AutenticationController> _logger;
        private readonly IAuthService _authService;

        public AutenticationController(ILogger<AutenticationController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);

            if (!result.IsSuccess)
            {
                return Unauthorized(result);
            }
            return Ok(result.Data);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDTO registerDTO)
        {

            var result = await _authService.RegisterAsync(registerDTO);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateRol([FromBody] ActualizarRolDTO updateRolDTO)
        {
            var result = await _authService.UpdateRolAsync(updateRolDTO);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
