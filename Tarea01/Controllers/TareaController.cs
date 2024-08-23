using Tarea01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace Tarea01.Controllers
{
    [ApiController]
    [Route("api/tareas")]
    public class TareaController : Controller
    {
        private readonly ILogger<TareaController> _logger;
        private IList<Tarea> _tareaList;

        public TareaController(ILogger<TareaController> logger)
        {
            _logger = logger;
            _tareaList = new List<Tarea>
            {
                new Tarea { Id = 1, Nombre = "Tarea 1", Descripcion = "Descripción de la tarea 1", DuracionHoras = 2, Responsable = "Juan", Fecha = DateTime.Now },
                new Tarea { Id = 2, Nombre = "Tarea 2", Descripcion = "Descripción de la tarea 2", DuracionHoras = 4, Responsable = "Ana", Fecha = DateTime.Now },
                new Tarea { Id = 3, Nombre = "Tarea 3", Descripcion = "Descripción de la tarea 3", DuracionHoras = 1, Responsable = "Luis", Fecha = DateTime.Now }
            };
        }

        [HttpGet]
        public ActionResult<IList<Tarea>> GetAll()
        {
            _logger.LogInformation("Retorno lista de tareas");
            return Ok(_tareaList.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Tarea> GetById(int id)
        {
            _logger.LogInformation($"Retorno tarea con ID {id}");
            var tarea = _tareaList.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }
            return Ok(tarea);
        }

        [HttpPost]
        public ActionResult Nuevo([FromBody] Tarea tarea)
        {
            _logger.LogInformation("Nueva tarea añadida");
            tarea.Id = _tareaList.Count + 1; // Genera un nuevo ID sencillo
            _tareaList.Add(tarea);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Borrar(int id)
        {
            _logger.LogInformation($"Borrar tarea con ID {id}");
            var tarea = _tareaList.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }
            _tareaList.Remove(tarea);
            return Ok();
        }
    }
}
