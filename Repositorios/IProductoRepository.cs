namespace tl2_tp8_2025_nicoodiaz;

public interface IProductoRepository
{
    public void CrearProducto(Producto producto);
    public void ActualizarProducto(int idProduct, Producto productoActualizar);
    public List<Producto> ObtenerTodosProductos();
    public Producto ObtenerProductoXId(int idProduct);
    public Producto EliminarProducto(int idProduct);
}