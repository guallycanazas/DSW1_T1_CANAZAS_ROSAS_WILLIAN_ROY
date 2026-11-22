using DSW1_T1_CANAZAS_ROSAS_WILLIAN_ROY.Data;
using DSW1_T1_CANAZAS_ROSAS_WILLIAN_ROY.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSW1_T1_CANAZAS_ROSAS_WILLIAN_ROY.Controllers;

public sealed class LibroController : Controller
{
    private readonly LibroRepository _libroRepository;
    private readonly ILogger<LibroController> _logger;

    public LibroController(LibroRepository libroRepository, ILogger<LibroController> logger)
    {
        _libroRepository = libroRepository;
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.Mensaje = TempData["Mensaje"];
        ViewBag.Error = TempData["Error"];

        try
        {
            return View(ListarLibros());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No se pudo listar los libros.");
            ViewBag.Error ??= "No se pudo conectar con la base de datos. Revise la cadena de conexión y ejecute el script SQL.";
            return View(Array.Empty<Libro>());
        }
    }

    [HttpGet]
    public IActionResult Buscar(string? autor)
    {
        if (string.IsNullOrWhiteSpace(autor))
        {
            return RedirectToAction(nameof(Index));
        }

        ViewBag.AutorBuscado = autor.Trim();
        try
        {
            return View("Index", ListarLibrosPorAutor(autor));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No se pudo buscar libros por autor.");
            ViewBag.Error = "No se pudo realizar la búsqueda. Revise la conexión con SQL Server.";
            return View("Index", Array.Empty<Libro>());
        }
    }

    [HttpGet]
    public IActionResult Crear() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Libro libro)
    {
        if (!ModelState.IsValid)
        {
            return View(libro);
        }

        try
        {
            var id = _libroRepository.Registrar(libro);
            TempData["Mensaje"] = $"El libro se registró correctamente con el código {id}.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No se pudo registrar el libro.");
            ViewBag.Error = "No se pudo registrar el libro. Revise la conexión y los datos de la base de datos.";
            return View(libro);
        }
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        try
        {
            var libro = _libroRepository.ObtenerPorId(id);
            if (libro is null)
            {
                TempData["Error"] = "No se encontró el libro solicitado.";
                return RedirectToAction(nameof(Index));
            }

            return View(libro);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No se pudo cargar el libro {Id} para editarlo.", id);
            TempData["Error"] = "No se pudo cargar el libro. Revise la conexión con SQL Server.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id, Libro libro)
    {
        if (id != libro.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(libro);
        }

        try
        {
            if (!_libroRepository.Actualizar(libro))
            {
                TempData["Error"] = "No se actualizó el libro porque ya no existe.";
            }
            else
            {
                TempData["Mensaje"] = "Los datos del libro se actualizaron correctamente.";
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No se pudo actualizar el libro {Id}.", id);
            ViewBag.Error = "No se pudo actualizar el libro. Revise la conexión y los datos de la base de datos.";
            return View(libro);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(int id)
    {
        try
        {
            if (!_libroRepository.Eliminar(id))
            {
                TempData["Error"] = "No se eliminó el libro porque no existe.";
            }
            else
            {
                TempData["Mensaje"] = "El libro se eliminó correctamente.";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No se pudo eliminar el libro {Id}.", id);
            TempData["Error"] = "No se pudo eliminar el libro. Revise la conexión o si existen registros relacionados.";
        }

        return RedirectToAction(nameof(Index));
    }

    private IEnumerable<Libro> ListarLibros() => _libroRepository.Listar();

    private IEnumerable<Libro> ListarLibrosPorAutor(string autor) => _libroRepository.ListarPorAutor(autor);
}
