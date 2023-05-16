using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaOrg.Models;

public partial class Vendedor
{
    public int IdVendedor { get; set; }

    public string NombreVendedor { get; set; } = null!;

    public int Telefono { get; set; }

    public string Logo { get; set; } = null!;

	[Required(ErrorMessage = "Por favor, agregue una imagen")]
    [Display(Name = "Imagen")]
    [NotMapped]
    public IFormFile FrontImage { get; set; } = null!;
	public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
