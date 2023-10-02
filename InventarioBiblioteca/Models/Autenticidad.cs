using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventarioBiblioteca.Models;

public partial class Autenticidad
{
    public int AutenticidadId { get; set; }

    public string autenticidad { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<InventarioLibro> InventarioLibros { get; set; } = new List<InventarioLibro>();
}
