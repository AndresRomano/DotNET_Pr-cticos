using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Tarea01.Models;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private static List<Tarea> Tareas = new List<Tarea>
        {
            new Tarea { Id = 1, Nombre = "Tarea 1", Descripcion = "Descripción de la tarea 1", DuracionHoras = 2, Responsable = "Juan", Fecha = DateTime.Now },
            new Tarea { Id = 2, Nombre = "Tarea 2", Descripcion = "Descripción de la tarea 2", DuracionHoras = 4, Responsable = "Ana", Fecha = DateTime.Now },
            new Tarea { Id = 3, Nombre = "Tarea 3", Descripcion = "Descripción de la tarea 3", DuracionHoras = 1, Responsable = "Luis", Fecha = DateTime.Now }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Tarea>> GetTareas()
        {
            return Tareas;
        }

        [HttpGet("{id}")]
        public ActionResult<Tarea> GetTarea(int id)
        {
            var tarea = Tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }
            return tarea;
        }

        [HttpPost]
        public ActionResult<Tarea> PostTarea(Tarea tarea)
        {
            tarea.Id = Tareas.Count + 1; // Generar un ID sencillo
            Tareas.Add(tarea);
            return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, tarea);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTarea(int id)
        {
            var tarea = Tareas.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }
            Tareas.Remove(tarea);
            return NoContent();
        }
    }
}
