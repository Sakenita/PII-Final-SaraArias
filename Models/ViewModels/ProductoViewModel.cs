using System.ComponentModel.DataAnnotations;

namespace TiendaOrg.Models.ViewModels
{
	public class ProductoViewModel
	{
		[Required]
		[Display(Name = "Vendedor")]
		public int IdVendedor { get; set; }

		[Required]
		[Display(Name="Categoría")]
		public int IdCategoria { get; set; }

		[Required]
		[Display(Name = "Nombre")]
		public string Nombre { get; set; }

		[Display(Name = "Descripción")]
		public string Descripcion { get; set; }

		[Display(Name = "Cantidad")]
		public string Cantidad { get; set; }

		[Display(Name = "Precio")]
		public string Precio { get; set; }
	}
}
