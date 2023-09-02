using System;
using System.Collections.Generic;

namespace InventarioBiblioteca.Modelos;

public partial class VInvReporte
{
    public string? Codigo { get; set; }

    public string? Nombrelib { get; set; }

    public string? Autores { get; set; }

    public int? Edicion { get; set; }

    public string? Editorial { get; set; }

    public int? Año { get; set; }

    public string? Descripcion { get; set; }

    public string? Autenticidad { get; set; }

    public int? Valor { get; set; }
}
