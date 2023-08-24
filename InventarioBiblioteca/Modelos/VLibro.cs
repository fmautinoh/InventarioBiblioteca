using InventarioBiblioteca.Modelos.ModelDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace InventarioBiblioteca.Modelos;

public partial class VLibro
{
    public int? Libroid { get; set; }
    [JsonIgnore]
    public int? Autorid { get; set; }

    public int? Tipolibroid { get; set; }

    public string? Nombrelib { get; set; }

    public string? Tipolibro { get; set; }

    [JsonIgnore]
    public string? Nombreautor { get; set; }

    public int? Edicion { get; set; }

    public int? Año { get; set; }

    public string? Editorial { get; set; }
    [NotMapped] // Exclude this property from database mapping
    public List<AutorDtosList> AutoresIds { get; set; }
}
