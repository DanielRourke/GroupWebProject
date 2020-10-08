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
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.Sqlite;

namespace GroupWebProject.Pages.Bookings
{
    [Authorize(Roles = "Customers")]
    public class BookRoomModel : PageModel
    {
        private readonly GroupWebProject.Data.ApplicationDbContext _context;

        public BookRoomModel(GroupWebProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public RoomBooking RoomBooking { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            else if (RoomBooking.CheckIn < DateTime.Today)
            {
                ModelState.AddModelError("Booking.CheckIn", "Check in Date Must be in the future");
                return Page();
            }
            else if (RoomBooking.CheckIn > RoomBooking.CheckOut)
            {
                ModelState.AddModelError("Booking.CheckOut", "Check Out Date Must after Check In Date");
                return Page();
            }


            string _email = User.FindFirst(ClaimTypes.Name).Value;

            Booking Booking = new Booking
            {
                TheCustomer = await _context.Customer.FirstOrDefaultAsync(m => m.Email == _email),
                TheRoom = await _context.Room.FirstOrDefaultAsync(m => m.ID == RoomBooking.RoomID),
                RoomID = RoomBooking.RoomID,
                CustomerEmail = _email,
                CheckIn = RoomBooking.CheckIn,
                CheckOut = RoomBooking.CheckOut
            };
            int days = (int)(Booking.CheckOut - Booking.CheckIn).TotalDays;
            Booking.Cost = days * Booking.TheRoom.Price;


            //raw sql
            var roomID = new SqliteParameter("roomID", RoomBooking.RoomID);
            var checkIn = new SqliteParameter("checkIn", RoomBooking.CheckIn);
            var checkOut = new SqliteParameter("checkOut", RoomBooking.CheckOut);



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
            if (thing.Count == 1)
            {
                _context.Booking.Add(Booking);
                await _context.SaveChangesAsync();
            }
            
            //TODO BULLSHIT OUTPUT

            return RedirectToPage("./Index");
        }
    }
}
