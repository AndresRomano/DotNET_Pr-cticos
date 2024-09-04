using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarea03.Models;

namespace Tarea03.Controllers
{
    [Route("Libro")]
    public class LibroController : Controller
    {
        // Lista estática para almacenar los libros en memoria
        private static List<Libro> _libro = new List<Libro>();

        // GET: Libro/Index
        [HttpGet("")]
        public IActionResult Index()
        {
            return View("LibroList", _libro); 
        }

        // GET: Libro/Details/5
        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            var libro = _libro.FirstOrDefault(l => l.Id == id);
            if (libro == null)
            {
                return NotFound();
            }
            return View("LibroDetails", libro); 
        }

        // GET: Libro/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View("LibroCreate"); 
        }

        // POST: Libro/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ISBN,Titulo,Autor,FechaPublicacion")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                // Generar un Id único si no está asignado
                if (_libro.Count > 0)
                {
                    libro.Id = _libro.Max(l => l.Id) + 1;
                }
                else
                {
                    libro.Id = 1;
                }

                _libro.Add(libro);
                return RedirectToAction("Index");
            }
            return View("LibroCreate", libro); 
        }

        // GET: Libro/Edit/5
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var libro = _libro.FirstOrDefault(l => l.Id == id);
            if (libro == null)
            {
                return NotFound();
            }
            return View("LibroEdit", libro);
        }
        // POST: Libro/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ISBN,Titulo,Autor,FechaPublicacion")] Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var libroExistente = _libro.FirstOrDefault(l => l.Id == id);
                if (libroExistente == null)
                {
                    return NotFound();
                }

                libroExistente.ISBN = libro.ISBN;
                libroExistente.Titulo = libro.Titulo;
                libroExistente.Autor = libro.Autor;
                libroExistente.FechaPublicacion = libro.FechaPublicacion;

                return RedirectToAction("Index");
            }
            return View("LibroEdit", libro); 
        }

        // GET: Libro/Delete/5
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var libro = _libro.FirstOrDefault(l => l.Id == id);
            if (libro == null)
            {
                return NotFound();
            }
            return View("LibroDelete", libro); 
        }

        // POST: Libro/Delete/5
        [HttpPost("Delete/{id}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var libro = _libro.FirstOrDefault(l => l.Id == id);
            if (libro != null)
            {
                _libro.Remove(libro);
            }
            return RedirectToAction("Index");
        }
    }
}
