using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InventarioBiblioteca.Modelos;

public partial class Usuario
{
    public int Usuarioid { get; set; }

    public string Usu { get; set; } = null!;

    public string Pwsd { get; set; } = null!;

    public int Tipousuarioid { get; set; }
    [JsonIgnore]
    public virtual Tipousuario Tipousuario { get; set; } = null!;
}
