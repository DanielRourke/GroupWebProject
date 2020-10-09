using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GroupWebProject.Data;
using GroupWebProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace GroupWebProject.Pages.Bookings
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly GroupWebProject.Data.ApplicationDbContext _context;

        public CreateModel(GroupWebProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerEmail"] = new SelectList(_context.Customer, "Email", "FullName");
        ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //TODO ADD SQL VALIDATION
            //check dates are availablew
            //raw sql
            var roomID = new SqliteParameter("roomID", Booking.RoomID);
            var checkIn = new SqliteParameter("checkIn", Booking.CheckIn);
            var checkOut = new SqliteParameter("checkOut", Booking.CheckOut);



            String query = "SELECT [Room].* FROM Room " +
                            "WHERE [Room].ID = @roomID ";

            String subQuery = "(SELECT [Room].ID " +
                              "FROM [Room] " +
                              "INNER JOIN [Booking] " +
                              "ON [Room].ID = [Booking].RoomId " +
                              "WHERE @checkIn < Booking.Checkout " +
                              "AND Booking.CheckIn < @checkOut ) ";



            String notQuery = query + " AND [Room].ID NOT IN " + subQuery;


            var searchQuery = _context.Room.FromSqlRaw(notQuery, roomID, checkIn, checkOut);

            var thing = await searchQuery.ToListAsync();

            //TODO FIX BULLSHIT OUTPUT
            if (thing.Count == 1)
            {

                _context.Booking.Add(Booking);
                await _context.SaveChangesAsync();
                ViewData["SuccessDB"] = $"Booked, {Booking.RoomID} on level {Booking.TheRoom.Level}" +
                    $"from {Booking.CheckIn:d} to {Booking.CheckOut:d} for {Booking.Cost:C2} ";
            }
            else
            {
                ViewData["SuccessDB"] = "Booking not available";
                return Page();
            }


            return RedirectToPage("./Index");
        }
    }
}
