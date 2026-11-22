# Sistema de Gestión de Libros

Aplicación web para administrar el catálogo de libros de una biblioteca. Permite mantener los datos de cada libro y encontrar registros por nombre de autor.

## Funcionalidades

- Registrar, listar, editar y eliminar libros.
- Buscar autores por coincidencia parcial.
- Validar los campos obligatorios antes de guardar.
- Confirmar la eliminación y mostrar mensajes de resultado.

## Tecnologías

- ASP.NET Core MVC con .NET 9
- SQL Server
- ADO.NET con `Microsoft.Data.SqlClient`
- Procedimientos almacenados

## Configuración

1. Ejecuta [`SQL/01_BaseDatosYProcedimientos.sql`](SQL/01_BaseDatosYProcedimientos.sql) en SQL Server Management Studio para preparar la base de datos `BDBIBLIOTECA2026`.
2. Revisa `DefaultConnection` en `appsettings.json` y ajústala al nombre de tu instancia.

La cadena incluida está preparada para LocalDB en Windows:

```text
Server=(localdb)\MSSQLLocalDB;Database=BDBIBLIOTECA2026;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

Para SQL Server Express puedes usar `Server=.\SQLEXPRESS`. Si tu instancia usa autenticación de SQL Server, configura ahí las credenciales correspondientes.

## Ejecución

Desde la carpeta del proyecto, ejecuta:

```bash
dotnet restore
dotnet run
```

Abre en el navegador la dirección local que muestra la terminal.

## Procedimientos almacenados

| Procedimiento | Función |
| --- | --- |
| `usp_ListarLibros` | Lista los libros registrados. |
| `usp_ObtenerLibroPorId` | Obtiene un libro para editarlo. |
| `usp_RegistrarLibro` | Registra un libro. |
| `usp_ActualizarLibro` | Actualiza los datos de un libro. |
| `usp_EliminarLibro` | Elimina un libro por su identificador. |
| `usp_ListarLibrosPorAutor` | Busca libros por autor usando coincidencias parciales. |

## Organización

- `Controllers`: acciones para las páginas y operaciones del catálogo.
- `Data`: conexión a SQL Server y ejecución de procedimientos.
- `Models`: modelo `Libro` y validaciones.
- `SQL`: script de la base de datos y procedimientos almacenados.
- `Views/Libro`: listado y formularios.

## Autor

Willian Roy Canazas Rosas
