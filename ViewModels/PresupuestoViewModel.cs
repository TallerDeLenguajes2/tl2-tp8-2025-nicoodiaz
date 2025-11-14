using System.ComponentModel.DataAnnotations;
using tl2_tp8_2025_nicoodiaz;

public class PresupuestoViewModel
{
    public PresupuestoViewModel(){}
      public PresupuestoViewModel(Presupuesto p)
        {
            if (p != null)
            {
            IdPresupuesto = p.IdPresupuesto;
            Monto = p.MontoPresupuesto();
            MontoIva = p.MontoPresupuestoConIva();
            NombreDestinatario = p.NombreDestinatario;
            FechaCreacion = p.FechaCreacion;
            Detalles = p.Detalles;
            }
        }

    [Display(Name = "Nombre del destinatario")]
    [Required(ErrorMessage = "El campo nombre es obligatorio")]
    public string NombreDestinatario { get; set; }

    [Display(Name = "Fecha de creacion")]
    [Required(ErrorMessage = "El campo fecha de creacion es obligatorio")]
    [DataType(DataType.Date)]
    public DateTime FechaCreacion { get; set; }
    public int IdPresupuesto { get; private set; }
    public decimal Monto { get; set; }
    public List<PresupuestoDetalle> Detalles { get; set; }
    public decimal MontoIva { get; set; }
}