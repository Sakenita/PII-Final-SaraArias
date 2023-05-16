using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaOrg.Models;
using TiendaOrg.Models.ViewModels;

namespace TiendaOrg.Controllers
{

	public class ProductoController : Controller
	{
		private readonly TiendaOrgContext _context;

		public ProductoController(TiendaOrgContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var productos = _context.Productos
				.Include(b => b.IdVendedorNavigation)
				.Include(b => b.IdCategoriaNavigation);
			return View(await productos.ToListAsync());
		}

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        public IActionResult Create()
		{
			ViewData["Vendedor"] = new SelectList(_context.Vendedors, "IdVendedor", "NombreVendedor");
			ViewData["Categorias"] = new SelectList(_context.Categorias, "IdCategoria", "NombreCategoria");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductoViewModel model)
		{
			if (ModelState.IsValid)
			{
				var producto = new Producto()
				{
					NombreProducto = model.Nombre,
					Descripcion = model.Descripcion,
					Cantidad = Convert.ToInt32(model.Cantidad),
					Precio = Convert.ToInt32(model.Precio),
					IdVendedor = model.IdVendedor,
					IdCategoria = model.IdCategoria
				};
				_context.Add(producto);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["Vendedor"] = new SelectList(_context.Vendedors, "IdVendedor", "NombreVendedor", model.IdVendedor);
			ViewData["Categorias"] = new SelectList(_context.Categorias, "IdCategoria", "NombreCategoria", model.IdCategoria);
			return View();
		}

		//Edit
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Productos == null)
			{
				return NotFound();
			}

			var producto = await _context.Productos.FindAsync(id);
			if (producto == null)
			{
				return NotFound();
			}
			return View(producto);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("IdProducto,NombreProducto,Descripcion,Cantidad, Precio, IdVendedor, IdCategoria")] Producto producto)
		{
			if (id != producto.IdProducto)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				_context.Update(producto);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(producto);
		}

		// GET: Producto/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Productos == null)
			{
				return NotFound();
			}

			var producto = await _context.Productos
				.FirstOrDefaultAsync(m => m.IdProducto == id);
			if (producto == null)
			{
				return NotFound();
			}

			return View(producto);
		}

		// POST: Producto/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Productos == null)
			{
				return Problem("Entity set 'TiendaOrgContext.Producto'  is null.");
			}
			var producto = await _context.Productos.FindAsync(id);
			if (producto != null)
			{
				_context.Productos.Remove(producto);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ProductoExists(int id)
		{
			return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
		}
	}
}
