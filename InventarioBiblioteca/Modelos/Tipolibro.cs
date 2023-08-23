using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace InventarioBiblioteca.Modelos;

public partial class Tipolibro
{
    public int Tipolibroid { get; set; }

    public string Tipolibro1 { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
