using Microsoft.AspNetCore.Mvc;

namespace tl2_tp8_2025_nicoodiaz;

public class PresupuestoController : Controller
{
    private PresupuestosRepository _presupuestoRepository;

    public PresupuestoController()
    {
        _presupuestoRepository = new PresupuestosRepository();
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
    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Presupuesto nuevoPresupuesto)
    {
        _presupuestoRepository.CrearPresupuesto(nuevoPresupuesto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id) => View(_presupuestoRepository.ObtenerDetallesPorId(id));

    [HttpPost]
    public IActionResult Edit(int id, Presupuesto nuevoPresupuesto)
    {
        _presupuestoRepository.ModificarPresupuesto(id, nuevoPresupuesto);
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
}