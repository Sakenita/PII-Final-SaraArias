using System;
using System.Collections.Generic;

namespace TiendaOrg.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string CorreoE { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int IdRol { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;
}
