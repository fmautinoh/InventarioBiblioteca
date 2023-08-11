using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventarioBiblioteca.Modelos;

public partial class Libro
{
    public int Libroid { get; set; }

    public string Nombrelib { get; set; } = null!;

    public int Tipoid { get; set; }

    public int? Edicion { get; set; }

    public DateOnly Año { get; set; }

    public string? Editorial { get; set; }

    [JsonIgnore]
    public virtual ICollection<Inventariolibro> Inventariolibros { get; set; } = new List<Inventariolibro>();
    [JsonIgnore]

    public virtual ICollection<Librosautore> Librosautores { get; set; } = new List<Librosautore>();
    [JsonIgnore]
    public virtual Tipolibro Tipo { get; set; } = null!;
}
