using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaOrg.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace TiendaOrg.Controllers
{
    public class VendedorController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly TiendaOrgContext _context;

        public VendedorController(TiendaOrgContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            webHostEnvironment = webHost;
        }

        // GET: Vendedor
        public async Task<IActionResult> Index()
        {
              return _context.Vendedors != null ? 
                          View(await _context.Vendedors.ToListAsync()) :
                          Problem("Entity set 'TiendaOrgContext.Vendedors'  is null.");
        }

        // GET: Vendedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vendedors == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedors
                .FirstOrDefaultAsync(m => m.IdVendedor == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        private string UploadedFile(Vendedor vendedor)
        {
            string uniqueFileName = null;
            if (vendedor.Logo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + vendedor.Logo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vendedor.Logo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: Vendedor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendedor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVendedor,NombreVendedor,Telefono,Logo")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendedor);
        }

        // GET: Vendedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vendedors == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedors.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }
            return View(vendedor);
        }

        // POST: Vendedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVendedor,NombreVendedor,Telefono,Logo")] Vendedor vendedor)
        {
            if (id != vendedor.IdVendedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendedorExists(vendedor.IdVendedor))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vendedor);
        }

        // GET: Vendedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vendedors == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedors
                .FirstOrDefaultAsync(m => m.IdVendedor == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // POST: Vendedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vendedors == null)
            {
                return Problem("Entity set 'TiendaOrgContext.Vendedors'  is null.");
            }
            var vendedor = await _context.Vendedors.FindAsync(id);
            if (vendedor != null)
            {
                _context.Vendedors.Remove(vendedor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendedorExists(int id)
        {
          return (_context.Vendedors?.Any(e => e.IdVendedor == id)).GetValueOrDefault();
        }
    }
}
