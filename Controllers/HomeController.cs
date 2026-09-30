using Microsoft.AspNetCore.Mvc;

namespace PrimerParcial1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumerosController : ControllerBase
    {
        [HttpGet("{numero}")]
        public IActionResult Sumar(int numero)
        {
            return Ok(numero + numero);
        }
    }
}