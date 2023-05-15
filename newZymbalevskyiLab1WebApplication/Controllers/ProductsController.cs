using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using newZymbalevskyiLab1WebApplication;

namespace newZymbalevskyiLab1WebApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DblibraryContext _context;

        public ProductsController(DblibraryContext context)
        {
            _context = context;
        }

        // GET: Products
        //return RedirectToAction("Index", "Products", new { id = category.Id, name = category.Name, description = category.Description});

        public async Task<IActionResult> Index(int? id, string? name, string? description)
        {
            /*var dblibraryContext = _context.Products.Include(p => p.Category).Include(p => p.Order).Include(p => p.Supplier);
            return View(await dblibraryContext.ToListAsync());*/
            if (id == null) return RedirectToAction("Index", "Categories");
            ViewBag.CategoryId = id;
            ViewBag.CategoryName = name;
            ViewBag.CategoryDescription = description;
            var productsByCategory = _context.Products.Where(p => p.CategoryId == id).Include(p => p.Category).Include(p => p.Order).Include(p => p.Supplier);
            return View(await productsByCategory.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Order)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create(int categoryId)
        {
            /*ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            return View();*/
            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryName = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Name;
            ViewBag.CategoryDescription = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Description;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int categoryId, [Bind("Id,Type,Name,Price,Description,Manufacturer,ImageLink,OrderId,CategoryId,SupplierId")] Product product)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", product.OrderId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", product.SupplierId);
            return View(product);*/
            product.CategoryId = categoryId;
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Products", new
                {
                    id = categoryId,
                    name = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Name,
                    description = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Description
                });
            }
            return RedirectToAction("Index", "Products", new
            {
                id = categoryId,
                name = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Name,
                description = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault().Description
            });
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", product.OrderId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", product.SupplierId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Name,Price,Description,Manufacturer,ImageLink,OrderId,CategoryId,SupplierId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", product.OrderId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", product.SupplierId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Order)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'DblibraryContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
