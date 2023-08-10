using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventarioBiblioteca.Modelos;

public partial class Librosautore
{
    public int Libroautorid { get; set; }

    public int Libroid { get; set; }

    public int Autorid { get; set; }

    [JsonIgnore]
    public virtual Autore Autor { get; set; } = null!;
    [JsonIgnore]
    public virtual Libro Libro { get; set; } = null!;
}
