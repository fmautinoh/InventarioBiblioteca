using System;
using System.Collections.Generic;

namespace InventarioBiblioteca.Modelos;

public partial class Tipousuario
{
    public int Tipousuarioid { get; set; }

    public string Tipousuario1 { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
