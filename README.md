<div align="center">

# 📚 Biblioteca Cibertec

### Evaluación de Laboratorio T1 · Desarrollo de Servicios Web I

Aplicación web para administrar libros y consultar el catálogo por autor.

**ASP.NET Core MVC · .NET 9 · SQL Server · ADO.NET**

</div>

---

## ¿Qué incluye?

- Registro, listado, edición y eliminación de libros.
- Búsqueda por coincidencia parcial del nombre del autor.
- Validación de los cuatro campos requeridos.
- Confirmación antes de eliminar y mensajes de resultado.
- Acceso a datos con `Microsoft.Data.SqlClient` y procedimientos almacenados.

## Puesta en marcha

### 1. Crear la base de datos

En SQL Server Management Studio, ejecuta [`SQL/01_BaseDatosYProcedimientos.sql`](SQL/01_BaseDatosYProcedimientos.sql). El script crea `BDBIBLIOTECA2026` si aún no existe, define `dbo.Libros` y crea todos los procedimientos que utiliza la aplicación.

### 2. Configurar SQL Server

La cadena inicial de `appsettings.json` utiliza LocalDB de Windows:

```text
Server=(localdb)\MSSQLLocalDB;Database=BDBIBLIOTECA2026;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

Si usas SQL Server Express, cambia el servidor a `Server=.\SQLEXPRESS`. Para autenticación de SQL Server, configura el usuario y la contraseña de tu instancia en `DefaultConnection`.

### 3. Ejecutar el sitio

Desde esta carpeta, ejecuta:

```bash
dotnet restore
dotnet run
```

Abre en el navegador la dirección local que muestra la terminal. La pantalla inicial lleva al catálogo de libros.

## Procedimientos almacenados

| Procedimiento | Uso |
| --- | --- |
| `usp_ListarLibros` | Lista todos los libros. |
| `usp_ObtenerLibroPorId` | Recupera un libro para editarlo. |
| `usp_RegistrarLibro` | Inserta un libro y devuelve su código. |
| `usp_ActualizarLibro` | Actualiza los datos de un libro. |
| `usp_EliminarLibro` | Elimina el libro indicado por su código. |
| `usp_ListarLibrosPorAutor` | Busca autores con `LIKE` y coincidencia parcial. |

## Estructura

```text
Controllers/    Acciones MVC para el catálogo y los formularios
Data/           Consultas ADO.NET a los procedimientos almacenados
Models/         Modelo Libro y validaciones
SQL/            Script de base de datos y procedimientos
Views/Libro/    Listado, registro y edición
wwwroot/        Estilos y recursos del sitio
```

## Sobre el esquema entregado

El enunciado menciona una categoría, pero la definición de `dbo.Libros` solo incluye `Id`, `NombreLibro`, `Autor`, `NombreEditorial` e `ISDN`; no proporciona tabla ni relación de categorías. La aplicación sigue esa estructura para que coincida con el script entregado.

---

**Autor:** Willian Roy Canazas Rosas  
**Curso:** Desarrollo de Servicios Web I
