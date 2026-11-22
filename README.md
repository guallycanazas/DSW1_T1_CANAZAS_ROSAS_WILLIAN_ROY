# Evaluación de Laboratorio T1

## Desarrollo de Servicios Web I

**Estudiante:** Willian Roy Canazas Rosas · **Tecnologías:** ASP.NET Core MVC, .NET 9, SQL Server y ADO.NET

### Presentación

Profesor, en este proyecto desarrollé una aplicación web para mantener el catálogo de libros de la base de datos `BDBIBLIOTECA2026`. La aplicación permite registrar, listar, editar y eliminar libros, además de buscar por coincidencias parciales del nombre del autor.

## Funciones implementadas

- CRUD de libros con validación de los campos obligatorios.
- Búsqueda por autor mediante `LIKE`.
- Confirmación antes de eliminar un registro.
- Mensajes para indicar el resultado de las operaciones.
- Acceso a SQL Server con `Microsoft.Data.SqlClient` y procedimientos almacenados.

## Cómo ejecutar el proyecto

### 1. Preparar la base de datos

Ejecuta [`SQL/01_BaseDatosYProcedimientos.sql`](SQL/01_BaseDatosYProcedimientos.sql) en SQL Server Management Studio. El script crea la base `BDBIBLIOTECA2026`, la tabla `dbo.Libros` y los procedimientos almacenados utilizados por la aplicación.

### 2. Revisar la conexión

En `appsettings.json` dejé configurada una conexión para LocalDB de Windows:

```text
Server=(localdb)\MSSQLLocalDB;Database=BDBIBLIOTECA2026;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

Si la instancia es SQL Server Express, cambia el servidor por `Server=.\SQLEXPRESS`. Si utilizas autenticación de SQL Server, actualiza la cadena con los datos de tu instancia.

### 3. Iniciar la aplicación

Abre una terminal en la carpeta del proyecto y ejecuta:

```bash
dotnet restore
dotnet run
```

Luego ingresa en el navegador a la dirección local que aparece en la terminal.

## Procedimientos almacenados

| Procedimiento | Operación |
| --- | --- |
| `usp_ListarLibros` | Muestra el catálogo completo. |
| `usp_ObtenerLibroPorId` | Busca un libro para cargar el formulario de edición. |
| `usp_RegistrarLibro` | Registra un libro nuevo. |
| `usp_ActualizarLibro` | Guarda los cambios de un libro. |
| `usp_EliminarLibro` | Elimina un libro según su identificador. |
| `usp_ListarLibrosPorAutor` | Consulta autores con coincidencia parcial. |

## Organización del proyecto

- `Controllers`: acciones para listar, buscar, registrar, editar y eliminar.
- `Data`: conexión ADO.NET y ejecución de procedimientos almacenados.
- `Models`: modelo `Libro` y reglas de validación.
- `SQL`: script de creación de la base de datos y procedimientos.
- `Views/Libro`: páginas del listado y los formularios.

## Observación

El enunciado menciona una categoría, pero el esquema proporcionado para `dbo.Libros` no define una columna, tabla ni relación de categorías. Por ese motivo, mantuve los campos indicados en el script: `Id`, `NombreLibro`, `Autor`, `NombreEditorial` e `ISDN`.
