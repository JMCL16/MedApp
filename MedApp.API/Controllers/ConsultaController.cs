using MedApp.Application.DTOs.Consultas;
using MedApp.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly ILogger<ConsultaController> _logger;
        private readonly IConsultaService _consultaService;

        public ConsultaController(ILogger<ConsultaController> logger, IConsultaService consultaService)
        {
            _logger = logger;
            _consultaService = consultaService;
        }

        // POST: ConsultaController/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConsultaDTO consultaDTO)
        {
            var result = await _consultaService.CrearConsultaAsync(consultaDTO);
            if (result.IsSuccess)
            {
                _logger.LogInformation("Consulta creada exitosamente: {Message}", result.Message);
                return Ok(result);
            }
            else
            {
                _logger.LogError("Error al crear consulta: {Message}", result.Message);
                return BadRequest(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByCedula(string cedula)
        {
            var result = await _consultaService.ObtenerPorCedulaAsync(cedula);
            if (result.IsSuccess)
            {
                _logger.LogInformation("Consulta obtenida exitosamente: {Message}", result.Message);
                return Ok(result);
            }
            else
            {
                _logger.LogError("Error al obtener consulta: {Message}", result.Message);
                return BadRequest(result);
            }
        }

       

        // POST: ConsultaController/Edit/5
        [HttpPatch]
        public async Task<IActionResult> Edit([FromBody] ConsultaUpdateDTO consultaUpdateDto)
        {
            var result = await _consultaService.ActualizarConsultaAsync(consultaUpdateDto);
            if (result.IsSuccess)
            {
                _logger.LogInformation("Consulta actualizada exitosamente: {Message}", result.Message);
                return Ok(result);
            }
            else
            {
                _logger.LogError("Error al actualizar consulta: {Message}", result.Message);
                return BadRequest(result);
            }
        }
        /*

        // GET: ConsultaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ConsultaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
