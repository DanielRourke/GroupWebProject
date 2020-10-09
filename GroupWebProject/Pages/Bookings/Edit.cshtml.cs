using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroupWebProject.Data;
using GroupWebProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.Sqlite;

namespace GroupWebProject.Pages.Bookings
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly GroupWebProject.Data.ApplicationDbContext _context;

        public EditModel(GroupWebProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking = await _context.Booking
                .Include(b => b.TheCustomer)
                .Include(b => b.TheRoom).FirstOrDefaultAsync(m => m.ID == id);

            if (Booking == null)
            {
                return NotFound();
            }
           ViewData["CustomerEmail"] = new SelectList(_context.Customer, "Email", "FullName");
           ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["CustomerEmail"] = new SelectList(_context.Customer, "Email", "FullName");
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Booking).State = EntityState.Modified;

            //TODO SQL VALIDATIION
            //Check new dates are aviable
            //dont include current booking in sub query 


            //raw sql
            var roomID = new SqliteParameter("roomID", Booking.RoomID);
            var checkIn = new SqliteParameter("checkIn", Booking.CheckIn);
            var checkOut = new SqliteParameter("checkOut", Booking.CheckOut);
            var bookingID = new SqliteParameter("bookingID", Booking.ID);


            String query = "SELECT [Room].* FROM Room " +
                            "WHERE [Room].ID = @roomID ";

            String subQuery = "(SELECT [Room].ID " +
                              "FROM [Room] " +
                              "INNER JOIN [Booking] " +
                              "ON [Room].ID = [Booking].RoomId " +
                              "WHERE @checkIn < Booking.Checkout " +
                              "AND Booking.CheckIn < @checkOut  " +
                                "AND Booking.ID != @bookingID ) ";


            String notQuery = query + " AND [Room].ID NOT IN " + subQuery;


            var searchQuery = _context.Room.FromSqlRaw(notQuery, roomID, checkIn, checkOut, bookingID);

            var thing = await searchQuery.ToListAsync();
            //TODO FIX BULLSHIT OUTPUT
            if (thing.Count == 1)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(Booking.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

           
            }
            else
            {
                ViewData["SuccessDB"] = "Booking not available";
                return Page();
            }


            return RedirectToPage("./Index");




        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.ID == id);
        }
    }
}
