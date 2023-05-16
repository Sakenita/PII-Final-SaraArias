using System;
using System.Collections.Generic;

namespace TiendaOrg.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string TipoRol { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
