using System;
using System.Collections.Generic;

namespace InventarioBiblioteca.Modelos;

public partial class Libro
{
    public int Libroid { get; set; }

    public string Nombrelib { get; set; } = null!;

    public int Tipoid { get; set; }

    public int? Edicion { get; set; }

    public string? Año { get; set; }

    public string? Editorial { get; set; }

    public virtual ICollection<Inventariolibro> Inventariolibros { get; set; } = new List<Inventariolibro>();

    public virtual ICollection<Librosautore> Librosautores { get; set; } = new List<Librosautore>();

    public virtual Tipolibro Tipo { get; set; } = null!;
}
