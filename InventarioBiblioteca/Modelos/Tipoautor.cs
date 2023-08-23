using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventarioBiblioteca.Modelos;

public partial class Tipoautor
{
    public int Tipoautorid { get; set; }

    public string tipoautor { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Autore> Autores { get; set; } = new List<Autore>();
}
