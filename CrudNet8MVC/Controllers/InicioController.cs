using System.Diagnostics;
using CrudNet8MVC.Data;
using CrudNet8MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudNet8MVC.Controllers
{
    public class InicioController : Controller
    {
        private readonly DataContext _contexto; // Inyección de dependencias (dependencia en DataContext / Modelos)

        public InicioController(DataContext contexto) // Constructor: recibimos la dependencia del contexto
        {
            _contexto = contexto;
        }

        // ---------- LISTAR + BUSCAR ----------
        public async Task<IActionResult> Index(string filtro)
        {
            var libros = from l in _contexto.Libro // recorre todos los registros de Libro con LINQ
                         select l;

            if (!string.IsNullOrEmpty(filtro))
            {
                libros = libros.Where(l =>
                    l.Id.ToString().Contains(filtro) ||
                    l.Titulo.Contains(filtro) ||
                    l.Autor.Contains(filtro) ||
                    l.Isbn.Contains(filtro));
            }

            ViewData["FiltroActual"] = filtro;

            return View(await libros.ToListAsync());
        }

        // ---------- CREAR ----------
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(libro);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(libro); // devolvemos el modelo para no perder lo digitado ni las validaciones
        }

        // ---------- EDITAR ----------
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _contexto.Libro.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Libro libro)
        {
            if (ModelState.IsValid)
            {
                _contexto.Update(libro);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(libro);
        }

        // ---------- DETALLE ----------
        [HttpGet]
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _contexto.Libro.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // ---------- BORRAR ----------
        [HttpGet]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _contexto.Libro.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarLibro(int? id)
        {
            var libro = await _contexto.Libro.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _contexto.Libro.Remove(libro);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ---------- OTROS ----------
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}