using System;
using System.Collections.Generic;

namespace InventarioBiblioteca.Modelos;

public partial class Tipoautor
{
    public int Tipoautorid { get; set; }

    public string Tipoautor1 { get; set; } = null!;

    public virtual ICollection<Autore> Autores { get; set; } = new List<Autore>();
}
