using Microsoft.Data.Sqlite;

namespace tl2_tp8_2025_nicoodiaz;

public class ProductoRepository : IProductoRepository
{
    private string cadenaConexion = "Data Source = Db/Tienda.db"; //Path de la BD

    public void CrearProducto(Producto producto)
    {
        string queryConsulta = "INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)"; //Creo la consulta
        using SqliteConnection conection = new SqliteConnection(cadenaConexion); //Creo conexion
        conection.Open(); //Conecto

        SqliteCommand command = new SqliteCommand(queryConsulta, conection); //Ejecuto consulta
        command.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion)); //Le paso los parametros a la consulta
        command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
        command.ExecuteNonQuery(); //Como no devuelve nada, ejecuto esto, si algo sale mal, tira excepcion
        conection.Close();//Desconecto
    }
    public void ActualizarProducto(int idProduct, Producto productoActualizar)
    {
        string queryConsulta = "UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @id";
        using var conection = new SqliteConnection(cadenaConexion);
        conection.Open();

        var command = new SqliteCommand(queryConsulta, conection);
        command.Parameters.Add(new SqliteParameter("@Descripcion", productoActualizar.Descripcion));
        command.Parameters.Add(new SqliteParameter("@Precio", productoActualizar.Precio));
        command.Parameters.Add(new SqliteParameter("@id", idProduct));
        command.ExecuteNonQuery();
        conection.Close();
    }

    public List<Producto> ObtenerTodosProductos()
    {
        string queryConsulta = "SELECT * FROM productos"; //Creo la consulta que quiero realizar
        List<Producto> productos = new List<Producto>(); //Creo la lista para guardar lo que trae la consulta

        using var conection = new SqliteConnection(cadenaConexion);//Creo la conexion mediante el path de la BD
        conection.Open(); //Me conecto

        var command = new SqliteCommand(queryConsulta, conection); //Ejecuto la consulta

        using (SqliteDataReader reader = command.ExecuteReader()) //Como es SELECT nos devuelve un DataReader para leerlo
        {
            while (reader.Read()) //Leo linea por linea
            {
                var producto = new Producto //Creo nuevos objetos con los datos obtenidos de la BD
                {
                    IdProducto = Convert.ToInt32(reader["idProducto"]),
                    Descripcion = reader["Descripcion"].ToString(),
                    Precio = Convert.ToInt32(reader["Precio"])
                };
                productos.Add(producto); //Los guardo en la lista
            }
        }
        conection.Close(); //Cierro 
        return productos; //Devuelvo la lista
    }
    public Producto ObtenerProductoXId(int idProduct)
    {
        string queryConsulta = "SELECT * FROM Productos WHERE IdProducto = @id";
        Producto productoConsultado = new Producto();

        using var conection = new SqliteConnection(cadenaConexion);
        conection.Open();

        using var command = new SqliteCommand(queryConsulta, conection);
        command.Parameters.Add(new SqliteParameter("@id", idProduct));

        using (SqliteDataReader reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                productoConsultado.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                productoConsultado.Descripcion = reader["Descripcion"].ToString();
                productoConsultado.Precio = Convert.ToInt32(reader["Precio"]);
            }
        }
        conection.Close();
        return productoConsultado;
    }

    public Producto EliminarProducto(int idProduct)
    {
        string queryConsulta = "DELETE FROM Productos WHERE IdProducto = @id";

        using var conection = new SqliteConnection(cadenaConexion);
        conection.Open();

        using var command = new SqliteCommand(queryConsulta, conection);
        command.Parameters.Add(new SqliteParameter("@id", idProduct));
        Producto prodEliminado = ObtenerProductoXId(idProduct); //Por si quiero devolver el producto que elimine

        int eliminado = command.ExecuteNonQuery();
        conection.Close();
        return eliminado == 0 ? null : prodEliminado;
    }
}