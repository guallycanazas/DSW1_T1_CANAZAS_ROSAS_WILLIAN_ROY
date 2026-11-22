using System.ComponentModel.DataAnnotations;

namespace DSW1_T1_CANAZAS_ROSAS_WILLIAN_ROY.Models;

public sealed class Libro
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ingrese el nombre del libro.")]
    [StringLength(120, ErrorMessage = "El nombre no puede superar los 120 caracteres.")]
    [Display(Name = "Nombre del libro")]
    public string NombreLibro { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese el autor.")]
    [StringLength(100, ErrorMessage = "El autor no puede superar los 100 caracteres.")]
    [Display(Name = "Autor")]
    public string Autor { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese la editorial.")]
    [StringLength(100, ErrorMessage = "La editorial no puede superar los 100 caracteres.")]
    [Display(Name = "Editorial")]
    public string NombreEditorial { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese el ISDN.")]
    [StringLength(20, ErrorMessage = "El ISDN no puede superar los 20 caracteres.")]
    [Display(Name = "ISDN")]
    public string ISDN { get; set; } = string.Empty;
}
