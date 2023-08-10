﻿using System;
using System.Collections.Generic;

namespace InventarioBiblioteca.Modelos;

public partial class Estadoconservacion
{
    public int Estadoid { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Valor { get; set; }

    public string Color { get; set; } = null!;

    public virtual ICollection<Inventariolibro> Inventariolibros { get; set; } = new List<Inventariolibro>();
}
