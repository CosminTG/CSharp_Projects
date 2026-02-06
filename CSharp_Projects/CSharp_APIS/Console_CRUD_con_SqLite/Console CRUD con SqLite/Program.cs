using Microsoft.Data.Sqlite;

class Algarrobo
{
    static string conexion_db = "Data Source = libros.db";
    static void Main()
    {
        string opciones_menu;
        do
        {
            Console.WriteLine("1. Crear un archivo");
            Console.WriteLine("2. Ver los registros");
            Console.WriteLine("3. Actualizar un registro");
            Console.WriteLine("4. Eliminar un registro");
            Console.WriteLine("5. Salir del Sistema");
            Console.Write("Opciones: ");
            opciones_menu = Console.ReadLine() ?? "";
            switch (opciones_menu)
            {
                case "1": CreateRegistro(); break;
                case "2": ReadRegistro(); break;
                case "3": UpdateRegistro(); break;
                case "4": DeleteRegistro(); break;
            }
        } while (opciones_menu != "5");
    }
    static void CreateRegistro()
    {
        Console.Write("Nombre autor: ");
        string nombre_autor_createregistro = Console.ReadLine() ?? "";
        Console.Write("Nombre libro: ");
        string nombre_libro_createregistro = Console.ReadLine() ?? "";

        using var conexion_tabla_db_createregistro = new SqliteConnection(conexion_db);
        conexion_tabla_db_createregistro.Open();

        using var cmd_conexion_tabla_db_createregistro = conexion_tabla_db_createregistro.CreateCommand();
        cmd_conexion_tabla_db_createregistro.CommandText = @"INSERT INTO 
            autor(nombre_autor,nombre_libro)
            values($nombre_autor_createregistro,$nombre_libro_createregistro);";
        cmd_conexion_tabla_db_createregistro.Parameters.AddWithValue("$nombre_autor_createregistro", nombre_autor_createregistro);
        cmd_conexion_tabla_db_createregistro.Parameters.AddWithValue("$nombre_libro_createregistro", nombre_libro_createregistro);
        cmd_conexion_tabla_db_createregistro.ExecuteNonQuery();
    }
    static void ReadRegistro()
    {
        using var conexion_tabla_db_readregistro = new SqliteConnection(conexion_db);
        conexion_tabla_db_readregistro.Open();

        using var cmd_conexion_tabla_db_readregistro = conexion_tabla_db_readregistro.CreateCommand();
        cmd_conexion_tabla_db_readregistro.CommandText = @"select nombre_autor,nombre_libro from autor";

        using var listar_datos = cmd_conexion_tabla_db_readregistro.ExecuteReader();
        while (listar_datos.Read())
        {
            string nombre_autor_read = listar_datos.GetString(0);
            string nombre_libro_read = listar_datos.GetString(1);
            Console.WriteLine($"Este es el nombre del sutor: {nombre_autor_read} y su libro es: {nombre_libro_read}");
        }
    }
    static void UpdateRegistro()
    {
        Console.Write("Introduzca ID: ");
        int id_updteregistro = int.Parse(Console.ReadLine() ?? "");

        Console.Write("Nuevo Nombre autor: ");
        string nuevo_nombre_autor_updateregistro = Console.ReadLine() ?? "";

        Console.Write("Nuevo Nombre libro: ");
        string nuevo_nombre_libro_updateregistro = Console.ReadLine() ?? "";

        using var conexion_tabla_db = new SqliteConnection(conexion_db);
        conexion_tabla_db.Open();

        using var cmd_conexion_tabla_db = conexion_tabla_db.CreateCommand();
        cmd_conexion_tabla_db.CommandText = @"update autor set nombre_autor=$nuevo_nombre_autor_updateregistro,
        nombre_libro=$nuevo_nombre_libro_updateregistro 
        where id=$id_updteregistro";
        cmd_conexion_tabla_db.Parameters.AddWithValue("$nuevo_nombre_autor_updateregistro", nuevo_nombre_autor_updateregistro);
        cmd_conexion_tabla_db.Parameters.AddWithValue("$nuevo_nombre_libro_updateregistro", nuevo_nombre_libro_updateregistro);
        cmd_conexion_tabla_db.Parameters.AddWithValue("$id_updteregistro", id_updteregistro);
        cmd_conexion_tabla_db.ExecuteNonQuery();
    }
    static void DeleteRegistro()
    {
        Console.Write("Introduzca el ID a Eliminar: ");
        int id_deleteregistro = int.Parse(Console.ReadLine() ?? "");

        using var conexion_tabla_db = new SqliteConnection(conexion_db);
        conexion_tabla_db.Open();

        using var cmd_conexion_tabla_db = conexion_tabla_db.CreateCommand();
        cmd_conexion_tabla_db.CommandText = @"delete from autor where id=$id_deleteregistro";
        cmd_conexion_tabla_db.Parameters.AddWithValue("$id_deleteregistro", id_deleteregistro);
        cmd_conexion_tabla_db.ExecuteNonQuery();
    }
}