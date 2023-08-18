using System;
using System.Collections.Generic;

namespace InventarioBiblioteca.Modelos;

public partial class Autenticidad
{
    public int Autenticidadid { get; set; }

    public string Autenticidad1 { get; set; } = null!;

    public virtual ICollection<Inventariolibro> Inventariolibros { get; set; } = new List<Inventariolibro>();
}
