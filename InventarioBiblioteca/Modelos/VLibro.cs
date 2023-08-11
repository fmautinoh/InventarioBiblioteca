using InventarioBiblioteca.Modelos.ModelDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventarioBiblioteca.Modelos;

public partial class VLibro
{
    public int? Libroid { get; set; }

    public int? Autorid { get; set; }

    public int? Tipolibroid { get; set; }

    public string? Nombrelib { get; set; }

    public string? Tipolibro { get; set; }

    public string? Nombreautor { get; set; }

    public int? Edicion { get; set; }

    public DateOnly? Año { get; set; }

    public string? Editorial { get; set; }
    [NotMapped] // Exclude this property from database mapping
    public List<AutorDtosList> AutoresIds { get; set; }
}
