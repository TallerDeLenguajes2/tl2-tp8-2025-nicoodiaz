using System.ComponentModel.DataAnnotations;
using System.Runtime.Versioning;
using tl2_tp8_2025_nicoodiaz;

public class ProductoViewModel
{
    public ProductoViewModel(Producto p)
    {
        IdProducto = p.IdProducto;
        Descripcion = p.Descripcion ?? "";
        Precio = p.Precio;
    }
    public int IdProducto { get; set; }

    [Display(Name = "Descripcion")]
    [StringLength(250, ErrorMessage = "No debe pasar los 250 caracteres")]
    public string Descripcion { get; set; }

    [Display(Name = "Precio")]
    [Required(ErrorMessage = "El campo es obligatorio")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0")]
    public decimal Precio {get; set;}


}