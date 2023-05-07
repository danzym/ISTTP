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
    public class OrdersController : Controller
    {
        private readonly DblibraryContext _context;

        public OrdersController(DblibraryContext context)
        {
            _context = context;
        }

        // GET: Orders
        //cc 5may 10 11
        public async Task<IActionResult> Index(int? id, string? firstName, string? lastName, string? phoneNumber, string? email, string? address)
        {
            /*var dblibraryContext = _context.Orders.Include(o => o.Customer);
            return View(await dblibraryContext.ToListAsync());*/
            if (id == null) return RedirectToAction("Index", "Customers");
            ViewBag.CustomerId = id;
            ViewBag.CustomerFirstName = firstName;
            ViewBag.CustomerLastName = lastName;
            ViewBag.CustomerPhoneNumber = phoneNumber;
            ViewBag.CustomerEmail = email;
            ViewBag.CustomerAddress = address;
            var ordersByCustomer = _context.Orders.Where(o => o.CustomerId == id).Include(o => o.Customer);
            return View(await ordersByCustomer.ToListAsync());

        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        //cc 5may 10 30
        // GET: Orders/Create
        public IActionResult Create(int customerId)
        {
            /*ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();*/
            ViewBag.CustomerId = customerId;
            ViewBag.CustomerFirstName = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().FirstName;
            ViewBag.CustomerLastName = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().LastName;
            ViewBag.CustomerPhoneNumber = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().PhoneNumber;
            ViewBag.CustomerEmail = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().Email;
            ViewBag.CustomerAddress = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().Address;

            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //cc 5may 10 36
        public async Task<IActionResult> Create(int customerId, [Bind("Id,Date,Time,ProductId,ProductQuantity,ProductPrice,CustomerId")] Order order)
        {
            /*if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            return View(order);*/
            order.CustomerId = customerId;
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Orders", new
                {
                    id = customerId,
                    firstName = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().FirstName,
                    lastName = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().LastName,
                    phoneNumber = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().PhoneNumber,
                    email = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().Email,
                    address = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().Address
                });
            }
            return RedirectToAction("Index", "Orders", new
            {
                id = customerId,
                firstName = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().FirstName,
                lastName = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().LastName,
                phoneNumber = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().PhoneNumber,
                email = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().Email,
                address = _context.Customers.Where(u => u.Id == customerId).FirstOrDefault().Address
            });
        }



        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Time,ProductId,ProductQuantity,ProductPrice,CustomerId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'DblibraryContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
