using Microsoft.Data.Sqlite;

namespace tl2_tp8_2025_nicoodiaz;

public class PresupuestosRepository : IPresupuestosRepository
{
    private string cadenaConexion = "Data Source = Db/Tienda.db"; //Path de la BD

    ProductoRepository _repositoryProducto = new ProductoRepository();

    public bool CrearPresupuesto(Presupuesto presupuesto)
    {
        string queryConsulta = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@NombreDestinatario, @FechaCreacion)"; //Creo la consulta
        using var conection = new SqliteConnection(cadenaConexion); //Creo conexion
        conection.Open(); //Conecto

        var command = new SqliteCommand(queryConsulta, conection); //Ejecuto consulta
        command.Parameters.Add(new SqliteParameter("@NombreDestinatario", presupuesto.NombreDestinatario)); //Le paso los parametros a la consulta
        command.Parameters.Add(new SqliteParameter("@FechaCreacion", presupuesto.FechaCreacion.ToString("yyyy-MM-dd")));
        bool resultado = command.ExecuteNonQuery() != 0; //Como no devuelve nada, ejecuto esto, si algo sale mal, tira excepcion
        if (resultado)
        {
            return true;
        }
        return false;
    }
    public List<Presupuesto> ObtenerTodosPresupuestos()
    {
        string queryConsulta = "SELECT * FROM Presupuestos"; //Creo la consulta que quiero realizar
        List<Presupuesto> presupuestos = new List<Presupuesto>(); //Creo la lista para guardar lo que trae la consulta

        using var conection = new SqliteConnection(cadenaConexion);//Creo la conexion mediante el path de la BD
        conection.Open(); //Me conecto

        var command = new SqliteCommand(queryConsulta, conection); //Ejecuto la consulta

        using (SqliteDataReader reader = command.ExecuteReader()) //Como es SELECT nos devuelve un DataReader para leerlo
        {
            while (reader.Read()) //Leo linea por linea
            {
                int idpresu = Convert.ToInt32(reader["idPresupuesto"]);
                var presupuesto = new Presupuesto //Creo nuevos objetos con los datos obtenidos de la BD
                {
                    IdPresupuesto = idpresu,
                    NombreDestinatario = reader["NombreDestinatario"].ToString(),
                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                    Detalles = ObtenerSoloDetalles(idpresu)
                };
                presupuestos.Add(presupuesto); //Los guardo en la lista
            }
        }
        conection.Close(); //Cierro 
        return presupuestos; //Devuelvo la lista
    }
    public List<PresupuestoDetalle> ObtenerSoloDetalles(int idPresupuesto)
    {
        using var conection = new SqliteConnection(cadenaConexion);
        conection.Open();

        string queryConsulta = "SELECT * FROM PresupuestosDetalle WHERE IdPresupuesto = @id";

        using var command = new SqliteCommand(queryConsulta, conection);

        command.Parameters.Add(new SqliteParameter("@id", idPresupuesto));

        List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();

        using (SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                detalles.Add(new PresupuestoDetalle
                {
                    Producto = _repositoryProducto.ObtenerProductoXId(Convert.ToInt32(reader["idProducto"])),
                    Cantidad = Convert.ToInt32(reader["Cantidad"])
                });
            }
        }
        return detalles;
    }
    public Presupuesto ObtenerDetallesPorId(int idPresupuesto)
    {
        string queryConsulta = "SELECT * FROM Presupuestos WHERE idPresupuesto = @id";

        Presupuesto presupuestoConsultado = new Presupuesto();

        using var conection = new SqliteConnection(cadenaConexion);
        conection.Open();

        using var command = new SqliteCommand(queryConsulta, conection);
        command.Parameters.Add(new SqliteParameter("@id", idPresupuesto));

        using (SqliteDataReader reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                presupuestoConsultado.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                presupuestoConsultado.NombreDestinatario = reader["NombreDestinatario"].ToString();
                presupuestoConsultado.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
            }
            else return null;
        }

        string queryConsulta2 = "SELECT * FROM PresupuestosDetalle WHERE IdPresupuesto = @id";

        using var command2 = new SqliteCommand(queryConsulta2, conection);

        command2.Parameters.Add(new SqliteParameter("@id", idPresupuesto));

        List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();

        using (SqliteDataReader reader = command2.ExecuteReader())
        {
            while (reader.Read())
            {
                detalles.Add(new PresupuestoDetalle
                {
                    Producto = _repositoryProducto.ObtenerProductoXId(Convert.ToInt32(reader["idProducto"])),
                    Cantidad = Convert.ToInt32(reader["Cantidad"])
                });
            }
        }
        presupuestoConsultado.Detalles = detalles;
        conection.Close();
        return presupuestoConsultado;
    }

    public void AgregarProducto(int idProduct, int idPresupuesto, int cantidad)
    {
        using var conection = new SqliteConnection(cadenaConexion);//Creo la conexion mediante el path de la BD
        conection.Open(); //Me conecto

        string queryConsulta = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto, @idProducto, @cantidad)";

        using SqliteCommand command = new SqliteCommand(queryConsulta, conection);
        command.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuesto));
        command.Parameters.Add(new SqliteParameter("@idProducto", idProduct));
        command.Parameters.Add(new SqliteParameter("@cantidad", cantidad));
        command.ExecuteNonQuery();

        conection.Close();
    }
    public void EliminarPresupuesto(int idPresupuesto)
    {
        using var conection = new SqliteConnection(cadenaConexion);//Creo la conexion mediante el path de la BD
        conection.Open(); //Me conecto

        string queryConsulta = "DELETE FROM Presupuestos WHERE IdPresupuesto = @id";

        var command = new SqliteCommand(queryConsulta, conection);
        command.Parameters.Add(new SqliteParameter("@id", idPresupuesto));

        string queryConsultaDetalles = "DELETE FROM PresupuestosDetalle WHERE IdPresupuesto = @id";
        var commandDetalle = new SqliteCommand(queryConsultaDetalles, conection);
        commandDetalle.Parameters.Add(new SqliteParameter("@id", idPresupuesto));
        
        commandDetalle.ExecuteNonQuery();
        command.ExecuteNonQuery();

        conection.Close();
    }
}