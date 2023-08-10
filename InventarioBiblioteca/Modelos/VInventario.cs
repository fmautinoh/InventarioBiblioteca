using System;
using System.Collections.Generic;

namespace InventarioBiblioteca.Modelos;

public partial class VInventario
{
    public int? Libroid { get; set; }

    public int? Inventarioid { get; set; }

    public int? Estadoid { get; set; }

    public string? Codigo { get; set; }

    public string? Descripcion { get; set; }

    public int? Valor { get; set; }

    public string? Color { get; set; }
}
