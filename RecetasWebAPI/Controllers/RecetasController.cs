using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecetasBack.dominio;
using RecetasBack.negocio.implementacion;
using RecetasBack.negocio.interfaz;

namespace RecetasWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetasController : ControllerBase
    {
        private IApplication app;

        public RecetasController()
        {
            app = new Application();
        }

        [HttpGet("ingredientes")]

        public IActionResult GetIngredientes()
        {
            return Ok(app.ObtenerIngredientes());
        }

        [HttpPost]
        public IActionResult PostReceta(Receta oReceta)
        {
            if(oReceta == null)
            {
                return BadRequest();
            }
            if(app.Save(oReceta))
            {
                return Ok("La receta se registro exitosamente");
            }
            else
            {
                return Ok("Error al registrar la receta");
            }
        }
    }
}
