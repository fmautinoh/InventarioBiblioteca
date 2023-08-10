using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventarioBiblioteca.Modelos;

public partial class Autore
{
    public int Autorid { get; set; }

    public string Nombreautor { get; set; } = null!;

    public int Tipoautorid { get; set; }

    [JsonIgnore]
    public virtual ICollection<Librosautore> Librosautores { get; set; } = new List<Librosautore>();
    [JsonIgnore]
    public virtual Tipoautor Tipoautor { get; set; } = null!;
}
