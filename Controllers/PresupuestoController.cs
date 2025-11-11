using Microsoft.AspNetCore.Mvc;

namespace tl2_tp8_2025_nicoodiaz;

public class PresupuestoController : Controller
{
    private PresupuestosRepository _presupuestoRepository;

    public PresupuestoController()
    {
        _presupuestoRepository = new PresupuestosRepository();
    }

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
}