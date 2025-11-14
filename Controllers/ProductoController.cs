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
    public IActionResult Create(ProductoViewModel nuevoProductoVM)
    {
        if(!ModelState.IsValid) return View(nuevoProductoVM);
        var nuevoProducto = new Producto
        {
            Descripcion = nuevoProductoVM.Descripcion,
            Precio = nuevoProductoVM.Precio
        };
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
        return View(new ProductoViewModel(_productoRepository.ObtenerProductoXId(id)));
    }

    [HttpPost]
    public IActionResult Edit(int id, ProductoViewModel prodVM)
    {
        if (id != prodVM.IdProducto) return NotFound();
        if(!ModelState.IsValid) return View(prodVM);
        var prodcto = new Producto
        {
            IdProducto = prodVM.IdProducto,
            Descripcion = prodVM.Descripcion,
            Precio = prodVM.Precio
        };

        _productoRepository.ActualizarProducto(id, prodcto);
        return RedirectToAction("Index");
    }
} 