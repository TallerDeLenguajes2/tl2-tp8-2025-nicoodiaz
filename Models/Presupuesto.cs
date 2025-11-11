namespace tl2_tp8_2025_nicoodiaz;

public class Presupuesto
{
    private const decimal IVA = 0.21m;
    public int IdPresupuesto { get; set; }
    public string NombreDestinatario { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<PresupuestoDetalle> Detalles { get; set; }

    public decimal MontoPresupuesto() => Detalles.Sum(d => d.Producto.Precio * d.Cantidad);
    public decimal MontoPresupuestoConIva() => MontoPresupuesto() * (1m + IVA);
    public int CantidadProductos() => Detalles.Sum(d => d.Cantidad);
}