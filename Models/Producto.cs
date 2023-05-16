using System;
using System.Collections.Generic;

namespace TiendaOrg.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Cantidad { get; set; }

    public double Precio { get; set; }

    public int IdVendedor { get; set; }

    public int IdCategoria { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Vendedor IdVendedorNavigation { get; set; } = null!;
}
