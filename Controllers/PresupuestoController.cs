using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace tl2_tp8_2025_nicoodiaz;

public class PresupuestoController : Controller
{
    private readonly PresupuestosRepository _presupuestoRepository;

    private readonly ProductoRepository _productoRepository;

    public PresupuestoController()
    {
        _presupuestoRepository = new PresupuestosRepository();
        _productoRepository = new ProductoRepository();
    }

            //ACCIONES PARA PODER LISTAR
    [HttpGet]
    public IActionResult Index()
    {
        List<Presupuesto> presupuestos = _presupuestoRepository.ObtenerTodosPresupuestos();
        return View(presupuestos);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        Presupuesto presupuesto = _presupuestoRepository.ObtenerDetallesPorId(id);
        return presupuesto != null ? View(presupuesto) : NotFound();
    }

    //ACCIONES PARA CREAR
    [HttpGet]
    public IActionResult Create()
    {
        var vm = new PresupuestoViewModel
        {
            FechaCreacion = DateTime.Now
        };
        return View(vm);
    }

    [HttpPost]
    public IActionResult Create(PresupuestoViewModel nuevoPresupuesto)
    {
        if(!ModelState.IsValid) return View(nuevoPresupuesto);
        var presu = new Presupuesto();

        presu.Detalles = nuevoPresupuesto.Detalles;
        presu.FechaCreacion = nuevoPresupuesto.FechaCreacion;
        presu.IdPresupuesto = nuevoPresupuesto.IdPresupuesto;
        presu.NombreDestinatario = nuevoPresupuesto.NombreDestinatario;

        _presupuestoRepository.CrearPresupuesto(presu);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id) => View(new PresupuestoViewModel(_presupuestoRepository.ObtenerDetallesPorId(id)));

    [HttpPost]
    public IActionResult Edit(int id, PresupuestoViewModel nuevoPresupuesto)
    {
        var presu = new Presupuesto
        {
            IdPresupuesto = nuevoPresupuesto.IdPresupuesto,
            FechaCreacion = nuevoPresupuesto.FechaCreacion,
            NombreDestinatario = nuevoPresupuesto.NombreDestinatario,
            Detalles = nuevoPresupuesto.Detalles
        };
        _presupuestoRepository.ModificarPresupuesto(id, presu);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Presupuesto presu = _presupuestoRepository.ObtenerDetallesPorId(id);
        return View(presu);
    }

    [HttpPost]
    public IActionResult Delete(Presupuesto presu)
    {
        _presupuestoRepository.EliminarPresupuesto(presu.IdPresupuesto);
        return RedirectToAction("Index");
    }

    public IActionResult AgregarProducto(int id)
    {
        //Obtener productos
        List<Producto> productos = _productoRepository.ObtenerTodosProductos();

        //Crear ViewModel
        AgregarProductoViewModel model = new AgregarProductoViewModel
        {
            IdPresupuesto = id,
            //Crear el SelectList
            ListaProductos = new SelectList(productos, "IdProducto", "Descripcion")
        };
        return View(model);
    }
    [HttpPost]
    public IActionResult AgregarProducto(AgregarProductoViewModel model)
    {
        if(!ModelState.IsValid)
        {
            //Logica critica de recarga. si falla la validacion debo recargar el SelecList porque se pierde en el post
            var productos = _productoRepository.ObtenerTodosProductos();
            model.ListaProductos = new SelectList(productos, "IdProducto", "Descripcion");

            return View(model);
        }
        //Si no entra al if, es porque es valido llamo al repo para guardar la relacion
        _presupuestoRepository.AgregarProducto(model.IdProducto, model.IdPresupuesto, model.Cantidad);
        return RedirectToAction(nameof(Details), new {id = model.IdPresupuesto});
    }
}