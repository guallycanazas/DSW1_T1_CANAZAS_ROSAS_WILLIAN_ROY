/*
    Evaluación DSW1 - T1
    Base de datos y procedimientos almacenados para el mantenimiento de Libros.
    La tabla conserva exactamente las columnas indicadas en el enunciado.
*/

IF DB_ID(N'BDBIBLIOTECA2026') IS NULL
BEGIN
    CREATE DATABASE [BDBIBLIOTECA2026];
END;
GO

USE [BDBIBLIOTECA2026];
GO

IF OBJECT_ID(N'dbo.Libros', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Libros
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        NombreLibro VARCHAR(120) NOT NULL,
        Autor VARCHAR(100) NOT NULL,
        NombreEditorial VARCHAR(100) NOT NULL,
        ISDN VARCHAR(20) NOT NULL
    );
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ListarLibros
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, NombreLibro, Autor, NombreEditorial, ISDN
    FROM dbo.Libros
    ORDER BY Id DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ObtenerLibroPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, NombreLibro, Autor, NombreEditorial, ISDN
    FROM dbo.Libros
    WHERE Id = @Id;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_RegistrarLibro
    @NombreLibro VARCHAR(120),
    @Autor VARCHAR(100),
    @NombreEditorial VARCHAR(100),
    @ISDN VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Libros (NombreLibro, Autor, NombreEditorial, ISDN)
    VALUES (@NombreLibro, @Autor, @NombreEditorial, @ISDN);

    SELECT CONVERT(INT, SCOPE_IDENTITY()) AS Id;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ActualizarLibro
    @Id INT,
    @NombreLibro VARCHAR(120),
    @Autor VARCHAR(100),
    @NombreEditorial VARCHAR(100),
    @ISDN VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Libros
    SET NombreLibro = @NombreLibro,
        Autor = @Autor,
        NombreEditorial = @NombreEditorial,
        ISDN = @ISDN
    WHERE Id = @Id;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_EliminarLibro
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Libros
    WHERE Id = @Id;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END;
GO

CREATE OR ALTER PROCEDURE dbo.usp_ListarLibrosPorAutor
    @Autor VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, NombreLibro, Autor, NombreEditorial, ISDN
    FROM dbo.Libros
    WHERE NULLIF(LTRIM(RTRIM(@Autor)), '') IS NULL
       OR Autor LIKE '%' + LTRIM(RTRIM(@Autor)) + '%'
    ORDER BY Id DESC;
END;
GO
