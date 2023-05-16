using System;
using System.Collections.Generic;

namespace TiendaOrg.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
