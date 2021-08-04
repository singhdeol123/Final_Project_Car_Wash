using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Project_Car_Wash.Data;
using Final_Project_Car_Wash.Models;
using Microsoft.AspNetCore.Authorization;

namespace Final_Project_Car_Wash.Controllers
{
    public class BookingsController : Controller
    {
        private readonly Final_Project_Car_WashDbContext _context;

        public BookingsController(Final_Project_Car_WashDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        [Authorize(Roles = "carwashAdmin, customer")]
        public async Task<IActionResult> Index()
        
        {
            var final_Project_Car_WashContext = _context.Booking.Include(b => b.Customer).Include(s => s.Services);
            if (User.IsInRole("customer"))
            {

                var final_Project_Car_WashContextCustomer = _context.Booking.Include(b => b.Customer)
                    .Include(b => b.Services).Where(s => s.Services.TypeofServices.Equals(User.Identity.Name));
                return View(await final_Project_Car_WashContext.ToListAsync());

            }

            {
                return View(await (from book in _context.Booking
                                   select book).ToListAsync()
                                  );
            }
        }


       

        // GET: Bookings/Details/5
        [Authorize(Roles = "carwashAdmin, customer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Customer)
                .Include(b => b.Services)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        [Authorize(Roles = "carwashAdmin")]
        public IActionResult Create()
        {
            return View();
        }


        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "carwashAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date,ServiceId,CustomerId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", booking.CustomerId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        [Authorize(Roles = "carwashAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", booking.CustomerId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "carwashAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,ServiceId,CustomerId")] Booking booking)
        {
            if (id != booking.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.ID))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", booking.CustomerId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        [Authorize(Roles = "carwashAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [Authorize(Roles = "carwashAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.ID == id);
        }
    }
}
