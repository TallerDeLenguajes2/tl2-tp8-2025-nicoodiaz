namespace tl2_tp8_2025_nicoodiaz;

public interface IPresupuestosRepository
{
    public bool CrearPresupuesto(Presupuesto presupuesto);
    public List<Presupuesto> ObtenerTodosPresupuestos();
    public List<PresupuestoDetalle> ObtenerSoloDetalles(int idPresupuesto);
    public Presupuesto ObtenerDetallesPorId(int idPresupuesto);
    public void AgregarProducto(int idProduct, int idPresupuesto, int cantidad);
    public void EliminarPresupuesto(int idPresupuesto);
}