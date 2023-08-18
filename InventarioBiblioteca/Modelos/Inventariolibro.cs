using System;
using System.Collections.Generic;

namespace InventarioBiblioteca.Modelos;

public partial class Inventariolibro
{
    public int Inventarioid { get; set; }

    public int Libroid { get; set; }

    public string Codigo { get; set; } = null!;

    public int Estadoid { get; set; }

    public int Autenticidadid { get; set; }

    public virtual Autenticidad Autenticidad { get; set; } = null!;

    public virtual Estadoconservacion Estado { get; set; } = null!;

    public virtual Libro Libro { get; set; } = null!;
}
