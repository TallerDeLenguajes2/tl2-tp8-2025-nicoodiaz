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
}