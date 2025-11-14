using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;

namespace tl2_tp8_2025_nicoodiaz;

public class ProductoController : Controller
{
    private ProductoRepository _productoRepository;

    public ProductoController()
    {
        _productoRepository = new ProductoRepository();
    }

    [HttpGet("ListarProductos")]
    public IActionResult Index()
    {
        List<Producto> productos = _productoRepository.ObtenerTodosProductos();
        return View(productos);
    }
    [HttpGet]
    public IActionResult Details(int id)
    {
        Producto producto = _productoRepository.ObtenerProductoXId(id);
        return producto != null ? View(producto) : NotFound();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Producto nuevoProducto)
    {
        _productoRepository.CrearProducto(nuevoProducto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        return View(_productoRepository.ObtenerProductoXId(id));
    }

    [HttpPost]
    public IActionResult Delete(Producto prodAElim)
    {
        _productoRepository.EliminarProducto(prodAElim.IdProducto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var producto = _productoRepository.ObtenerProductoXId(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult Edit(Producto prod)
    {
        _productoRepository.ActualizarProducto(prod.IdProducto, prod);
        return RedirectToAction("Index");
    }
} 