using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using CrudNet8MVC.Data;
using CrudNet8MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudNet8MVC.Controllers
{
    public class InicioController : Controller
    {
        private readonly DataContext _contexto; // Inyeccion de dependencias (dependencia en DataContext (Modelos))

        public InicioController(DataContext contexto) // Constructor de la clase InicioController donde pasamos la dependencia Modelos.
        {
            _contexto = contexto;   
        }

        public async Task<IActionResult> Index(string filtro)
        {
            var contactos = from c in _contexto.Contact // es lo mismo que contactos = _contexto.contact solo que en linq y recorriendo c por todos los registros
                            select c;

            if (!string.IsNullOrEmpty(filtro))
            {
                contactos = contactos.Where(c =>
                    c.Id.ToString().Contains(filtro) ||
                    c.Name.Contains(filtro));
            }

            ViewData["FiltroActual"] = filtro;

            return View(await contactos.ToListAsync());
        }


        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contact contacto)
        {
            if(ModelState.IsValid)
            {
                //Agrego la fecha de registro
                contacto.FechaCreacion = DateTime.Now;

                _contexto.Add(contacto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction("Index"); // O, RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var contacto = _contexto.Contact.Find(id);

            if (contacto == null)
            {
                return NotFound();

            }
            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Contact contacto)
        {
            if (ModelState.IsValid)
            {
                contacto.FechaModificacion = DateTime.Now;

                _contexto.Update(contacto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction("Index"); // O, RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = _contexto.Contact.Find(id);

            if (contacto == null)
            {
                return NotFound();

            }

            return View(contacto);
        }

        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = _contexto.Contact.Find(id);

            if (contacto == null)
            {
                return NotFound();

            }

            return View(contacto);
        }

        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarContacto(int? id)
        {
            var contacto = await _contexto.Contact.FindAsync(id);
            if (contacto == null)
            {
                return View();
            }

            //Borrado
            _contexto.Contact.Remove(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }

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
