using System;
using System.Collections.Generic;

namespace InventarioBiblioteca.Modelos;

public partial class Tipolibro
{
    public int Tipolibroid { get; set; }

    public string Tipolibro1 { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
