using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cloud_BikeStore_Femia.Models;
using Microsoft.AspNetCore.Authorization;

namespace Cloud_BikeStore_Femia.Controllers
{
    
    public class OrdersController : Controller
    {
        private readonly Cloud_BikeStoresContext _context;

        public OrdersController(Cloud_BikeStoresContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole("Administrators"))
            {
                var bikeStoresContext = _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Staff)
                    .Include(o => o.Store)
                    .Include(o => o.OrderStatus);
                return View(await bikeStoresContext.ToListAsync());
            }

            else if (User.IsInRole("Employees"))
            {
                var bikeStoresContext = _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Staff)
                    .Include(o => o.Store)
                    .Include(o => o.OrderStatus)
                    .Where(o => o.Staff.Email == User.Identity.Name);
                return View(await bikeStoresContext.ToListAsync());
            }

            else if (User.IsInRole("Customers"))
            {
                var bikeStoresContext = _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Staff)
                    .Include(o => o.Store)
                    .Include(o => o.OrderStatus)
                    .Where(o => o.Customer.Email == User.Identity.Name);
                return View(await bikeStoresContext.ToListAsync());
            }

            else { return NotFound(); }
            

        }

        //elenco ordini

        public async Task<IActionResult> ListOrdersByCustomer(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Where(o => o.CustomerId == id)
                .Include(o => o.Customer)
                .Include(o => o.OrderStatus)
                .Include(o => o.Staff)
                .Include(o => o.Store)
                .ToListAsync();

            if (orders == null)
            {
                return NotFound();
            }
            return View("Index", orders);
        }



        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderStatus)
                .Include(o => o.Staff)
                .Include(o => o.Store)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            foreach(var p in order.OrderItems)
            {
                p.Product = await _context.Products.Where(n => n.ProductId == p.ProductId).FirstOrDefaultAsync();
                    
            }

            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles = "Administrators")]
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email");
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "StatusId", "StatusName");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email");
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,OrderStatusId,OrderDate,RequiredDate,ShippedDate,StoreId,StaffId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", order.CustomerId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "StatusId", "StatusName", order.OrderStatusId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email", order.StaffId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", order.StoreId);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", order.CustomerId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "StatusId", "StatusName", order.OrderStatusId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email", order.StaffId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", order.StoreId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,OrderStatusId,OrderDate,RequiredDate,ShippedDate,StoreId,StaffId")] Order order)
        {
            if (id != order.OrderId)
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
                    if (!OrderExists(order.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", order.CustomerId);
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "StatusId", "StatusName", order.OrderStatusId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email", order.StaffId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", order.StoreId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderStatus)
                .Include(o => o.Staff)
                .Include(o => o.Store)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [Authorize(Roles = "Administrators")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
