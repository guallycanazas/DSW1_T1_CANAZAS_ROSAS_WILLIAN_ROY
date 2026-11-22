using System.Data;
using DSW1_T1_CANAZAS_ROSAS_WILLIAN_ROY.Models;
using Microsoft.Data.SqlClient;

namespace DSW1_T1_CANAZAS_ROSAS_WILLIAN_ROY.Data;

public sealed class LibroRepository
{
    private readonly string _connectionString;

    public LibroRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("No se configuró la cadena DefaultConnection.");
    }

    public IEnumerable<Libro> Listar()
    {
        var libros = new List<Libro>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("dbo.usp_ListarLibros", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            libros.Add(LeerLibro(reader));
        }

        return libros;
    }

    public IEnumerable<Libro> ListarPorAutor(string autor)
    {
        var libros = new List<Libro>();
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("dbo.usp_ListarLibrosPorAutor", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.Add("@Autor", SqlDbType.VarChar, 100).Value = autor.Trim();

        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            libros.Add(LeerLibro(reader));
        }

        return libros;
    }

    public Libro? ObtenerPorId(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("dbo.usp_ObtenerLibroPorId", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? LeerLibro(reader) : null;
    }

    public int Registrar(Libro libro)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("dbo.usp_RegistrarLibro", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        AgregarDatosLibro(command, libro);

        connection.Open();
        return Convert.ToInt32(command.ExecuteScalar());
    }

    public bool Actualizar(Libro libro)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("dbo.usp_ActualizarLibro", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.Add("@Id", SqlDbType.Int).Value = libro.Id;
        AgregarDatosLibro(command, libro);

        connection.Open();
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public bool Eliminar(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("dbo.usp_EliminarLibro", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        connection.Open();
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    private static void AgregarDatosLibro(SqlCommand command, Libro libro)
    {
        command.Parameters.Add("@NombreLibro", SqlDbType.VarChar, 120).Value = libro.NombreLibro.Trim();
        command.Parameters.Add("@Autor", SqlDbType.VarChar, 100).Value = libro.Autor.Trim();
        command.Parameters.Add("@NombreEditorial", SqlDbType.VarChar, 100).Value = libro.NombreEditorial.Trim();
        command.Parameters.Add("@ISDN", SqlDbType.VarChar, 20).Value = libro.ISDN.Trim();
    }

    private static Libro LeerLibro(SqlDataReader reader) => new()
    {
        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        NombreLibro = reader.GetString(reader.GetOrdinal("NombreLibro")),
        Autor = reader.GetString(reader.GetOrdinal("Autor")),
        NombreEditorial = reader.GetString(reader.GetOrdinal("NombreEditorial")),
        ISDN = reader.GetString(reader.GetOrdinal("ISDN"))
    };
}
